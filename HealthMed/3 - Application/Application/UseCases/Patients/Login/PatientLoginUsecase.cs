using Application.UseCases.Patient.Get.Common;
using CrossCutting.Extensions;
using Domain.Repositories.Relational;

namespace Application.UseCases.Patient.Login;

public class PatientLoginUsecase(IPatientRepository patientRepository) : IPatientLoginUsecase
{
    private readonly IPatientRepository _patientRepository = patientRepository;

    public async Task<GetPatientResponse?> ValidateCredentialsAsync(PatientLoginRequest loginRequest)
    {
        // Obter o médico pelo CRM
        var patient = await _patientRepository.GetByEmail(loginRequest.Email);
        if (patient == null)
        {
            return null;
        }

        return patient.Password == loginRequest.Password.HashPassword() ? GetPatientResponse.Create(patient) : null;
    }}
