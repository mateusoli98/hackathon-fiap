namespace Application.UseCases.Appointment.Create.Interfaces;

using Application.UseCases.Appointment.Create.Commom;
using ErrorOr;


public interface ISendCreateAppointmentRequestUseCase
{
    Task<ErrorOr<CreateAppointmentResponse>> Execute(CreateAppointmentRequest request, CancellationToken cancellationToken = default);
}
