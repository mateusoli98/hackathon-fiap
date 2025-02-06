using System.Text.Json.Serialization;

namespace Application.Assessment.Update.Common;

public class UpdateAssessmentRequest
{
    [JsonIgnore]
    public string? AssessmentId { get; set; }
    public string Description { get; set; } = string.Empty;
    public int Rating { get; set; }
    public bool IsEnabled { get; set; }
    public long DoctorId { get; set; }
    public long PatientId { get; set; }
}
