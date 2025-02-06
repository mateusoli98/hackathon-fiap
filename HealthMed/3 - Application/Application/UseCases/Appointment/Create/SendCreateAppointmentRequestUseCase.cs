namespace Application.UseCases.Appointment.Create;


using Application.UseCases.Appointment.Create.Commom;
using Application.UseCases.Appointment.Create.Interfaces;
using CrossCutting.Extensions;
using Domain.Entities;
using Domain.Repositories.Relational;
using ErrorOr;
using Infra.Services.Messages;
using System.Text.Json;

public class SendCreateAppointmentRequestUseCase : ISendCreateAppointmentRequestUseCase
{

    private readonly IRabbitMqProducerService _rabbitMqService;
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IDoctorRepository _doctorRepository;
    private readonly IPatientRepository _patientRepository;

    public SendCreateAppointmentRequestUseCase(
        IRabbitMqProducerService rabbitMqProducerService,
        IAppointmentRepository appointmentRepository,
        IDoctorRepository doctorRepository,
        IPatientRepository patientRepository)
    {
        _rabbitMqService = rabbitMqProducerService;
        _appointmentRepository = appointmentRepository;
        _doctorRepository = doctorRepository;
        _patientRepository = patientRepository;
    }

    public async Task<ErrorOr<CreateAppointmentResponse>> Execute(CreateAppointmentRequest request, CancellationToken cancellationToken = default)
    {
        var validationResult = new CreateAppointmentRequestValidator().Validate(request);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrorList();
        }

        var doctor = await _doctorRepository.GetByIdAsync(request.DoctorId, cancellationToken);
        if (doctor is null)
        {
            return Error.Validation("Validation", "Médico informado não está cadastrado.");
        }

        var patient = await _patientRepository.GetByIdAsync(request.PatientId, cancellationToken);
        if (patient is null)
        {
            return Error.Validation("Validation", "Paciente informado não está cadastrado.");
        }

        var alreadyExists = await _appointmentRepository.Exists(request.DoctorId, request.PatientId, request.AppointmentDate, cancellationToken);
        if (!alreadyExists)
        {
            var appointment = new Appointments()
            {
                Doctor = doctor,
                Patient = patient,
                AppointmentDate = request.AppointmentDate,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            _rabbitMqService.SendMessage(JsonSerializer.Serialize(appointment), "create_appointment");

            return new CreateAppointmentResponse()
            {
                Id = appointment.Id.ToString(),
                Message = $"Solicitação de criação do agendamento com id {appointment.Id} enviada com sucesso"
            };
        }

        return Error.Validation("Validation", "Agendamento informado já está cadastrado.");
    }
}
