
using ErrorOr;

namespace Application.UseCases.Patient.DeletePermanently.Interfaces;

public interface ISendDeletePatientPermanentlyProcessingUseCase
{
    Task<Error?> Execute(long id, CancellationToken cancellationToken = default);
}
