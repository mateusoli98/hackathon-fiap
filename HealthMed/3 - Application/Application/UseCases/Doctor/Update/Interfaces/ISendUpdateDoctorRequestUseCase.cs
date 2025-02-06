using Application.UseCases.Doctor.Update.Common;
using ErrorOr;

namespace Application.UseCases.Doctor.Update.Interfaces;

public interface ISendUpdateDoctorRequestUseCase
{
    Task<ErrorOr<UpdateDoctorResponse>> Execute(long doctorId, UpdateDoctorRequest request, CancellationToken cancellationToken = default);
}
