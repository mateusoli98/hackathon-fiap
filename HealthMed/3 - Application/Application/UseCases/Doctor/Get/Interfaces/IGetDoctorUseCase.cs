using Application.UseCases.Doctor.Get.Common;
using ErrorOr;

namespace Application.UseCases.Doctor.Get.Interfaces;

public interface IGetDoctorUseCase
{
    Task<ErrorOr<GetDoctorResponse>> Execute(long id, CancellationToken cancellationToken = default);
}
