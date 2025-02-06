using Application.UseCases.Doctor.Create.Commom;
using ErrorOr;

namespace Application.UseCases.Doctor.Create.Interfaces;

public interface ICreateDoctorProcessingUseCase
{
    Task<ErrorOr<CreateDoctorResponse>> Execute(CreateDoctorRequest request, CancellationToken cancellationToken = default);
}
