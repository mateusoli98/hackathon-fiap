namespace Application.UseCases.Appointment.Create.Interfaces;

using Application.UseCases.Appointment.Create.Commom;
using ErrorOr;

public interface ICreateAppointmentProcessingUseCase
{
    Task<ErrorOr<CreateAppointmentResponse>> Execute(CreateAppointmentRequest appointments, CancellationToken cancellationToken = default);
}
