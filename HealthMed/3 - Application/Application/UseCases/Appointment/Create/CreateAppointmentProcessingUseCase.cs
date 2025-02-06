namespace Application.UseCases.Appointment.Create;

using Application.UseCases.Appointment.Create.Interfaces;
using Domain.Repositories.Relational;
using Domain.Entities;

public class CreateAppointmentProcessingUseCase(IAppointmentRepository repository) : ICreateAppointmentProcessingUseCase
{
    private readonly IAppointmentRepository _appointmentRepository = repository;

    public async Task Execute(Appointments appointment, CancellationToken cancellationToken = default)
    {
        Console.WriteLine("Validando se o agendamento já existe");
        var alreadyExists = await _appointmentRepository.Exists(appointment.Doctor.Id,appointment.Patient.Id, appointment.AppointmentDate, cancellationToken);
        if (!alreadyExists)
        {
            Console.WriteLine($"Salvando agendamento '{appointment.Id}'");
            await _appointmentRepository.SaveAsync(appointment, cancellationToken);
            return;
        }

        throw new Exception($"Erro: o agendamento do paciente {appointment.Patient.Name} com o médico(a) {appointment.Doctor.Name} já existe no banco de dados");
    }
}
