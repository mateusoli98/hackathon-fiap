
using Application.UseCases.Doctor.Get.Interfaces;
using Application.UseCases.Doctor.Update.Common;
using Application.UseCases.Doctor.Update.Interfaces;
using ErrorOr;
using Infra.Services.Messages;
using System.Text.Json;
using CrossCutting.Extensions;

namespace Application.UseCases.Doctor.Update;

public class SendUpdateDoctorRequestUseCase : ISendUpdateDoctorRequestUseCase
{
    private readonly IRabbitMqProducerService _rabbitMqService;
    private readonly IGetDoctorUseCase _getDoctorUseCase;

    public SendUpdateDoctorRequestUseCase(IRabbitMqProducerService rabbitMqProducerService, IGetDoctorUseCase getDoctorUseCase)
    {
        _rabbitMqService = rabbitMqProducerService;
        _getDoctorUseCase = getDoctorUseCase;
    }
    public async Task<ErrorOr<UpdateDoctorResponse>> Execute(long doctorId, UpdateDoctorRequest request, CancellationToken cancellationToken = default)
    {
        var validationResult = new UpdateDoctorRequestValidator().Validate(request);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrorList();
        }

        var doctor = await _getDoctorUseCase.Execute(doctorId, cancellationToken);
        if (!doctor.IsError)
        {
            doctor.Value.Id = doctorId;
            doctor.Value.Name = request.Name;
            doctor.Value.Email = request.Email;
            doctor.Value.Specialty = request.Specialty;
            doctor.Value.CRM = request.CRM;

            _rabbitMqService.SendMessage(JsonSerializer.Serialize(doctor.Value), "update_doctor");

            return new UpdateDoctorResponse
            {
                Message = $"Solicitação de alteração do médico com Id {doctorId} realizado com sucesso."
            };
        }

        return Error.Validation("NotFound", $"Médico com id: {doctorId} não encontrado. Revise o Id informado ou tente novamente mais tarde");

    }
}
