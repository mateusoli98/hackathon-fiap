
using ErrorOr;

namespace Application.UseCases.Doctor.DeletePermanently.Interfaces;

public interface ISendDeleteDoctorPermanentlyRequestUseCase
{
    Task<Error?> Execute(long id, CancellationToken cancellationToken = default);
}
