using Application.Appointment.Update.Common;
using Application.UseCases.Appointment.Common;
using ErrorOr;

namespace Application.UseCases.Appointment.Interfaces;

public interface ISendUpdateAppointmentRequestUseCase
{
    Task<ErrorOr<UpdateAppointmentResponse>> Execute(long appointmentId, UpdateAppointmentRequest request, CancellationToken cancellationToken = default);
}
