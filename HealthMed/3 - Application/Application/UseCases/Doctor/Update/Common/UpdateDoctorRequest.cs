using Domain.DomainObjects.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.UseCases.Doctor.Update.Common;

public class UpdateDoctorRequest
{
    [JsonIgnore]
    public string? DoctorId { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "O CRM é obrigatório.")]
    [StringLength(10, MinimumLength = 3, ErrorMessage = "O CRM deve ter entre 3 e 10 caracteres.")]
    public string CRM { get; set; } = string.Empty;

    [Required(ErrorMessage = "O email é obrigatório.")]
    [EmailAddress(ErrorMessage = "O email não está em um formato válido.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "A senha é obrigatória.")]
    [StringLength(15, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 15 caracteres.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,15}$", ErrorMessage = "A senha deve conter pelo menos uma letra maiúscula, uma letra minúscula, um número e um caractere especial")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "A especialidade é obrigatória.")]
    public SpecialtyEnum Specialty { get; set; }
}
