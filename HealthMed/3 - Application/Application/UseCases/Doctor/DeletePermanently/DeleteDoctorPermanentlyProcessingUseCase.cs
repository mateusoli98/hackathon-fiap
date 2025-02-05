using Application.UseCases.Doctor.DeletePermanently.Interfaces;
using Domain.Repositories.Relational;

namespace Application.UseCases.Doctor.DeletePermanently;

public class DeleteDoctorPermanentlyProcessingUseCase : IDeleteDoctorPermanentlyProcessingUseCase
{
    private readonly IDoctorRepository _doctorRepository;

    public DeleteDoctorPermanentlyProcessingUseCase(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public async Task Execute(long id, CancellationToken cancellationToken = default)
    {
        var doctor = await _doctorRepository.GetByIdAsync(id, cancellationToken);

        if (doctor is not null)
        {
            await _doctorRepository.PermanentDelete(doctor, cancellationToken);
            return;
        }

        throw new Exception("Médico não encontrado.");
    }
}
