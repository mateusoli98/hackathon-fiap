namespace Application.UseCases.Assessment.Create.Interfaces;
using Domain.Entities;

public interface ICreateAssessmentProcessingUseCase
{
    Task Execute(Appointments appointments, CancellationToken cancellationToken = default);
}
