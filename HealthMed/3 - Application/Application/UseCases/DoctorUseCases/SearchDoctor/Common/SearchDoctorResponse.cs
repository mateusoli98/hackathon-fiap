namespace Application.UseCases.DoctorUseCases.SearchDoctor.Common;

using Domain.Entities;

public class SearchDoctorResponse
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string CRM { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;


    public static SearchDoctorResponse Create(Doctor doctor)
    {
        return new SearchDoctorResponse
        {
            Id = doctor.Id,
            Name = doctor.Name,
            CRM = doctor.CRM,
            Email = doctor.Email
        };
    }
}
