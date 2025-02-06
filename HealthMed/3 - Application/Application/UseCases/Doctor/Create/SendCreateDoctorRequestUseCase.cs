namespace Application.UseCases.Doctor.Create;

using Application.UseCases.Doctor.Create.Commom;
using Application.UseCases.Doctor.Create.Interfaces;
using Domain.Repositories.Relational;
using ErrorOr;
using Infra.Services.Messages;
using CrossCutting.Extensions;

using Domain.Entities;
using System.Text.Json;

public class SendCreateDoctorRequestUseCase : ISendCreateDoctorRequestUseCase
{

    private readonly IRabbitMqProducerService _rabbitMqService;
    private readonly IDoctorRepository _doctorRepository;

    public SendCreateDoctorRequestUseCase(IRabbitMqProducerService rabbitMqProducerService, IDoctorRepository doctorRepository)
    {
        _rabbitMqService = rabbitMqProducerService;
        _doctorRepository = doctorRepository;        
    }

    public async Task<ErrorOr<CreateDoctorResponse>> Execute(CreateDoctorRequest request, CancellationToken cancellationToken = default)
    {
        var validationResult = new CreateDoctorRequestValidator().Validate(request);
        if(!validationResult.IsValid)
        {
            return validationResult.ToErrorList();
        }

        var alreadyExists = await _doctorRepository.Exists(request.CRM, cancellationToken);
        if(!alreadyExists)
        {
            var doctor = new Doctor()
            {
                Name = request.Name,
                CRM = request.CRM,
                Specialty = request.Specialty,
                Email = request.Email,
                Password = request.Password,
                IsEnabled = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            _rabbitMqService.SendMessage(JsonSerializer.Serialize(doctor), "create_doctor");

            return new CreateDoctorResponse()
            {
                Id = doctor.Id.ToString(),
                Message = $"Solicitação de criação do médico com id {doctor.Id} enviada com sucesso"
            };
        }

        return Error.Validation("Validation", "Médico informado já está cadastrado.");
    }
}
