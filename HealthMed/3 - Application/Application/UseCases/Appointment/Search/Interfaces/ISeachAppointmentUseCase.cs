using Application.UseCases.Appointment.Search.Common;
using Domain.DomainObjects.Filters;
using ErrorOr;

namespace Application.UseCases.Appointment.Search.Interfaces;

public interface ISeachAppointmentUseCase
{
    Task<ErrorOr<PaginationResult<SearchAppointmentResponse>>> Execute(AppointmentFilter filter, CancellationToken cancellationToken = default);
}
