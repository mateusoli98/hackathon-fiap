namespace Application.UseCases.Appointment.Create.Interfaces;
using Domain.Entities;

public interface ICreateAppointmentProcessingUseCase
{
    Task Execute(Appointments appointments, CancellationToken cancellationToken = default);
}
