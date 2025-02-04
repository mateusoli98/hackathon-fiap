namespace Application.UseCases.DoctorUseCases.DeleteDoctorPermanently.Interfaces;

public interface IDeleteDoctorPermanentlyProcessingUseCase
{
    Task Execute(long id, CancellationToken cancellationToken = default);
}
