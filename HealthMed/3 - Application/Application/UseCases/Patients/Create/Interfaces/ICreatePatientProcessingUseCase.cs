namespace Application.UseCases.Patient.Create.Interfaces;
using Domain.Entities;

public interface ICreatePatientProcessingUseCase
{
    Task Execute(Patient patient, CancellationToken cancellationToken = default);
}
