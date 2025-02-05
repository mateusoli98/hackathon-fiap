namespace Application.UseCases.Appointment.Create;

using Application.UseCases.Appointment.Create.Interfaces;
using Domain.Repositories.Relational;
using Domain.Entities;

public class CreateAppointmentProcessingUseCase(IAppointmentRepository repository) : ICreateAppointmentProcessingUseCase
{
    private readonly IAppointmentRepository _appointmentRepository = repository;

    public async Task Execute(Appointments appointment, CancellationToken cancellationToken = default)
    {
        //Console.WriteLine("Validando se o paciente já existe");
        //var alreadyExists = await _appointmentRepository.Exists(appointment.CPF, cancellationToken);
        //if (!alreadyExists)
        //{
        //    Console.WriteLine($"Salvando paciente '{appointment.Id}'");
        //    await _appointmentRepository.SaveAsync(appointment, cancellationToken);
        //    return;
        //}

        //Console.WriteLine("Erro: paciente já existe no banco de dados");
        throw new Exception("Agendamento já cadastrado anteriormente no sistema.");
    }
}
