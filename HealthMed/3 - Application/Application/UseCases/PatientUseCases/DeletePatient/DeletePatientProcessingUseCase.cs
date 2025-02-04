using Application.UseCases.PatientUseCases.DeletePatient.Interfaces;
using Domain.Repositories.Relational;
using ErrorOr;

namespace Application.UseCases.PatientUseCases.DeletePatient;

public class DeletePatientProcessingUseCase : IDeletePatientProcessingUseCase
{
    readonly IPatientRepository _patientRepository;

    public DeletePatientProcessingUseCase(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<Error?> Execute(long id, CancellationToken cancellationToken = default)
    {
        var patient = await _patientRepository.GetByIdAsync(id, cancellationToken);

        if (patient is not null)
        {
            await _patientRepository.DeleteAsync(patient, cancellationToken);
            return null;
        }

        return Error.NotFound();
    }
}
