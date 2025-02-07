
namespace Application.UseCases.Patient.Update.Interfaces;

using Application.UseCases.Patient.Update.Common;
using ErrorOr;

public interface IUpdatePatientProcessingUseCase
{
    Task<ErrorOr<UpdatePatientResponse>> Execute(long patientId, UpdatePatientRequest request, CancellationToken cancellationToken = default);
}
