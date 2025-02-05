using Application.UseCases.Appointment.DeletePermanently.Interfaces;
using Domain.Repositories.Relational;

namespace Application.UseCases.Appointment.DeletePermanently;

public class DeleteAppointmentPermanentlyProcessingUseCase : IDeleteAppointmentPermanentlyProcessingUseCase
{
    private readonly IAppointmentRepository _appointmentRepository;

    public DeleteAppointmentPermanentlyProcessingUseCase(IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    public async Task Execute(long id, CancellationToken cancellationToken = default)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(id, cancellationToken);

        if (appointment is not null)
        {
            await _appointmentRepository.PermanentDelete(appointment, cancellationToken);
            return;
        }

        throw new Exception("Agendamento não encontrado.");
    }
}
