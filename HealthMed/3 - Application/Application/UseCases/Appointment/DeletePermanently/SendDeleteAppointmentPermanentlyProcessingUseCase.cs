using Application.UseCases.Appointment.DeletePermanently.Interfaces;
using Application.UseCases.Appointment.Get.Interfaces;
using ErrorOr;
using Infra.Services.Messages;
using System.Text.Json;

namespace Application.UseCases.Appointment.Delete;

public class SendDeleteAppointmentPermanentlyProcessingUseCase : ISendDeleteAppointmentPermanentlyRequestUseCase
{
    private readonly IRabbitMqProducerService _rabbitMqService;
    private readonly IGetAppointmentUseCase _getAppointmentUseCase;

    public SendDeleteAppointmentPermanentlyProcessingUseCase(IRabbitMqProducerService rabbitMqProducerService, IGetAppointmentUseCase getAppoitmentUseCase)
    {
        _rabbitMqService = rabbitMqProducerService;
        _getAppointmentUseCase = getAppoitmentUseCase;
    }

    public async Task<Error?> Execute(long id, CancellationToken cancellationToken = default)
    {
        var appointment = await _getAppointmentUseCase.Execute(id, cancellationToken);

        if (!appointment.IsError)
        {
            _rabbitMqService.SendMessage(JsonSerializer.Serialize(appointment.Value.Id), "delete_permanently_appointment");
            return null;
        }

        return Error.Validation("NotFound", $"Agendamento com Id {id} não encontrado. Revise o Id informado ou tente novamente mais tarde");
    }
}
