
using ErrorOr;

namespace Application.UseCases.Assessment.DeletePermanently.Interfaces;

public interface ISendDeleteAssessmentPermanentlyRequestUseCase
{
    Task<Error?> Execute(long id, CancellationToken cancellationToken = default);
}
