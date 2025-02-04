using Application.UseCases.PatientUseCases.GetPatient.Common;
using ErrorOr;

namespace Application.UseCases.PatientUseCases.GetPatient.Interfaces;

public interface IGetPatientUseCase
{
    Task<ErrorOr<GetPatientResponse>> Execute(long id, CancellationToken cancellationToken = default);
}
