
namespace Application.UseCases.Doctor.Update.Interfaces;

using Domain.Entities;

public interface IUpdateDoctorProcessingUseCase
{
    Task Execute(Doctor doctor, CancellationToken cancellationToken = default);
}
