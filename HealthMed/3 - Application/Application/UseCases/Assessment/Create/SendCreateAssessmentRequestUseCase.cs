using Application.UseCases.Assessment.Create.Commom;
using Application.UseCases.Assessment.Create.Interfaces;
using Application.UseCases.Doctor.Get.Interfaces;
using Application.UseCases.Patient.Get.Interfaces;
using CrossCutting.Extensions;
using ErrorOr;
using Infra.Services.Messages;
using System.Text.Json;

namespace Application.UseCases.Assessment.Create;

using Application.UseCases.Doctor.Get.Common;
using Application.UseCases.Patient.Get.Common;
using Domain.Entities;
using Domain.Repositories.Relational;

public class SendCreateAssessmentRequestUseCase : ISendCreateAssessmentRequestUseCase
{
    private readonly IRabbitMqProducerService _rabbitMqService;
    private readonly IGetDoctorUseCase _getDoctorUseCase;
    private readonly IGetPatientUseCase _getPatientUseCase;
    private readonly IAssessmentRepository _assessmentRepository;

    public SendCreateAssessmentRequestUseCase(
        IRabbitMqProducerService rabbitMqProducerService,
        IGetDoctorUseCase getDoctorUseCase,
        IGetPatientUseCase getPatientUseCase,
        IAssessmentRepository assessmentRepository)
    {
        _rabbitMqService = rabbitMqProducerService;
        _getDoctorUseCase = getDoctorUseCase;
        _getPatientUseCase = getPatientUseCase;
        _assessmentRepository = assessmentRepository;
    }

    public async Task<ErrorOr<CreateAssessmentResponse>> Execute(CreateAssessmentRequest request, CancellationToken cancellationToken = default)
    {
        var validationResult = new CreateAssessmentRequestValidator().Validate(request);
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

        var alreadyExists = await _assessmentRepository.Exists(request.DoctorId, request.PatientId, cancellationToken);
        if (!alreadyExists)
        {
            var assessment = new Assessment()
            {
                Rating = request.Rating,
                Description = request.Description,
                Doctor = GetDoctorResponse.GetDoctor( doctor.Value),
                Patient = GetPatientResponse.GetPatient( patient.Value),
                IsEnabled = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            _rabbitMqService.SendMessage(JsonSerializer.Serialize(assessment), "create_assessment");

            return new CreateAssessmentResponse()
            {
                Id = assessment.Id.ToString(),
                Message = $"Solicitação de criação de avaliação com id {assessment.Id} enviada com sucesso"
            };
        }

        return Error.Validation("Validation", "Avaliação informada já está cadastrada.");
    }
}
