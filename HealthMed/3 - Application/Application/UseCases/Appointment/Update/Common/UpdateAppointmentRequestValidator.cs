using Application.Appointment.Update.Common;
using FluentValidation;

namespace Application.UseCases.Appointment.Common;

public class UpdateAppointmentRequestValidator : AbstractValidator<UpdateAppointmentRequest>
{
    public UpdateAppointmentRequestValidator()
    {
        RuleFor(x => x.Status).NotNull().WithMessage("O status é obrigatório.");

        RuleFor(x => x.AppointmentDate).NotNull().WithMessage("A data da consulta é obrigatória.")
            .Must(x => x > DateTime.UtcNow).WithMessage("A data da consulta não pode ser no passado.");

        RuleFor(x => x.DoctorId).NotNull().WithMessage("O médico é obrigatório.");

        RuleFor(x => x.PatientId).NotNull().WithMessage("O paciente é obrigatório.");
    }
}
