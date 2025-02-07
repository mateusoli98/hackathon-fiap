using Application.UseCases.Patient.Search.Common;
using Domain.DomainObjects.Filters;
using ErrorOr;

namespace Application.UseCases.Patient.Search.Interfaces;

public interface ISearchPatientUseCase
{
    Task<ErrorOr<PaginationResult<SearchPatientResponse>>> Execute(PatientFilter filter, CancellationToken cancellationToken = default);
}
