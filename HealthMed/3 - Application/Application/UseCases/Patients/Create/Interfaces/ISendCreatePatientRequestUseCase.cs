namespace Application.UseCases.Patient.Create.Interfaces;

using Application.UseCases.Patient.Create.Commom;
using ErrorOr;

public interface ISendCreatePatientRequestUseCase
{
    Task<ErrorOr<CreatePatientResponse>> Execute(CreatePatientRequest request, CancellationToken cancellationToken = default);
}
