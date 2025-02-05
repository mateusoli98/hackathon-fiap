using Application.UseCases.Appointment.Get.Common;
using ErrorOr;

namespace Application.UseCases.Appointment.Get.Interfaces;

public interface IGetAppointmentUseCase
{
    Task<ErrorOr<GetAppoitmentResponse>> Execute(long id, CancellationToken cancellationToken = default);
}
