
namespace Application.UseCases.Patient.Update.Interfaces;

using Domain.Entities;

public interface IUpdatePatientProcessingUseCase
{
    Task Execute(Patient patient, CancellationToken cancellationToken = default);
}
