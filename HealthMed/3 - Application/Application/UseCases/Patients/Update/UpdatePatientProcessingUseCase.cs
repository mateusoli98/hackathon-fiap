namespace Application.UseCases.Patient.Update;

using Application.UseCases.Patient.Update.Common;
using Application.UseCases.Patient.Update.Interfaces;
using Domain.Repositories.Relational;
using CrossCutting.Extensions;
using ErrorOr;

public class UpdatePatientProcessingUseCase(IPatientRepository repository) : IUpdatePatientProcessingUseCase
{
    private readonly IPatientRepository _patientRepository = repository;

    public async Task<ErrorOr<UpdatePatientResponse>> Execute(long patientId, UpdatePatientRequest request, CancellationToken cancellationToken = default)
    {
        var validationResult = new UpdatePatientRequestValidator().Validate(request);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrorList();
        }

        var patient = await _patientRepository.GetByIdAsync(patientId, cancellationToken);
        if (patient is not null)
        {
            patient.Id = patientId;
            patient.Name = request.Name;
            patient.Email = request.Email;
            patient.CPF = request.CPF;

           await _patientRepository.UpdateAsync(patient, cancellationToken);

            return new UpdatePatientResponse
            {
                Message = $"Paciente com Id {patientId} atualizado com sucesso."
            };
        }

        return Error.Validation("NotFound", $"Paciente com id: {patientId} não encontrado. Revise o Id informado ou tente novamente mais tarde");
    }

}
