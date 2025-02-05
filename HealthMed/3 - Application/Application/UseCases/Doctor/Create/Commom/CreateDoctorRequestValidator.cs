using FluentValidation;

namespace Application.UseCases.Doctor.Create.Commom;

public class CreateDoctorRequestValidator : AbstractValidator<CreateDoctorRequest>
{
    public CreateDoctorRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .Length(3, 100).WithMessage("O nome deve ter entre 3 e 100 caracteres.");

        RuleFor(x => x.CRM)
            .NotEmpty().WithMessage("O CRM é obrigatório.")
            .Length(3, 10).WithMessage("O CRM deve ter entre 3 e 10 caracteres.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("O email é obrigatório.")
            .EmailAddress().WithMessage("O email não está em um formato válido.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("A senha é obrigatória.")
            .MinimumLength(6).WithMessage("A senha deve ter entre 6 e 15 caracteres.")
            .MaximumLength(15).WithMessage("A senha deve ter entre 6 e 15 caracteres.")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,15}$")
                .WithMessage("A senha deve conter pelo menos uma letra maiúscula, uma letra minúscula, um número e um caractere especial");

        RuleFor(x => x.Specialty)
            .NotEmpty().WithMessage("A especialidade é obrigatória.");
    }
}
