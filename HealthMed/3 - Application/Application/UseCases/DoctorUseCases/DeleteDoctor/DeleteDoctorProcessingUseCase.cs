using Application.UseCases.DoctorUseCases.DeleteDoctor.Interfaces;
using Domain.Repositories.Relational;
using ErrorOr;

namespace Application.UseCases.DoctorUseCases.DeleteDoctor;

public class DeleteDoctorProcessingUseCase : IDeleteDoctorProcessingUseCase
{
    readonly IDoctorRepository _doctorRepository;

    public DeleteDoctorProcessingUseCase(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public async Task<Error?> Execute(long id, CancellationToken cancellationToken = default)
    {
        var contact = await _doctorRepository.GetByIdAsync(id, cancellationToken);

        if (contact is not null)
        {
            await _doctorRepository.DeleteAsync(contact, cancellationToken);
            return null;
        }

        return Error.NotFound();
    }
}
