namespace Application.UseCases.Patient.Create;

using Application.UseCases.Patient.Create.Commom;
using Application.UseCases.Patient.Create.Interfaces;
using CrossCutting.Extensions;
using Domain.Entities;
using Domain.Repositories.Relational;
using ErrorOr;

public class CreatePatientProcessingUseCase(IPatientRepository repository) : ICreatePatientProcessingUseCase
{
    private readonly IPatientRepository _patientRepository = repository;

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
                Password = request.Password.HashPassword(),
                IsEnabled = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            patient.Id = await _patientRepository.SaveAsync(patient, cancellationToken);

            return new CreatePatientResponse()
            {
                Id = patient.Id.ToString(),
                Message = $"Paciente {patient.Id} criado com sucesso."
            };
        }

        return Error.Validation("Validation", "Paciente informado já está cadastrado.");
    }
}
