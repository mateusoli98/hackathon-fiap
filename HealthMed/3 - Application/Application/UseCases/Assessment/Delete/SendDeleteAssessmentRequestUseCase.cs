using Application.UseCases.Assessment.Delete.Interfaces;
using Application.UseCases.Assessment.Get.Interfaces;
using ErrorOr;
using Infra.Services.Messages;
using System.Text.Json;

namespace Application.UseCases.Assessment.Delete;

public class SendDeleteAssessmentRequestUseCase : ISendDeleteAssessmentRequestUseCase
{
    private readonly IRabbitMqProducerService _rabbitMqService;
    private readonly IGetAssessmentUseCase _getAssessmentUseCase;

    public SendDeleteAssessmentRequestUseCase(IRabbitMqProducerService rabbitMqProducerService, IGetAssessmentUseCase getAssessmentUseCase)
    {
        _rabbitMqService = rabbitMqProducerService;
        _getAssessmentUseCase = getAssessmentUseCase;
    }

    public async Task<Error?> Execute(long id, CancellationToken cancellationToken = default)
    {
        var assessment = await _getAssessmentUseCase.Execute(id, cancellationToken);

        if (!assessment.IsError)
        {
            _rabbitMqService.SendMessage(JsonSerializer.Serialize(assessment.Value.Id), "delete_assessment");
            return null;
        }

        return Error.Validation("NotFound", $"Avaliação com Id {id} não encontrado. Revise o Id informado ou tente novamente mais tarde");
    }
}
