namespace Application.UseCases.AppoAssessmentintment.Create;

using Application.UseCases.Assessment.Create.Interfaces;
using Domain.Repositories.Relational;
using Domain.Entities;

public class CreateAppointmentProcessingUseCase(IAppointmentRepository repository) : ICreateAssessmentProcessingUseCase
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
        throw new Exception("Avaliação já cadastrado anteriormente no sistema.");
    }
}
