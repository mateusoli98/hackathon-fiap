using ErrorOr;

namespace Application.UseCases.Patient.Delete.Interfaces;

public interface ISendDeleteAppointmentRequestUseCase
{
    Task<Error?> Execute(long id, CancellationToken cancellationToken = default);
}
