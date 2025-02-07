using Domain.Repositories.Relational;
using CrossCutting.Extensions;
using Application.UseCases.Doctor.Get.Common;

namespace Application.UseCases.Doctor.Login;

public class PatientLoginUsecase(IDoctorRepository doctorRepository) : IPatientLoginUsecase
{
    private readonly IDoctorRepository _doctorRepository = doctorRepository;

    public async Task<GetDoctorResponse?> ValidateCredentialsAsync(LoginRequest loginRequest)
    {
        // Obter o médico pelo CRM
        var doctor = await _doctorRepository.GetByCRM(loginRequest.Crm);
        if (doctor == null)
        {
            return null;
        }

        return doctor.Password == loginRequest.Password.HashPassword() ? GetDoctorResponse.Create(doctor) : null;
    }}
