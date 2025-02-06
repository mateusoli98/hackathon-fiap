namespace Application.UseCases.Patient.DeletePermanently.Interfaces;

public interface IDeletePatientPermanentlyProcessingUseCase
{
    Task Execute(long id, CancellationToken cancellationToken = default);
}
