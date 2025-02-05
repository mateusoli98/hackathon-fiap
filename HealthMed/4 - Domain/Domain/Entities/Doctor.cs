using Domain.DomainObjects.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Doctor
{
    [Key]
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string CRM { get; set; } = string.Empty;
    public SpecialtyEnum Specialty { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty; //Isso deve ser salvo de forma hasheada
    public bool IsEnabled { get; set; }
}
