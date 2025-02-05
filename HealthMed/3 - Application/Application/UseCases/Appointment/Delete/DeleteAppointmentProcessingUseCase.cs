using Application.UseCases.Appointment.Delete.Interfaces;
using Domain.Repositories.Relational;
using ErrorOr;

namespace Application.UseCases.Appointment.Delete;

public class DeleteAppointmentProcessingUseCase : IDeleteAppointmentProcessingUseCase
{
    readonly IAppointmentRepository _appointmentRepository;

    public DeleteAppointmentProcessingUseCase(IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    public async Task<Error?> Execute(long id, CancellationToken cancellationToken = default)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(id, cancellationToken);

        if (appointment is not null)
        {
            await _appointmentRepository.DeleteAsync(appointment, cancellationToken);
            return null;
        }

        return Error.NotFound();
    }
}
