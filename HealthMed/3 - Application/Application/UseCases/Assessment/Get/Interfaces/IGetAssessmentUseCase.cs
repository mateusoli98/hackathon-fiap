using Application.UseCases.Assessment.Get.Common;
using ErrorOr;

namespace Application.UseCases.Assessment.Get.Interfaces;

public interface IGetAssessmentUseCase
{
    Task<ErrorOr<GetAssessmentResponse>> Execute(long id, CancellationToken cancellationToken = default);
}
