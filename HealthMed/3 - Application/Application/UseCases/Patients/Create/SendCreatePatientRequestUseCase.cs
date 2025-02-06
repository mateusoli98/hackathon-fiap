using Application.UseCases.Patient.Create.Commom;
using Domain.Repositories.Relational;
using ErrorOr;
using Infra.Services.Messages;
using System.Text.Json;
using CrossCutting.Extensions;

namespace Application.UseCases.Patient.Create;

using Application.UseCases.Patient.Create.Interfaces;
using Domain.Entities;

public class SendCreatePatientRequestUseCase : ISendCreatePatientRequestUseCase
{
    private readonly IRabbitMqProducerService _rabbitMqService;
    private readonly IPatientRepository _patientRepository;

    public SendCreatePatientRequestUseCase(IRabbitMqProducerService rabbitMqProducerService, IPatientRepository patientRepository)
    {
        _rabbitMqService = rabbitMqProducerService;
        _patientRepository = patientRepository;
    }

    public async Task<ErrorOr<CreatePatientResponse>> Execute(CreatePatientRequest request, CancellationToken cancellationToken = default)
    {
        var validationResult = new CreatePatientRequestValidator().Validate(request);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrorList();
        }

        var alreadyExists = await _patientRepository.Exists(request.CPF, cancellationToken);
        if (!alreadyExists)
        {
            var patient = new Patient()
            {
                Name = request.Name,
                CPF = request.CPF,
                Email = request.Email,
                Password = request.Password, // criptografar
                IsEnabled = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            _rabbitMqService.SendMessage(JsonSerializer.Serialize(patient), "create_patient");

            return new CreatePatientResponse()
            {
                Id = patient.Id.ToString(),
                Message = $"Solicitação de criação do paciente com id {patient.Id} enviada com sucesso"
            };
        }

        return Error.Validation("Validation", "Paciente informado já está cadastrado.");
    }
}
