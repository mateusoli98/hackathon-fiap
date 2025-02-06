namespace Application.UseCases.Doctor.Get.Common;

using Domain.DomainObjects.Enums;
using Domain.Entities;

public class GetDoctorResponse
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string CRM { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public SpecialtyEnum Specialty { get; set; }


    public static GetDoctorResponse Create(Doctor doctor)
    {
        return new GetDoctorResponse
        {
            Id = doctor.Id,
            Name = doctor.Name,
            CRM = doctor.CRM,
            Email = doctor.Email,
            Specialty = doctor.Specialty
        };
    }

    public static Doctor GetDoctor(GetDoctorResponse response)
    {
        return new Doctor
        {
            Id = response.Id,
            Name = response.Name,
            CRM = response.CRM,
            Email = response.Email,
            Specialty = response.Specialty
        };
    }
}
