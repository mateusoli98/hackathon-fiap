namespace Application.UseCases.Assessment.Get.Common;

using Domain.Entities;

public class GetAssessmentResponse
{
    public long Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public int Rating { get; set; }
    public DateTime CreatedAt { get; set; }

    public static GetAssessmentResponse Create(Assessment appointment)
    {
        return new GetAssessmentResponse
        {
            Id = appointment.Id,
            Description = appointment.Description,
            Rating = appointment.Rating,
            CreatedAt = appointment.CreatedAt
        };
    }

    public static Assessment GetAppointment(GetAssessmentResponse response)
    {
        return new Assessment
        {
            Id = response.Id,
            Description = response.Description,
            Rating = response.Rating,
            CreatedAt = response.CreatedAt
        };
    }
}
