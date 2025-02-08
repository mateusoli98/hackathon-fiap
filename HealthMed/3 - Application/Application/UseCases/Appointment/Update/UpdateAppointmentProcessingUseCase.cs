using Application.Appointment.Update.Common;
using Application.UseCases.Appointment.Common;
using Application.UseCases.Appointment.Interfaces;
using Domain.Repositories.Relational;
using CrossCutting.Extensions;
using ErrorOr;

namespace Application.UseCases.Appointment;

public class UpdateAppointmentProcessingUseCase(
    IAppointmentRepository repository,
    IDoctorRepository doctorRepository,
    IPatientRepository patientRepository) : IUpdateAppointmentProcessingUseCase
{
    private readonly IAppointmentRepository _appointmentRepository = repository;
    private readonly IDoctorRepository _doctorRepository = doctorRepository;
    private readonly IPatientRepository _patientRepository = patientRepository;

    public async Task<ErrorOr<UpdateAppointmentResponse>> Execute(long id, UpdateAppointmentRequest request, CancellationToken cancellationToken = default)
    {
        var validationResult = new UpdateAppointmentRequestValidator().Validate(request);
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

        var appointment = await _appointmentRepository.GetByIdAsync(id, cancellationToken);
        if (appointment is not null)
        {
            appointment.AppointmentDate = request.AppointmentDate;
            appointment.Doctor = doctor;
            appointment.Patient = patient;
            appointment.Status = request.Status;

            await _appointmentRepository.UpdateAsync(appointment);

            return new UpdateAppointmentResponse
            {
                Message = $"Alteração do agendamento com Id {id} realizado com sucesso."
            };
        }

        return Error.Validation("NotFound", $"Agendamento com id: {id} não encontrado. Revise o Id informado ou tente novamente mais tarde");
    }
}
