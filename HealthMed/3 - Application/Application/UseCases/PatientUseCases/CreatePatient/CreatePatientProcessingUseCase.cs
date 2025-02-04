namespace Application.UseCases.PatientUseCases.CreatePatient;

using Application.UseCases.PatientUseCases.CreatePatient.Interfaces;
using Domain.Repositories.Relational;
using Domain.Entities;

public class CreatePatientProcessingUseCase(IPatientRepository repository) : ICreatePatientProcessingUseCase
{
    private readonly IPatientRepository _patientRepository = repository;

    public async Task Execute(Patient patient, CancellationToken cancellationToken = default)
    {
        Console.WriteLine("Validando se o paciente já existe");
        var alreadyExists = await _patientRepository.Exists(patient.CPF, cancellationToken);
        if (!alreadyExists)
        {
            Console.WriteLine($"Salvando paciente '{patient.Id}'");
            await _patientRepository.SaveAsync(patient, cancellationToken);
            return;
        }

        Console.WriteLine("Erro: paciente já existe no banco de dados");
        throw new Exception("Paciente já cadastrado anteriormente no sistema.");
    }
}
