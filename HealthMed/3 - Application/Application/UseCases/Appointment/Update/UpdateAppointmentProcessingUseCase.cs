namespace Application.UseCases.Appointment;

using Application.UseCases.Appointment.Interfaces;
using Domain.Repositories.Relational;
using Domain.Entities;

public class UpdateAppointmentProcessingUseCase(IAppointmentRepository repository) : IUpdateAppointmentProcessingUseCase
{
    private readonly IAppointmentRepository _appointmentRepository = repository;

    public async Task Execute(Appointments updatedAppointment, CancellationToken cancellationToken = default)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(updatedAppointment.Id!, cancellationToken);

        if (appointment is null)
        {
            throw new Exception("Agendamento não encontrado.");
        }

        await _appointmentRepository.UpdateAsync(appointment, cancellationToken);
    }
}
