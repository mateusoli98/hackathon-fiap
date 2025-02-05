using Application.UseCases.Doctor.Delete.Interfaces;
using Domain.Repositories.Relational;
using ErrorOr;

namespace Application.UseCases.Doctor.Delete;

public class DeleteDoctorProcessingUseCase : IDeleteDoctorProcessingUseCase
{
    readonly IDoctorRepository _doctorRepository;

    public DeleteDoctorProcessingUseCase(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public async Task<Error?> Execute(long id, CancellationToken cancellationToken = default)
    {
        var doctor = await _doctorRepository.GetByIdAsync(id, cancellationToken);

        if (doctor is not null)
        {
            await _doctorRepository.DeleteAsync(doctor, cancellationToken);
            return null;
        }

        return Error.NotFound();
    }
}
