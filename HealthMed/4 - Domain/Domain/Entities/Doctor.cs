using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Doctor
{
    [Key]
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string CRM { get; set; } = string.Empty;
    public string Specialty { get; set; } = string.Empty; // Um enumerador ou uma lista de algum lugar para limitar e padronizar esse campo?
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty; //Isso deve ser salvo de forma hasheada
}
