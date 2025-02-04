using ErrorOr;

namespace Application.UseCases.PatientUseCases.DeletePatient.Interfaces;

public interface ISendDeletePatientRequestUseCase
{
    Task<Error?> Execute(long id, CancellationToken cancellationToken = default);
}
