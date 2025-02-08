namespace Application.UseCases.Appointment.Create;

using Application.UseCases.Appointment.Create.Interfaces;
using Domain.Repositories.Relational;
using Domain.Entities;
using Application.UseCases.Appointment.Create.Commom;
using Domain.DomainObjects.Enums;
using ErrorOr;

public class CreateAppointmentProcessingUseCase(

    IAppointmentRepository repository,
    IDoctorRepository doctorRepository,
    IPatientRepository patientRepository
    ) : ICreateAppointmentProcessingUseCase
{
    private readonly IAppointmentRepository _appointmentRepository = repository;
    private readonly IDoctorRepository _doctorRepository = doctorRepository;
    private readonly IPatientRepository _patientRepository = patientRepository;

    public async Task<ErrorOr<CreateAppointmentResponse>> Execute(CreateAppointmentRequest request, CancellationToken cancellationToken = default)
    {
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
            var appointment = new Appointments
            {
                Status = AppointmentStatus.Pending,
                AppointmentDate = request.AppointmentDate,
                IsEnabled = true,
                Doctor = doctor,
                Patient = patient,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            appointment.Id = await _appointmentRepository.SaveAsync(appointment, cancellationToken); 

            return new CreateAppointmentResponse
            {
                Id = appointment.Id.ToString(),
                Message = $"Agendamento com id {appointment.Id} criada com sucesso"
            };
        }

        throw new Exception($"Erro: o agendamento do paciente {patient.Name} com o médico(a) {doctor.Name} já existe no banco de dados");
    }
}
