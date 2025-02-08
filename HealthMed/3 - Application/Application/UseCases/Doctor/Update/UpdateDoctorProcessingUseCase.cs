using Application.UseCases.Doctor.Update.Common;
using Application.UseCases.Doctor.Update.Interfaces;
using Domain.Repositories.Relational;
using CrossCutting.Extensions;
using ErrorOr;

namespace Application.UseCases.UpdateDoctor;

public class UpdateDoctorProcessingUseCase(IDoctorRepository repository) : IUpdateDoctorProcessingUseCase
{
    private readonly IDoctorRepository _doctorRepository = repository;

    public async Task<ErrorOr<UpdateDoctorResponse>> Execute(long doctorId, UpdateDoctorRequest request, CancellationToken cancellationToken = default)
    {
        var validationResult = new UpdateDoctorRequestValidator().Validate(request);
        if (!validationResult.IsValid)
        {
            return validationResult.ToErrorList();
        }

        var doctor = await _doctorRepository.GetByIdAsync(doctorId, cancellationToken);
        if (doctor is not null)
        {
            doctor.Name = request.Name;
            doctor.Email = request.Email;
            doctor.Specialty = request.Specialty;

            if (doctor.CRM != request.CRM)
            {
                var canUpdateCRM = await Validate(request, cancellationToken);
                if (canUpdateCRM)
                {
                    doctor.CRM = request.CRM;
                }
                else
                {
                    return Error.Validation("Validation", "CRM informado já está cadastrado.");
                }
            }

            await _doctorRepository.UpdateAsync(doctor);

            return new UpdateDoctorResponse
            {
                Message = $"Alteração do médico com Id {doctorId} realizado com sucesso."
            };
        }

        return Error.Validation("NotFound", $"Médico com id: {doctorId} não encontrado. Revise o Id informado ou tente novamente mais tarde");

    }

    private async Task<bool> Validate(UpdateDoctorRequest request, CancellationToken cancellationToken) =>
        await _doctorRepository.Exists(request.CRM, cancellationToken);
}
