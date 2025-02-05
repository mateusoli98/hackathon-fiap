using Application.UseCases.Assessment.DeletePermanently.Interfaces;
using Domain.Repositories.Relational;

namespace Application.UseCases.Assessment.DeletePermanently;

public class DeleteAssessmentPermanentlyProcessingUseCase : IDeleteAssessmentPermanentlyProcessingUseCase
{
    private readonly IAssessmentRepository _assessmentRepository;

    public DeleteAssessmentPermanentlyProcessingUseCase(IAssessmentRepository assessmentRepository)
    {
        _assessmentRepository = assessmentRepository;
    }

    public async Task Execute(long id, CancellationToken cancellationToken = default)
    {
        var appointment = await _assessmentRepository.GetByIdAsync(id, cancellationToken);

        if (appointment is not null)
        {
            await _assessmentRepository.PermanentDelete(appointment, cancellationToken);
            return;
        }

        throw new Exception("Agendamento não encontrado.");
    }
}
