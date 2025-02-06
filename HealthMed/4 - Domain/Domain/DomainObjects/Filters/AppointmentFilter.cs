using Domain.DomainObjects.Enums;
using Domain.Entities;

namespace Domain.DomainObjects.Filters;

public class AppointmentFilter : PaginationParams
{
    public AppointmentStatus? Status { get; set; }
    public DateTime? AppointmentDate { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public Doctor? Doctor { get; set; }
    public Patient? Patient { get; set; }
}
