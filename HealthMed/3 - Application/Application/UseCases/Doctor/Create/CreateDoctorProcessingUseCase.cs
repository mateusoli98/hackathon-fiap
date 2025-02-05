namespace Application.UseCases.Doctor.Create;

using Application.UseCases.Doctor.Create.Interfaces;
using Domain.Repositories.Relational;
using Domain.Entities;

public class CreateDoctorProcessingUseCase(IDoctorRepository repository) : ICreateDoctorProcessingUseCase
{
    private readonly IDoctorRepository _doctorRepository = repository;

    public async Task Execute(Doctor doctor, CancellationToken cancellationToken = default)
    {
        Console.WriteLine("Validando se o médico já existe");
        var alreadyExists = await _doctorRepository.Exists(doctor.CRM, cancellationToken);
        if (!alreadyExists)
        {
            Console.WriteLine($"Salvando médico '{doctor.Id}'");
            await _doctorRepository.SaveAsync(doctor, cancellationToken);
            return;
        }

        Console.WriteLine("Erro: médico já existe no banco de dados");
        throw new Exception("Médico já cadastrado anteriormente no sistema.");
    }
}
