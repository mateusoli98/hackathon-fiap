using Application.UseCases.CreateDoctor.Commom;
using ErrorOr;

namespace Application.UseCases.CreateContact.Interfaces;

public interface ISendCreateDoctorRequestUseCase
{
    Task<ErrorOr<CreateDoctorResponse>> Execute(CreateDoctorRequest request, CancellationToken cancellationToken = default);
}
