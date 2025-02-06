
using Application.Assessment.Update.Common;
using Application.UseCases.Assessment.Common;
using Application.UseCases.Assessment.Get.Interfaces;
using Application.UseCases.Assessment.Interfaces;
using Application.UseCases.Doctor.Get.Interfaces;
using Application.UseCases.Patient.Get.Interfaces;
using CrossCutting.Extensions;
using ErrorOr;
using Infra.Services.Messages;
using System.Text.Json;


namespace Application.UseCases.Assessment;

public class SendUpdateAssessmentRequestUseCase : ISendUpdateAssessmentRequestUseCase
{
    private readonly IRabbitMqProducerService _rabbitMqService;
    private readonly IGetAssessmentUseCase _getAssessmentUseCase;
    private readonly IGetDoctorUseCase _getDoctorUseCase;
    private readonly IGetPatientUseCase _getPatientUseCase;

    public SendUpdateAssessmentRequestUseCase(
        IRabbitMqProducerService rabbitMqProducerService,
        IGetAssessmentUseCase getAssessmentUseCase,
        IGetDoctorUseCase getDoctorUseCase,
        IGetPatientUseCase getPatientUseCase)
    {
        _rabbitMqService = rabbitMqProducerService;
        _getAssessmentUseCase = getAssessmentUseCase;
        _getDoctorUseCase = getDoctorUseCase;
        _getPatientUseCase = getPatientUseCase;
    }

    public async Task<ErrorOr<UpdateAssessmentResponse>> Execute(long assessmentId, UpdateAssessmentRequest request, CancellationToken cancellationToken = default)
    {
        var validationResult = new UpdateAssessmentRequestValidator().Validate(request);
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

        var assessment = await _getAssessmentUseCase.Execute(assessmentId, cancellationToken);
        if (!assessment.IsError)
        {
            assessment.Value.Id = assessmentId;
            assessment.Value.Rating = request.Rating;
            assessment.Value.Description = request.Description;
            assessment.Value.IsEnable = request.IsEnabled;

            _rabbitMqService.SendMessage(JsonSerializer.Serialize(assessment.Value), "update_assessment");

            return new UpdateAssessmentResponse
            {
                Message = $"Solicitação de alteração da avaliação com Id {assessmentId} realizado com sucesso."
            };
        }

        return Error.Validation("NotFound", $"Avaliação com id: {assessmentId} não encontrada. Revise o Id informado ou tente novamente mais tarde");
    }
}
