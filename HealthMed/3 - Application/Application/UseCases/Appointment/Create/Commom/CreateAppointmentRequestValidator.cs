using FluentValidation;

namespace Application.UseCases.Appointment.Create.Commom;

public class CreateAppointmentRequestValidator : AbstractValidator<CreateAppointmentRequest>
{
    public CreateAppointmentRequestValidator()
    {
        RuleFor(x => x.AppointmentDate)
            .NotEmpty().WithMessage("A data é obrigatória.")
            .Must(date => date >= DateTime.UtcNow).WithMessage("A data não pode estar no passado.");

        RuleFor(x => x.DoctorId)
            .NotEmpty().WithMessage("O médico é obrigatório.");

        RuleFor(x => x.PatientId)
            .NotEmpty().WithMessage("O paciente é obrigatório.");
    }
}
