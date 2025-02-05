using Application.UseCases.Patient.Get.Common;
using Application.UseCases.Patient.Get.Interfaces;
using Domain.Repositories.Relational;
using ErrorOr;

namespace Application.UseCases.Patient.Get;

public class GetPatientUseCase : IGetPatientUseCase
{
    private readonly IPatientRepository _patientRepository;

    public GetPatientUseCase(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<ErrorOr<GetAppoitmentResponse>> Execute(long id, CancellationToken cancellationToken = default)
    {
        var patient = await _patientRepository.GetByIdAsync(id, cancellationToken);

        if (patient is not null)
        {
            return GetAppoitmentResponse.Create(patient);
        }

        return Error.NotFound(description: $"Paciente com id: {id} não encontrado. Revise o Id informado ou tente novamente mais tarde");
    }
}
