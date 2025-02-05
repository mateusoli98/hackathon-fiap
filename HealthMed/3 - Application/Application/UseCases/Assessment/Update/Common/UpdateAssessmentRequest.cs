using Domain.DomainObjects.Enums;
using Domain.Entities;
using System.Text.Json.Serialization;

namespace Application.Assessment.Update.Common;

public class UpdateAssessmentRequest
{
    [JsonIgnore]
    public string? PatientId { get; set; }
    public AppointmentStatus Status { get; set; }
    public DateTime AppointmentDate { get; set; }
    [JsonIgnore]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public Doctor Doctor { get; set; }
    public Patient Patient { get; set; }
}
