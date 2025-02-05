using ErrorOr;

namespace Application.UseCases.Appointment.Delete.Interfaces;

public interface ISendDeleteAppointmentRequestUseCase
{
    Task<Error?> Execute(long id, CancellationToken cancellationToken = default);
}
