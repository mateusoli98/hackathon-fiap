namespace Application.UseCases.Patient.Search.Common;

using Domain.Entities;

public class SearchAppointmentResponse
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;


    public static SearchAppointmentResponse Create(Patient doctor)
    {
        return new SearchAppointmentResponse
        {
            Id = doctor.Id,
            Name = doctor.Name,
            CPF = doctor.CPF,
            Email = doctor.Email
        };
    }
}
