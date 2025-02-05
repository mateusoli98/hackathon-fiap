using Application.UseCases.Assessment.Delete.Interfaces;
using Domain.Repositories.Relational;
using ErrorOr;

namespace Application.UseCases.Assessment.Delete;

public class DeleteAssessmentProcessingUseCase : IDeleteAssessmentProcessingUseCase
{
    readonly IAssessmentRepository _assessmentRepository;

    public DeleteAssessmentProcessingUseCase(IAssessmentRepository assessmentRepository)
    {
        _assessmentRepository = assessmentRepository;
    }

    public async Task<Error?> Execute(long id, CancellationToken cancellationToken = default)
    {
        var appointment = await _assessmentRepository.GetByIdAsync(id, cancellationToken);

        if (appointment is not null)
        {
            await _assessmentRepository.DeleteAsync(appointment, cancellationToken);
            return null;
        }

        return Error.NotFound();
    }
}
