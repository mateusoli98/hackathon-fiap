namespace Application.UseCases.PatientUseCases.DeletePatientPermanently.Interfaces;

public interface IDeletePatientPermanentlyProcessingUseCase
{
    Task Execute(long id, CancellationToken cancellationToken = default);
}
