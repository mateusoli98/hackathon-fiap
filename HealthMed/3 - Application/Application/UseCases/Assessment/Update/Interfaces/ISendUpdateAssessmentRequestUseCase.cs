using Application.Assessment.Update.Common;
using Application.UseCases.Assessment.Common;
using ErrorOr;

namespace Application.UseCases.Assessment.Interfaces;

public interface ISendUpdateAssessmentRequestUseCase
{
    Task<ErrorOr<UpdateAssessmentResponse>> Execute(string assessmentId, UpdateAssessmentRequest request, CancellationToken cancellationToken = default);
}
