using Application.UseCases.DoctorUseCases.GetDoctor.Common;
using ErrorOr;

namespace Application.UseCases.DoctorUseCases.GetDoctor.Interfaces;

public interface IGetDoctorUseCase
{
    Task<ErrorOr<GetDoctorResponse>> Execute(long id, CancellationToken cancellationToken = default);
}
