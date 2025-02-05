using Domain.DomainObjects.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Appointments
{
    [Key]
    public long Id { get; set; }
    public AppointmentStatus Status { get; set; }

    public DateTime AppointmentDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
    public bool IsEnabled { get; set; }

    [ForeignKey("Id")]
    public Doctor Doctor { get; set; }

    [ForeignKey("Id")]
    public Patient Patient { get; set; }
}
