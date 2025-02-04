namespace Application.UseCases.UpdatePatient;

using Application.UseCases.UpdatePatient.Interfaces;
using Domain.Repositories.Relational;
using Domain.Entities;

public class UpdatePatientProcessingUseCase(IPatientRepository repository) : IUpdatePatientProcessingUseCase
{
    private readonly IPatientRepository _patientRepository = repository;

    public async Task Execute(Patient updatedPatient, CancellationToken cancellationToken = default)
    {
        var patient = await _patientRepository.GetByIdAsync(updatedPatient.Id!, cancellationToken);

        if (patient is null)
        {
            throw new Exception("Paciente não encontrado."); ;
        }

        var alreadyExists = await Validate(patient, updatedPatient, cancellationToken);
        if (!alreadyExists)
        {
            patient.Name = updatedPatient.Name;
            patient.Email = updatedPatient.Email;  
            patient.CPF = updatedPatient.CPF;

            await _patientRepository.UpdateAsync(patient, cancellationToken);

            return;
        }

        throw new Exception("CPF informado já está cadastrado no sistema.");
    }

    private async Task<bool> Validate(Patient findedPatient, Patient updatedPatient, CancellationToken cancellationToken)
    {
        if (updatedPatient.CPF != findedPatient.CPF)
        {
            var alreadyExists = await _patientRepository.Exists(updatedPatient.CPF, cancellationToken);

            return alreadyExists;
        }

        return true;
    }

}
