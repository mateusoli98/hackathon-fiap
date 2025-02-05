namespace Application.UseCases.Appointment.DeletePermanently.Interfaces;

public interface IDeleteAppointmentPermanentlyProcessingUseCase
{
    Task Execute(long id, CancellationToken cancellationToken = default);
}
