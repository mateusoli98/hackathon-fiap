using Application.UseCases.Doctor.DeletePermanently.Interfaces;
using Application.UseCases.Doctor.Get.Interfaces;
using ErrorOr;
using Infra.Services.Messages;
using System.Text.Json;

namespace Application.UseCases.Doctor.DeletePermanently;

public class SendDeleteDoctorPermanentlyProcessingUseCase(IRabbitMqProducerService rabbitMqProducerService, IGetDoctorUseCase getDoctorUseCase) : ISendDeleteDoctorPermanentlyRequestUseCase
{
    private readonly IRabbitMqProducerService _rabbitMqService = rabbitMqProducerService;
    private readonly IGetDoctorUseCase _getDoctorUseCase = getDoctorUseCase;

    public async Task<Error?> Execute(long id, CancellationToken cancellationToken = default)
    {
        var doctor = await _getDoctorUseCase.Execute(id, cancellationToken);

        if (!doctor.IsError)
        {
            _rabbitMqService.SendMessage(JsonSerializer.Serialize(doctor.Value.Id), "delete_permanently_doctor");
            return null;
        }

        return Error.Validation("NotFound", $"Médico com Id {id} não encontrado. Revise o Id informado ou tente novamente mais tarde");
    }
}
