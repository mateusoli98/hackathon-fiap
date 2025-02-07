using Application.UseCases.Patient.Get.Common;

namespace Application.UseCases.Patient.Login;

public interface IPatientLoginUsecase
{
    Task<GetPatientResponse?> ValidateCredentialsAsync(LoginRequest loginRequest);
}
