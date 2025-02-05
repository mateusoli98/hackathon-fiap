
namespace Application.UseCases.Patient.Update.Interfaces;

using Domain.Entities;

public interface IUpdateAppointmentProcessingUseCase
{
    Task Execute(Patient patient, CancellationToken cancellationToken = default);
}
