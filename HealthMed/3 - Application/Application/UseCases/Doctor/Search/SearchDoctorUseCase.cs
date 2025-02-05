using Application.UseCases.Doctor.Search.Common;
using Application.UseCases.Doctor.Search.Interfaces;
using Domain.DomainObjects.Filters;
using Domain.Repositories.Relational;
using ErrorOr;

namespace Application.UseCases.Doctor.Search;

public  class SearchDoctorUseCase : ISeachDoctorUseCase
{
    readonly IDoctorRepository _doctorRepository;
    public SearchDoctorUseCase(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }
    public async Task<ErrorOr<PaginationResult<SearchDoctorResponse>>> Execute(DoctorFilter filter, CancellationToken cancellationToken = default)
    {
        var result = await _doctorRepository.SearchAsync(filter, cancellationToken);

        return new PaginationResult<SearchDoctorResponse>(result.Total, result.Items.Select(item => SearchDoctorResponse.Create(item)));
    }
}
