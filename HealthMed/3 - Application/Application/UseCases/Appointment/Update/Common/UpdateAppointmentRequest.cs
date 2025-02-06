using Domain.DomainObjects.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Appointment.Update.Common;

public class UpdateAppointmentRequest
{
    [JsonIgnore]
    public string? AppointmentId { get; set; }

    [Required(ErrorMessage = "O status é obrigatório.")]
    public AppointmentStatus Status { get; set; }

    [Required(ErrorMessage = "A data da consulta é obrigatória.")]
    [CustomValidation(typeof(UpdateAppointmentRequest), nameof(ValidateAppointmentDate))]
    public DateTime AppointmentDate { get; set; }

    [Required(ErrorMessage = "O médico é obrigatório.")]
    public long DoctorId { get; set; }

    [Required(ErrorMessage = "O paciente é obrigatório.")]
    public long PatientId { get; set; }

    private static ValidationResult? ValidateAppointmentDate(DateTime appointmentDate, ValidationContext context)
    {
        if (appointmentDate < DateTime.UtcNow)
        {
            return new ValidationResult("A data da consulta não pode ser no passado.");
        }
        return ValidationResult.Success;
    }
}
