
namespace Application.UseCases.Assessment.Interfaces;

using Domain.Entities;

public interface IUpdateAssessmentProcessingUseCase
{
    Task Execute(Assessment assessment, CancellationToken cancellationToken = default);
}
