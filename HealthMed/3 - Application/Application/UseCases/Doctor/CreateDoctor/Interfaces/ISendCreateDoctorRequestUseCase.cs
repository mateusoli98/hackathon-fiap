using Application.UseCases.Doctor.CreateDoctor.Commom;
using ErrorOr;

namespace Application.UseCases.Doctor.CreateDoctor.Interfaces;

public interface ISendCreateDoctorRequestUseCase
{
    Task<ErrorOr<CreateDoctorResponse>> Execute(CreateDoctorRequest request, CancellationToken cancellationToken = default);
}
