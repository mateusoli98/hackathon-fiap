using Application.UseCases.UpdateDoctor.Common;
using ErrorOr;

namespace Application.UseCases.UpdateDoctor.Interfaces;

public interface ISendUpdateDoctorRequestUseCase
{
    Task<ErrorOr<UpdateDoctorResponse>> Execute(string doctorId, UpdateDoctorRequest request, CancellationToken cancellationToken = default);
}
