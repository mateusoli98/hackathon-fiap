using FluentValidation;

namespace Application.UseCases.Assessment.Create.Commom;

public class CreateAssessmentRequestValidator : AbstractValidator<CreateAssessmentRequest>
{
    public CreateAssessmentRequestValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("A descrição é obrigatória.");

        RuleFor(x => x.Rating)
            .NotEmpty().WithMessage("A nota é obrigatória.");

        RuleFor(x => x.DoctorId)
            .NotEmpty().WithMessage("O ID do médico é obrigatório.");

        RuleFor(x => x.PatientId)
            .NotEmpty().WithMessage("O ID do paciente é obrigatório.");
    }
}
