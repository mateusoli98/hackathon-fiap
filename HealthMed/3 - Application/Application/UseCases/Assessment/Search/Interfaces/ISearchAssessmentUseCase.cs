using Application.UseCases.Assessment.Search.Common;
using Domain.DomainObjects.Filters;
using ErrorOr;

namespace Application.UseCases.Assessment.Search.Interfaces;

public interface ISearchAssessmentUseCase
{
    Task<ErrorOr<PaginationResult<SearchAssessmentResponse>>> Execute(AssessmentFilter filter, CancellationToken cancellationToken = default);
}
