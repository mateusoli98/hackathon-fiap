
namespace Application.UseCases.Appointment.Interfaces;

using Domain.Entities;

public interface IUpdateAppointmentProcessingUseCase
{
    Task Execute(Appointments appointment, CancellationToken cancellationToken = default);
}
