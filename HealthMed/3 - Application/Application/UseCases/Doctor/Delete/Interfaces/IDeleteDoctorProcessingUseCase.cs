using ErrorOr;

namespace Application.UseCases.Doctor.Delete.Interfaces;

public interface IDeleteDoctorProcessingUseCase
{
    Task<Error?> Execute(long id, CancellationToken cancellationToken = default);
}
