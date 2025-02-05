using Application.UseCases.Doctor.Get.Common;
using Application.UseCases.Doctor.Get.Interfaces;
using Domain.Repositories.Relational;
using ErrorOr;

namespace Application.UseCases.Doctor.Get;

public class GetDoctorUseCase : IGetDoctorUseCase
{
    private readonly IDoctorRepository _doctorRepository;

    public GetDoctorUseCase(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public async Task<ErrorOr<GetDoctorResponse>> Execute(long id, CancellationToken cancellationToken = default)
    {
        var doctor = await _doctorRepository.GetByIdAsync(id, cancellationToken);

        if (doctor is not null)
        {
            return GetDoctorResponse.Create(doctor);
        }

        return Error.NotFound(description: $"Médico com id: {id} não encontrado. Revise o Id informado ou tente novamente mais tarde");
    }
}
