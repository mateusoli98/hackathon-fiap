using Application.Appointment.Update.Common;
using FluentValidation;

namespace Application.UseCases.Appointment.Common
{
    public class UpdateAppointmentRequestValidator : AbstractValidator<UpdateAppointmentRequest>
    {
        public UpdateAppointmentRequestValidator()
        {
           
        }
    }
}
