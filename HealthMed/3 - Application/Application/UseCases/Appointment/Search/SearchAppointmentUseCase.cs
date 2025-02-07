using Application.UseCases.Appointment.Search.Common;
using Application.UseCases.Appointment.Search.Interfaces;
using Domain.DomainObjects.Filters;
using Domain.Repositories.Relational;
using ErrorOr;

namespace Application.Appointment.Search.Search;

public  class SearchAppointmentUseCase : ISearchAppointmentUseCase
{
    readonly IAppointmentRepository _appointmentRepository;
    public SearchAppointmentUseCase(IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    public async Task<ErrorOr<PaginationResult<SearchAppointmentResponse>>> Execute(AppointmentFilter filter, CancellationToken cancellationToken = default)
    {
        var result = await _appointmentRepository.SearchAsync(filter, cancellationToken);

        return new PaginationResult<SearchAppointmentResponse>(result.Total, result.Items.Select(item => SearchAppointmentResponse.Create(item)));
    }
}
