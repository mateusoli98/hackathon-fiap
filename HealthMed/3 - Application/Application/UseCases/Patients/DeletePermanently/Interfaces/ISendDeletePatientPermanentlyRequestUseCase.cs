
using ErrorOr;

namespace Application.UseCases.Patient.DeletePermanently.Interfaces;

public interface ISendDeleteAppointmentPermanentlyRequestUseCase
{
    Task<Error?> Execute(long id, CancellationToken cancellationToken = default);
}
