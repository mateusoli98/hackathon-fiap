namespace Application.UseCases.DoctorUseCases.CreateDoctor.Interfaces;
using Domain.Entities;

public interface ICreateDoctorProcessingUseCase
{
    Task Execute(Doctor doctor, CancellationToken cancellationToken = default);
}
