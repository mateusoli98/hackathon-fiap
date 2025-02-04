
using ErrorOr;

namespace Application.UseCases.DoctorUseCases.DeleteDoctorPermanently.Interfaces;

public interface ISendDeleteDoctorPermanentlyRequestUseCase
{
    Task<Error?> Execute(long id, CancellationToken cancellationToken = default);
}
