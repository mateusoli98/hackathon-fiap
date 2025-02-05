using Domain.DomainObjects.Enums;

namespace Domain.DomainObjects.Filters;

public class DoctorFilter : PaginationParams
{
    public string? CRM { get; set; }
    public string? Name { get; set; }
    public SpecialtyEnum? Specialty { get; set; }
    public string? Email { get; set; }
}
