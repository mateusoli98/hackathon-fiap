namespace Application.UseCases.Doctor.Create.Interfaces;
using Domain.Entities;

public interface ICreateDoctorProcessingUseCase
{
    Task Execute(Doctor doctor, CancellationToken cancellationToken = default);
}
