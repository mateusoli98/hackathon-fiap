using Application.UseCases.Patient.Search.Common;
using Application.UseCases.Patient.Search.Interfaces;
using Domain.DomainObjects.Filters;
using Domain.Repositories.Relational;
using ErrorOr;

namespace Application.UseCases.Patient.Search;

public  class SearchAppointmentUseCase : ISeachAppointmentUseCase
{
    readonly IPatientRepository _patientRepository;
    public SearchAppointmentUseCase(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }
    public async Task<ErrorOr<PaginationResult<SearchAppointmentResponse>>> Execute(PatientFilter filter, CancellationToken cancellationToken = default)
    {
        var result = await _patientRepository.SearchAsync(filter, cancellationToken);

        return new PaginationResult<SearchAppointmentResponse>(result.Total, result.Items.Select(item => SearchAppointmentResponse.Create(item)));
    }
}
