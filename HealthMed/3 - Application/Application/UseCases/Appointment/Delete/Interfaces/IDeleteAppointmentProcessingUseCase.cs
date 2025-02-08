using Domain.Entities;
using ErrorOr;

namespace Application.UseCases.Appointment.Delete.Interfaces;

public interface IDeleteAppointmentProcessingUseCase
{
    Task<Error?> Execute(long id, CancellationToken cancellationToken = default);
}
