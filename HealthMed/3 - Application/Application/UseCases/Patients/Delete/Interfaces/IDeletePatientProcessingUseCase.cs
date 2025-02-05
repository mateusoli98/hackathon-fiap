using ErrorOr;

namespace Application.UseCases.Patient.Delete.Interfaces;

public interface IDeleteAppointmentProcessingUseCase
{
    Task<Error?> Execute(long id, CancellationToken cancellationToken = default);
}
