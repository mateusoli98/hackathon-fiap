
namespace Application.UseCases.Assessment.DeletePermanently.Interfaces;

public interface IDeleteAssessmentPermanentlyProcessingUseCase
{
    Task Execute(long id, CancellationToken cancellationToken = default); 
}
