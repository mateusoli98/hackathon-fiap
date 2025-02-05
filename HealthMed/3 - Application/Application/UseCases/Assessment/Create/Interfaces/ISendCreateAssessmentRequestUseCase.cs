namespace Application.UseCases.Assessment.Create.Interfaces;

using Application.UseCases.Assessment.Create.Commom;
using ErrorOr;


public interface ISendCreateAssessmentRequestUseCase
{
    Task<ErrorOr<CreateAssessmentResponse>> Execute(CreateAssessmentRequest request, CancellationToken cancellationToken = default);
}
