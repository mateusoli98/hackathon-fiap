using Domain.DomainObjects.Enums;
using Domain.Entities;

namespace Domain.DomainObjects.Filters;

public class AppointmentFilter : PaginationParams
{
    public AppointmentStatus? Status { get; set; }
    public DateTime? AppointmentDate { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Doctor? Doctor { get; set; }
    public Patient? Patient { get; set; }
}
