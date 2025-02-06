namespace Application.UseCases.Appointment.Search.Common;

using Domain.DomainObjects.Enums;
using Domain.Entities;

public class SearchAppointmentResponse
{
    public long Id { get; set; }
    public AppointmentStatus Status { get; set; }
    public DateTime AppointmentDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public static SearchAppointmentResponse Create(Appointments appointment)
    {
        return new SearchAppointmentResponse
        {
            Id = appointment.Id,
            AppointmentDate = appointment.AppointmentDate,
            Status = appointment.Status,
            CreatedAt = appointment.CreatedAt,
            UpdatedAt = appointment.UpdatedAt
        };
    }
}
