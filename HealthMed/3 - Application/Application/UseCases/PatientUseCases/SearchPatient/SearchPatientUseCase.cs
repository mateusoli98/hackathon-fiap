using Application.UseCases.PatientUseCases.SearchPatient.Common;
using Application.UseCases.PatientUseCases.SearchPatient.Interfaces;
using Domain.DomainObjects.Filters;
using Domain.Repositories.Relational;
using ErrorOr;

namespace Application.UseCases.PatientUseCases.SearchPatient;

public  class SearchPatientUseCase : ISeachPatientUseCase
{
    readonly IPatientRepository _patientRepository;
    public SearchPatientUseCase(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }
    public async Task<ErrorOr<PaginationResult<SearchPatientResponse>>> Execute(PatientFilter filter, CancellationToken cancellationToken = default)
    {
        var result = await _patientRepository.SearchAsync(filter, cancellationToken);

        return new PaginationResult<SearchPatientResponse>(result.Total, result.Items.Select(item => SearchPatientResponse.Create(item)));
    }
}
