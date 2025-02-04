namespace Domain.DomainObjects.Filters;

public class PatientFilter : PaginationParams
{
    public string CPF { get; set; } = string.Empty;
}
