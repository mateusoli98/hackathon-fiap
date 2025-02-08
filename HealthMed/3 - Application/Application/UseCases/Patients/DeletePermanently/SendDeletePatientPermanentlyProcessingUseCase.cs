using Application.UseCases.Doctor.Get.Interfaces;
using Application.UseCases.Patient.DeletePermanently.Interfaces;
using ErrorOr;
using Infra.Services.Messages;
using System.Text.Json;

namespace Application.UseCases.Patient.DeletePermanently;

public class SendDeletePatientPermanentlyProcessingUseCase : ISendDeletePatientPermanentlyProcessingUseCase
{
    private readonly IRabbitMqProducerService _rabbitMqService;
    private readonly IGetDoctorUseCase _getDoctorUseCase;

    public SendDeletePatientPermanentlyProcessingUseCase(IRabbitMqProducerService rabbitMqProducerService, IGetDoctorUseCase getDoctorUseCase)
    {
        _rabbitMqService = rabbitMqProducerService;
        _getDoctorUseCase = getDoctorUseCase;
    }

    public async Task<Error?> Execute(long id, CancellationToken cancellationToken = default)
    {
        var patient = await _getDoctorUseCase.Execute(id, cancellationToken);

        if (!patient.IsError)
        {
            _rabbitMqService.SendMessage(JsonSerializer.Serialize(patient.Value.Id), "delete_permanently_patient");
            return null;
        }

        return Error.Validation("NotFound", $"Paciente com Id {id} não encontrado. Revise o Id informado ou tente novamente mais tarde");
    }
}
