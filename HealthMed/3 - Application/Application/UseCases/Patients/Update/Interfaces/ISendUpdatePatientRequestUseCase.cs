using Application.UseCases.Patient.Update.Common;
using ErrorOr;

namespace Application.UseCases.Patient.Update.Interfaces;

public interface ISendUpdatePatientRequestUseCase
{
    Task<ErrorOr<UpdatePatientResponse>> Execute(long patientId, UpdatePatientRequest request, CancellationToken cancellationToken = default);
}
