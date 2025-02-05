using Application.UseCases.Assessment.Get.Common;
using Application.UseCases.Assessment.Get.Interfaces;
using Domain.Repositories.Relational;
using ErrorOr;

namespace Application.UseCases.Assessment.Get;

public class GetAssessmentUseCase : IGetAssessmentUseCase
{
    private readonly IAssessmentRepository _assessmentRepository;

    public GetAssessmentUseCase(IAssessmentRepository assessmentRepository)
    {
        _assessmentRepository = assessmentRepository;
    }

    public async Task<ErrorOr<GetAssessmentResponse>> Execute(long id, CancellationToken cancellationToken = default)
    {
        var assessment = await _assessmentRepository.GetByIdAsync(id, cancellationToken);

        if (assessment is not null)
        {
            return GetAssessmentResponse.Create(assessment);
        }

        return Error.NotFound(description: $"Avaliação com id: {id} não encontrada. Revise o Id informado ou tente novamente mais tarde");
    }
}
