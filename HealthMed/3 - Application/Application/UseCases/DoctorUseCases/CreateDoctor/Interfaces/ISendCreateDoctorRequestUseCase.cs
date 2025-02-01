namespace Application.UseCases.DoctorUseCases.CreateDoctor.Interfaces;

using Application.UseCases.DoctorUseCases.CreateDoctor.Commom;
using ErrorOr;


public interface ISendCreateDoctorRequestUseCase
{
    Task<ErrorOr<CreateDoctorResponse>> Execute(CreateDoctorRequest request, CancellationToken cancellationToken = default);
}
