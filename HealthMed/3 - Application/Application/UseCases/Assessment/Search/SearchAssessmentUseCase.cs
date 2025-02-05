using Application.UseCases.Assessment.Search.Common;
using Application.UseCases.Assessment.Search.Interfaces;
using Domain.DomainObjects.Filters;
using Domain.Repositories.Relational;
using ErrorOr;

namespace Application.Assessment.Search.Search;

public  class SearchAssessmentUseCase : ISeachAssessmentUseCase
{
    readonly IAssessmentRepository _assessmentRepository;
    public SearchAssessmentUseCase(IAssessmentRepository assessmentRepository)
    {
        _assessmentRepository = assessmentRepository;
    }

    public async Task<ErrorOr<PaginationResult<SearchAssessmentResponse>>> Execute(AssessmentFilter filter, CancellationToken cancellationToken = default)
    {
        var result = await _assessmentRepository.SearchAsync(filter, cancellationToken);

        return new PaginationResult<SearchAssessmentResponse>(result.Total, result.Items.Select(item => SearchAssessmentResponse.Create(item)));
    }
}
