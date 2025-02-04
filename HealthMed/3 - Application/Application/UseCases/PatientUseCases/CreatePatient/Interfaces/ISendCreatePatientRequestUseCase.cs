namespace Application.UseCases.PatientUseCases.CreatePatient.Interfaces;

using Application.UseCases.PatientUseCases.CreatePatient.Commom;
using ErrorOr;


public interface ISendCreatePatientRequestUseCase
{
    Task<ErrorOr<CreatePatientResponse>> Execute(CreatePatientRequest request, CancellationToken cancellationToken = default);
}
