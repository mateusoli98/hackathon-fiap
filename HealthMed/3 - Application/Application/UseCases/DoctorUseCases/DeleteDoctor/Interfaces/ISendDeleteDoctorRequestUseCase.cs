using ErrorOr;

namespace Application.UseCases.DoctorUseCases.DeleteDoctor.Interfaces;

public interface ISendDeleteDoctorRequestUseCase
{
    Task<Error?> Execute(long id, CancellationToken cancellationToken = default);
}
