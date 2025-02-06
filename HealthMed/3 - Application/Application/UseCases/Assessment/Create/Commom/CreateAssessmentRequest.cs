using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.UseCases.Assessment.Create.Commom;

public class CreateAssessmentRequest
{
    [Required(ErrorMessage = "A descrição é obrigatória.")]
    public string Description { get; set; }

    [Required(ErrorMessage = "A nota é obrigatória.")]
    public int Rating { get; set; }

    [Required(ErrorMessage = "O ID do médico é obrigatório.")]
    public long DoctorId { get; set; }

    [Required(ErrorMessage = "O ID do paciente é obrigatório.")]
    public long PatientId { get; set; }
}
