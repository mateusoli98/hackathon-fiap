using Application.UseCases.Patient.Get.Common;
using ErrorOr;

namespace Application.UseCases.Patient.Get.Interfaces;

public interface IGetPatientUseCase
{
    Task<ErrorOr<GetPatientResponse>> Execute(long id, CancellationToken cancellationToken = default);
}
