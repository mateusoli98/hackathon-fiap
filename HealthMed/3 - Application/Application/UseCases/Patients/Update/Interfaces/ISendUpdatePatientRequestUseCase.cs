using Application.UseCases.Patient.Update.Common;
using ErrorOr;

namespace Application.UseCases.Patient.Update.Interfaces;

public interface ISendUpdateAppointmentRequestUseCase
{
    Task<ErrorOr<UpdateAppointmentResponse>> Execute(string patientId, UpdatePatientRequest request, CancellationToken cancellationToken = default);
}
