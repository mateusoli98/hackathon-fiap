using Application.UseCases.Patient.Search.Common;
using Domain.DomainObjects.Filters;
using ErrorOr;

namespace Application.UseCases.Patient.Search.Interfaces;

public interface ISeachPatientUseCase
{
    Task<ErrorOr<PaginationResult<SearchPatientResponse>>> Execute(PatientFilter filter, CancellationToken cancellationToken = default);
}
