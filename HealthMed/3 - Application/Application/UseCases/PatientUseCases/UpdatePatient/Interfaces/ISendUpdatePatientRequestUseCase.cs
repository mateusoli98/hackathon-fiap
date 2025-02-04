using Application.UseCases.UpdatePatient.Common;
using ErrorOr;

namespace Application.UseCases.UpdatePatient.Interfaces;

public interface ISendUpdatePatientRequestUseCase
{
    Task<ErrorOr<UpdatePatientResponse>> Execute(string patientId, UpdatePatientRequest request, CancellationToken cancellationToken = default);
}
