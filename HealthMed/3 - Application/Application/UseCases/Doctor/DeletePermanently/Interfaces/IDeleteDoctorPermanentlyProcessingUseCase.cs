namespace Application.UseCases.Doctor.DeletePermanently.Interfaces;

public interface IDeleteDoctorPermanentlyProcessingUseCase
{
    Task Execute(long id, CancellationToken cancellationToken = default);
}
