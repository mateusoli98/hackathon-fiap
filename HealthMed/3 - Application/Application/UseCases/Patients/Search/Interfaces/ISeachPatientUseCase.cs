using Application.UseCases.Patient.Search.Common;
using Domain.DomainObjects.Filters;
using ErrorOr;

namespace Application.UseCases.Patient.Search.Interfaces;

public interface ISeachAppointmentUseCase
{
    Task<ErrorOr<PaginationResult<SearchAppointmentResponse>>> Execute(PatientFilter filter, CancellationToken cancellationToken = default);
}
