
using ErrorOr;

namespace Application.UseCases.PatientUseCases.DeletePatientPermanently.Interfaces;

public interface ISendDeletePatientPermanentlyRequestUseCase
{
    Task<Error?> Execute(long id, CancellationToken cancellationToken = default);
}
