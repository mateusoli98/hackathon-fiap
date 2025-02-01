using Domain.Entities;

namespace Application.UseCases.Doctor.CreateDoctor.Interfaces;

public interface ICreateDoctorProcessingUseCase
{
    Task Execute(Doctor contact, CancellationToken cancellationToken = default);
}
