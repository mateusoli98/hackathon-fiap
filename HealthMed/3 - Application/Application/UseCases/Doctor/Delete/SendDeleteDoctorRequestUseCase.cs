using Application.UseCases.Doctor.Delete.Interfaces;
using Application.UseCases.Doctor.Get.Interfaces;
using ErrorOr;
using Infra.Services.Messages;
using System.Text.Json;

namespace Application.UseCases.Doctor.Delete;

public class SendDeleteDoctorRequestUseCase : ISendDeleteDoctorRequestUseCase
{
    private readonly IRabbitMqProducerService _rabbitMqService;
    private readonly IGetDoctorUseCase _getDoctorUseCase;

    public SendDeleteDoctorRequestUseCase(IRabbitMqProducerService rabbitMqProducerService, IGetDoctorUseCase getDoctorUseCase)
    {
        _rabbitMqService = rabbitMqProducerService;
        _getDoctorUseCase = getDoctorUseCase;
    }


    public async Task<Error?> Execute(long id, CancellationToken cancellationToken = default)
    {
        var doctor = await _getDoctorUseCase.Execute(id, cancellationToken);

        if (!doctor.IsError)
        {
            _rabbitMqService.SendMessage(JsonSerializer.Serialize(doctor.Value.Id), "delete_doctor");
            return null;
        }

        return Error.Validation("NotFound", $"Médico com Id {id} não encontrado. Revise o Id informado ou tente novamente mais tarde");
    }
}
