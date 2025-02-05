using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;


public class HealthCenter
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public int Number { get; set; }
    public string PostalCode { get; set; } = string.Empty;
}
