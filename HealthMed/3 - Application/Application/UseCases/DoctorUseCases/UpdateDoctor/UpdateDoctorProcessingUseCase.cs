namespace Application.UseCases.UpdateDoctor;

using Application.UseCases.UpdateDoctor.Interfaces;
using Domain.Repositories.Relational;
using Domain.Entities;

public class UpdateDoctorProcessingUseCase(IDoctorRepository repository) : IUpdateDoctorProcessingUseCase
{
    private readonly IDoctorRepository _doctorRepository = repository;

    public async Task Execute(Doctor updatedDoctor, CancellationToken cancellationToken = default)
    {
        

        throw new Exception("");
    }
   
}
