namespace Domain.DomainObjects.Filters;

public class PatientFilter : PaginationParams
{
    public string? CPF { get; set; } 
    public string? Name { get; set; } 
    public string? Email { get; set; } 
}
