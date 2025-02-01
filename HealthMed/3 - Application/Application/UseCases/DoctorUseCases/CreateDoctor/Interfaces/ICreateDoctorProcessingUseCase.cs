namespace Application.UseCases.DoctorUseCases.CreateDoctor.Interfaces;
using Domain.Entities;

public interface ICreateDoctorProcessingUseCase
{
    Task Execute(Doctor contact, CancellationToken cancellationToken = default);
}
