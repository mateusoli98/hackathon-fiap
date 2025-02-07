using Application.UseCases.Doctor.Get.Common;

namespace Application.UseCases.Doctor.Login;

public interface IPatientLoginUsecase
{
    Task<GetDoctorResponse?> ValidateCredentialsAsync(LoginRequest loginRequest);
}
