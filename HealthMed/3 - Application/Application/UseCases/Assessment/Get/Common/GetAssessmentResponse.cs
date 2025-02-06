namespace Application.UseCases.Assessment.Get.Common;

using Domain.Entities;
using System.Text.Json.Serialization;

public class GetAssessmentResponse
{
    public long Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public int Rating { get; set; }
    public bool IsEnable { get; set; }

    [JsonIgnore]
    public DateTime CreatedAt { get; set; }
    
    [JsonIgnore]
    public DateTime UpdatedAt { get; set; }

    public static GetAssessmentResponse Create(Assessment appointment)
    {
        return new GetAssessmentResponse
        {
            Id = appointment.Id,
            Description = appointment.Description,
            Rating = appointment.Rating,
            IsEnable = appointment.IsEnabled,
            CreatedAt = appointment.CreatedAt,
            UpdatedAt = appointment.UpdatedAt

        };
    }

    public static Assessment GetAppointment(GetAssessmentResponse response)
    {
        return new Assessment
        {
            Id = response.Id,
            Description = response.Description,
            Rating = response.Rating,
            IsEnabled = response.IsEnable,
            CreatedAt = response.CreatedAt,
            UpdatedAt = response.UpdatedAt,
        };
    }
}
