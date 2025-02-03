using Application.UseCases.DoctorUseCases.SearchDoctor.Common;
using Domain.DomainObjects.Filters;
using ErrorOr;

namespace Application.UseCases.DoctorUseCases.SearchDoctor.Interfaces;

public interface ISeachDoctorUseCase
{
    Task<ErrorOr<PaginationResult<SearchDoctorResponse>>> Execute(DoctorFilter filter, CancellationToken cancellationToken = default);
}
