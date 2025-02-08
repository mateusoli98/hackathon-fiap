using Application.Appointment.Update.Common;
using Application.UseCases.Appointment.Common;
using ErrorOr;

namespace Application.UseCases.Appointment.Interfaces;

public interface IUpdateAppointmentProcessingUseCase
{
    Task<ErrorOr<UpdateAppointmentResponse>> Execute(long id, UpdateAppointmentRequest appointment, CancellationToken cancellationToken = default);
}
