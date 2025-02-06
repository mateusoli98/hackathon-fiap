
using Application.UseCases.Patient.Get.Interfaces;
using Application.UseCases.Patient.Update.Common;
using Application.UseCases.Patient.Update.Interfaces;
using ErrorOr;
using Infra.Services.Messages;
using System.Text.Json;
using CrossCutting.Extensions;

namespace Application.UseCases.Patient.Update;

public class SendUpdatePatientRequestUseCase : ISendUpdatePatientRequestUseCase
{
    private readonly IRabbitMqProducerService _rabbitMqService;
    private readonly IGetPatientUseCase _getPatientUseCase;

    public SendUpdatePatientRequestUseCase(IRabbitMqProducerService rabbitMqProducerService, IGetPatientUseCase getPatientUseCase)
    {
        _rabbitMqService = rabbitMqProducerService;
        _getPatientUseCase = getPatientUseCase;
    }

    public async Task<ErrorOr<UpdatePatientResponse>> Execute(long patientId, UpdatePatientRequest request, CancellationToken cancellationToken = default)
    {
        var validationResult = new UpdatePatientRequestValidator().Validate(request);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrorList();
        }

        var patient = await _getPatientUseCase.Execute(patientId, cancellationToken);
        if (!patient.IsError)
        {
            patient.Value.Id = patientId;
            patient.Value.Name = request.Name;
            patient.Value.Email = request.Email;
            patient.Value.CPF = request.CPF;

            _rabbitMqService.SendMessage(JsonSerializer.Serialize(patient.Value), "update_doctor");

            return new UpdatePatientResponse
            {
                Message = $"Solicitação de alteração do paciente com Id {patientId} realizado com sucesso."
            };
        }

        return Error.Validation("NotFound", $"Paciente com id: {patientId} não encontrado. Revise o Id informado ou tente novamente mais tarde");
    }
}
