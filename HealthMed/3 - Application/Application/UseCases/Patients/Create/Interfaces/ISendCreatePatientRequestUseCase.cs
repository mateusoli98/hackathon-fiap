namespace Application.UseCases.Patient.Create.Interfaces;

using Application.UseCases.Patient.Create.Commom;
using ErrorOr;


public interface ISendCreateAppointmentRequestUseCase
{
    Task<ErrorOr<CreateAppointmentResponse>> Execute(CreateAppointmentRequest request, CancellationToken cancellationToken = default);
}
