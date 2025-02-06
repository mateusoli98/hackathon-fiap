using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.Appointment.Create.Commom;

public class CreateAppointmentRequest
{
    [Required(ErrorMessage = "A data é obrigatória.")]
    public DateTime AppointmentDate { get; set; }
    
    [Required(ErrorMessage = "O médico é obrigatório.")]
    public long DoctorId { get; set; }

    [Required(ErrorMessage = "O paciente é obrigatório.")]
    public long PatientId { get; set; }
}
