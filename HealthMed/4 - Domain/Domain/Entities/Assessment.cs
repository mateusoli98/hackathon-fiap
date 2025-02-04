using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

/// <summary>
/// Representa a avaliação de um usuário para um doutor.
/// </summary>
public class Assessment
{
    [Key]
    public long Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public int Rating { get; set; }
    public DateTime CreatedAt { get; set; }
    [ForeignKey("Id")]
    public Doctor Doctor { get; set; }

    [ForeignKey("Id")]
    public Patient Patient { get; set; }
}
