namespace Application.UseCases.Assessment;

using Application.UseCases.Assessment.Interfaces;
using Domain.Repositories.Relational;
using Domain.Entities;

public class UpdateAssessmentProcessingUseCase(IAssessmentRepository repository) : IUpdateAssessmentProcessingUseCase
{
    private readonly IAssessmentRepository _assessmentRepository = repository;

    public async Task Execute(Assessment updatedAssessmentt, CancellationToken cancellationToken = default)
    {
        var assessmentt = await _assessmentRepository.GetByIdAsync(updatedAssessmentt.Id!, cancellationToken);

        if (assessmentt is null)
        {
            throw new Exception("Avaliação não encontrado.");
        }

        await _assessmentRepository.UpdateAsync(assessmentt, cancellationToken);
    }
}
