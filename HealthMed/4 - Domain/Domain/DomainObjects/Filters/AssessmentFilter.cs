
using Domain.Entities;

namespace Domain.DomainObjects.Filters;

public class AssessmentFilter : PaginationParams
{
    public string? Description { get; set; }
    public int? Rating { get; set; }
    public bool? IsEnabled { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public Doctor? Doctor { get; set; }  
    public Patient? Patient { get; set; }

}
