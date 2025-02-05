using ErrorOr;

namespace Application.UseCases.Assessment.Delete.Interfaces;

public interface IDeleteAssessmentProcessingUseCase
{
    Task<Error?> Execute(long id, CancellationToken cancellationToken = default);
}
