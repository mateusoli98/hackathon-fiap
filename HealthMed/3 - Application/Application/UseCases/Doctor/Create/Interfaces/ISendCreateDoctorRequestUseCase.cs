namespace Application.UseCases.Doctor.Create.Interfaces;

using Application.UseCases.Doctor.Create.Commom;
using ErrorOr;


public interface ISendCreateDoctorRequestUseCase
{
    Task<ErrorOr<CreateDoctorResponse>> Execute(CreateDoctorRequest request, CancellationToken cancellationToken = default);
}
