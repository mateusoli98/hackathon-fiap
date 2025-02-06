using Application.Appointment.Update.Common;
using Application.UseCases.Appointment.Common;
using Application.UseCases.Appointment.Get.Interfaces;
using Application.UseCases.Appointment.Interfaces;
using Application.UseCases.Doctor.Get.Common;
using Application.UseCases.Doctor.Get.Interfaces;
using Application.UseCases.Patient.Get.Common;
using Application.UseCases.Patient.Get.Interfaces;
using CrossCutting.Extensions;
using ErrorOr;
using Infra.Services.Messages;
using System.Text.Json;


namespace Application.UseCases.Appointment;

public class SendUpdateAppointmentRequestUseCase : ISendUpdateAppointmentRequestUseCase
{
    private readonly IRabbitMqProducerService _rabbitMqService;
    private readonly IGetAppointmentUseCase _getAppointmentUseCase;
    private readonly IGetDoctorUseCase _getDoctorUseCase;
    private readonly IGetPatientUseCase _getPatientUseCase;

    public SendUpdateAppointmentRequestUseCase(
        IRabbitMqProducerService rabbitMqProducerService,
        IGetAppointmentUseCase getAppointmentUseCase,
        IGetDoctorUseCase getDoctorUseCase,
        IGetPatientUseCase getPatientUseCase)
    {
        _rabbitMqService = rabbitMqProducerService;
        _getAppointmentUseCase = getAppointmentUseCase;
        _getDoctorUseCase = getDoctorUseCase;
        _getPatientUseCase = getPatientUseCase;
    }

    public async Task<ErrorOr<UpdateAppointmentResponse>> Execute(long appointmentId, UpdateAppointmentRequest request, CancellationToken cancellationToken = default)
    {
        var validationResult = new UpdateAppointmentRequestValidator().Validate(request);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrorList();
        }

        var doctor = await _getDoctorUseCase.Execute(request.DoctorId, cancellationToken);
        if (doctor.IsError)
        {
            return Error.Validation("NotFound", $"Médico com id: {request.DoctorId} não encontrado. Revise o Id informado ou tente novamente mais tarde");
        }

        var patient = await _getPatientUseCase.Execute(request.PatientId, cancellationToken);
        if (patient.IsError)
        {
            return Error.Validation("NotFound", $"Paciente com id: {request.PatientId} não encontrado. Revise o Id informado ou tente novamente mais tarde");
        }

        var appointment = await _getAppointmentUseCase.Execute(appointmentId, cancellationToken);
        if (!appointment.IsError)
        {
            appointment.Value.Id = appointmentId;
            appointment.Value.Patient = GetPatientResponse.GetPatient(patient.Value);
            appointment.Value.Doctor = GetDoctorResponse.GetDoctor(doctor.Value);
            appointment.Value.AppointmentDate = request.AppointmentDate;
            appointment.Value.Status = request.Status;

            _rabbitMqService.SendMessage(JsonSerializer.Serialize(appointment.Value), "update_appointment");

            return new UpdateAppointmentResponse
            {
                Message = $"Solicitação de alteração do agendamento com Id {appointmentId} realizado com sucesso."
            };
        }

        return Error.Validation("NotFound", $"Agendamento com id: {appointmentId} não encontrado. Revise o Id informado ou tente novamente mais tarde");
    }
}