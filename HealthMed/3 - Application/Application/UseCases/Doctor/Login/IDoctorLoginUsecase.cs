using Application.UseCases.Doctor.Get.Common;

namespace Application.UseCases.Doctor.Login;

public interface IDoctorLoginUsecase
{
    Task<GetDoctorResponse?> ValidateCredentialsAsync(LoginRequest loginRequest);
}
