using ErrorOr;

namespace Application.UseCases.Patient.Delete.Interfaces;

public interface ISendDeletePatientRequestUseCase
{
    Task<Error?> Execute(long id, CancellationToken cancellationToken = default);
}
