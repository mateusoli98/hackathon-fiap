using Application.UseCases.Patient.Get.Common;
using ErrorOr;

namespace Application.UseCases.Patient.Get.Interfaces;

public interface IGetPatientUseCase
{
    Task<ErrorOr<GetAppoitmentResponse>> Execute(long id, CancellationToken cancellationToken = default);
}
