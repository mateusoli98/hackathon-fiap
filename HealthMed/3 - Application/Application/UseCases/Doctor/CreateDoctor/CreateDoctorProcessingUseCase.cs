using Application.UseCases.Doctor.CreateDoctor.Interfaces;
using Domain.Entities;
using Domain.Repositories.Relational;

namespace Application.UseCases.Doctor.CreateDoctor
{
    public class CreateDoctorProcessingUseCase(IDoctorRepository repository) : ICreateDoctorProcessingUseCase
    {
        private readonly IDoctorRepository _doctorRepository = repository;

        public async Task Execute(Doctor contact, CancellationToken cancellationToken = default)
        {
            Console.WriteLine("Validando se o médico já existe");
            var alreadyExists = await _doctorRepository.Exists(contact.CRM, cancellationToken);
            if (!alreadyExists)
            {
                Console.WriteLine($"Salvando médico '{contact.Id}'");
                await _doctorRepository.SaveAsync(contact, cancellationToken);
                return;
            }

            Console.WriteLine("Erro: médico já existe no banco de dados");
            throw new Exception("Médico já cadastrado anteriormente no sistema.");
        }
    }
}
