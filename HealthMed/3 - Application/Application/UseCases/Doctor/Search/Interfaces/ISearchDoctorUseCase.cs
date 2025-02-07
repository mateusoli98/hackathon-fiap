using Application.UseCases.Doctor.Search.Common;
using Domain.DomainObjects.Filters;
using ErrorOr;

namespace Application.UseCases.Doctor.Search.Interfaces;

public interface ISearchDoctorUseCase
{
    Task<ErrorOr<PaginationResult<SearchDoctorResponse>>> Execute(DoctorFilter filter, CancellationToken cancellationToken = default);
}
