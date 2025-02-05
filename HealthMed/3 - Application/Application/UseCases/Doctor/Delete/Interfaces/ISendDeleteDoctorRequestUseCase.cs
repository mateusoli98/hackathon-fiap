using ErrorOr;

namespace Application.UseCases.Doctor.Delete.Interfaces;

public interface ISendDeleteDoctorRequestUseCase
{
    Task<Error?> Execute(long id, CancellationToken cancellationToken = default);
}
