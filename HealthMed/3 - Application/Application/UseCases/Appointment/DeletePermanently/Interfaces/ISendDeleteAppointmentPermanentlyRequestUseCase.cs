
using ErrorOr;

namespace Application.UseCases.Appointment.DeletePermanently.Interfaces;

public interface ISendDeleteAppointmentPermanentlyRequestUseCase
{
    Task<Error?> Execute(long id, CancellationToken cancellationToken = default);
}
