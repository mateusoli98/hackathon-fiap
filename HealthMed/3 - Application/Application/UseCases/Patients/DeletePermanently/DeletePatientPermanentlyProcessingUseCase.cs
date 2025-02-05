using Application.UseCases.Patient.DeletePermanently.Interfaces;
using Domain.Repositories.Relational;

namespace Application.UseCases.Patient.DeletePermanently;

public class DeleteAppointmentPermanentlyProcessingUseCase : IDeleteAppointmentPermanentlyProcessingUseCase
{
    private readonly IPatientRepository _patientRepository;

    public DeleteAppointmentPermanentlyProcessingUseCase(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task Execute(long id, CancellationToken cancellationToken = default)
    {
        var patient = await _patientRepository.GetByIdAsync(id, cancellationToken);

        if (patient is not null)
        {
            await _patientRepository.PermanentDelete(patient, cancellationToken);
            return;
        }

        throw new Exception("Paciente não encontrado.");
    }
}
