namespace Application.UseCases.PatientUseCases.CreatePatient.Interfaces;
using Domain.Entities;

public interface ICreatePatientProcessingUseCase
{
    Task Execute(Patient patient, CancellationToken cancellationToken = default);
}
