using ErrorOr;

namespace Application.UseCases.PatientUseCases.DeletePatient.Interfaces;

public interface IDeletePatientProcessingUseCase
{
    Task<Error?> Execute(long id, CancellationToken cancellationToken = default);
}
