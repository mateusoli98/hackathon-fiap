using Application.UseCases.Doctor.Create.Commom;
using Application.UseCases.Doctor.Create.Interfaces;
using Domain.Repositories.Relational;
using CrossCutting.Extensions;
using ErrorOr;
using DoctorEntity = Domain.Entities.Doctor;

namespace Application.UseCases.Doctor.Create;

public class CreateDoctorProcessingUseCase(IDoctorRepository repository) : ICreateDoctorProcessingUseCase
{
    private readonly IDoctorRepository _doctorRepository = repository;

    public async Task<ErrorOr<CreateDoctorResponse>> Execute(CreateDoctorRequest request, CancellationToken cancellationToken = default)
    {
        var validationResult = new CreateDoctorRequestValidator().Validate(request);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrorList();
        }

        var alreadyExists = await _doctorRepository.Exists(request.CRM, cancellationToken);
        if (!alreadyExists)
        {
            var doctor = new DoctorEntity()
            {
                Name = request.Name,
                CRM = request.CRM,
                Specialty = request.Specialty,
                Email = request.Email,
                Password = request.Password,
                IsEnabled = true,
                CreatedAt = DateTime.UtcNow
            };

            doctor.Id = await _doctorRepository.SaveAsync(doctor, cancellationToken);

            return new CreateDoctorResponse()
            {
                Id = doctor.Id.ToString(),
                Message = $"Solicitação de criação do médico com id {doctor.Id} enviada com sucesso"
            };
        }

        return Error.Validation("Validation", "Médico informado já está cadastrado.");
    }
}
