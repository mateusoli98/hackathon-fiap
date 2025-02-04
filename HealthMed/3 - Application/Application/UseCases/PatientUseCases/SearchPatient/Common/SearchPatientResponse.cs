namespace Application.UseCases.PatientUseCases.SearchPatient.Common;

using Domain.Entities;

public class SearchPatientResponse
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;


    public static SearchPatientResponse Create(Patient doctor)
    {
        return new SearchPatientResponse
        {
            Id = doctor.Id,
            Name = doctor.Name,
            CPF = doctor.CPF,
            Email = doctor.Email
        };
    }
}
