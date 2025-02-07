namespace Application.UseCases.Assessment.Create.Interfaces;
using Domain.Entities;

public interface ICreateAssessmentProcessingUseCase
{
    Task Execute(Assessment appointments, CancellationToken cancellationToken = default);
}
