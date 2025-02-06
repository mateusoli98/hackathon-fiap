namespace Application.UseCases.Patient.Get.Common;

using Domain.Entities;

public class GetPatientResponse
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;


    public static GetPatientResponse Create(Patient Patient)
    {
        return new GetPatientResponse
        {
            Id = Patient.Id,
            Name = Patient.Name,
            CPF = Patient.CPF,
            Email = Patient.Email
        };
    }

    public static Patient GetPatient(GetPatientResponse response)
    {
        return new Patient
        {
            Id = response.Id,
            Name = response.Name,
            CPF = response.CPF,
            Email = response.Email
        };
    }
}
