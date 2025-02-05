namespace Application.UseCases.Assessment.Search.Common;

using Domain.Entities;

public class SearchAssessmentResponse
{
    public long Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public int Rating { get; set; }
    public DateTime CreatedAt { get; set; }

    public static SearchAssessmentResponse Create(Assessment appointment)
    {
        return new SearchAssessmentResponse
        {
            Id = appointment.Id,
            Description = appointment.Description,
            Rating = appointment.Rating,
            CreatedAt = appointment.CreatedAt,
        };
    }
}
