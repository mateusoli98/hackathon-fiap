namespace Application.UseCases.Patient.Create.Interfaces;

using Application.UseCases.Patient.Create.Commom;
using ErrorOr;

public interface ICreatePatientProcessingUseCase
{
    Task<ErrorOr<CreatePatientResponse>> Execute(CreatePatientRequest request, CancellationToken cancellationToken = default);
}
