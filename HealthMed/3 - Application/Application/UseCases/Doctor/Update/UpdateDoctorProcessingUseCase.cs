namespace Application.UseCases.UpdateDoctor;

using Application.UseCases.Doctor.Update.Interfaces;
using Domain.Repositories.Relational;
using Domain.Entities;

public class UpdateDoctorProcessingUseCase(IDoctorRepository repository) : IUpdateDoctorProcessingUseCase
{
    private readonly IDoctorRepository _doctorRepository = repository;

    public async Task Execute(Doctor updatedDoctor, CancellationToken cancellationToken = default)
    {
        var doctor = await _doctorRepository.GetByIdAsync(updatedDoctor.Id!, cancellationToken);

        if (doctor is null)
        {
            throw new Exception("Médico não encontrado."); ;
        }

        var alreadyExists = await Validate(doctor, updatedDoctor, cancellationToken);
        if (!alreadyExists)
        {
            doctor.Name = updatedDoctor.Name;
            doctor.Email = updatedDoctor.Email;  
            doctor.CRM = updatedDoctor.CRM;

            await _doctorRepository.UpdateAsync(doctor, cancellationToken);

            return;
        }

        throw new Exception("CRM informado já está cadastrado no sistema.");
    }

    private async Task<bool> Validate(Doctor findedDoctor, Doctor updatedDoctor, CancellationToken cancellationToken)
    {
        if (updatedDoctor.CRM != findedDoctor.CRM)
        {
            var alreadyExists = await _doctorRepository.Exists(updatedDoctor.CRM, cancellationToken);

            return alreadyExists;
        }

        return true;
    }

}
