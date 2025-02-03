using ErrorOr;

namespace Application.UseCases.DoctorUseCases.DeleteDoctor.Interfaces;

public interface IDeleteDoctorProcessingUseCase
{
    Task<Error?> Execute(long id, CancellationToken cancellationToken = default);
}
