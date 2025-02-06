using Application.UseCases.Doctor.Update.Common;
using ErrorOr;

namespace Application.UseCases.Doctor.Update.Interfaces;

public interface IUpdateDoctorProcessingUseCase
{
    Task<ErrorOr<UpdateDoctorResponse>> Execute(long doctorId, UpdateDoctorRequest request, CancellationToken cancellationToken = default);
}
