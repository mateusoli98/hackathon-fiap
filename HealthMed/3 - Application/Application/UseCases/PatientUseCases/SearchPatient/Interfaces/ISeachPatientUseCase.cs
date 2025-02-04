using Application.UseCases.PatientUseCases.SearchPatient.Common;
using Domain.DomainObjects.Filters;
using ErrorOr;

namespace Application.UseCases.PatientUseCases.SearchPatient.Interfaces;

public interface ISeachPatientUseCase
{
    Task<ErrorOr<PaginationResult<SearchPatientResponse>>> Execute(PatientFilter filter, CancellationToken cancellationToken = default);
}
