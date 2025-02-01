namespace Domain.DomainObjects.Filters;

public class DoctorFilter : PaginationParams
{
    public string CRM { get; set; } = string.Empty;
}
