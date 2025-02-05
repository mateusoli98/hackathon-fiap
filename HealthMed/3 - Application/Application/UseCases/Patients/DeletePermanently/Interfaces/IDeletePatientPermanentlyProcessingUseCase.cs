namespace Application.UseCases.Patient.DeletePermanently.Interfaces;

public interface IDeleteAppointmentPermanentlyProcessingUseCase
{
    Task Execute(long id, CancellationToken cancellationToken = default);
}
