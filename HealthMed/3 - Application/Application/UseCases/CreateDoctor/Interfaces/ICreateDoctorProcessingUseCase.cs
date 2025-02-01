using Domain.Entities;

namespace Application.UseCases.CreateContact.Interfaces;

public interface ICreateDoctorProcessingUseCase
{
    Task Execute(Doctor contact, CancellationToken cancellationToken = default);
}
