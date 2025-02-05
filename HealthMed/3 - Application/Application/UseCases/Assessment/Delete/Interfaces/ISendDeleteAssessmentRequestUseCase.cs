using ErrorOr;

namespace Application.UseCases.Assessment.Delete.Interfaces;

public interface ISendDeleteAssessmentRequestUseCase
{
    Task<Error?> Execute(long id, CancellationToken cancellationToken = default);
}
