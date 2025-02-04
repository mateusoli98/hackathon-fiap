
namespace Application.UseCases.UpdatePatient.Interfaces;

using Domain.Entities;

public interface IUpdatePatientProcessingUseCase
{
    Task Execute(Patient patient, CancellationToken cancellationToken = default);
}
