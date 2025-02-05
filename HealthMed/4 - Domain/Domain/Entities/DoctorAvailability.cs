using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class DoctorAvailability
{
    [Key]
    public long Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public long AppointmentValue { get; set; }
    public bool IsEnabled { get; set; }

    [ForeignKey("Id")]
    public Doctor Doctor { get; set; } 
    [ForeignKey("Id")]
    public HealthCenter Place { get; set; }
}
