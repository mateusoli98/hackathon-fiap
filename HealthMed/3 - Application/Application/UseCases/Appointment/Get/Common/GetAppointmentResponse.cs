namespace Application.UseCases.Appointment.Get.Common;

using Domain.DomainObjects.Enums;
using Domain.Entities;

public class GetAppointmentResponse
{
    public long Id { get; set; }
    public AppointmentStatus Status { get; set; }
    public DateTime AppointmentDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsEnabled { get; set; }
    public Doctor Doctor { get; set; }
    public Patient Patient { get; set; }


    public static GetAppointmentResponse Create(Appointments appointment)
    {
        return new GetAppointmentResponse
        {
            Id = appointment.Id,
            Status = appointment.Status,
            AppointmentDate = appointment.AppointmentDate,
            CreatedAt = appointment.CreatedAt,
            UpdatedAt = appointment.UpdatedAt
        };
    }

    public static Appointments GetAppointment(GetAppointmentResponse response)
    {
        return new Appointments
        {
            Id = response.Id,
            Status = response.Status,
            AppointmentDate = response.AppointmentDate,
            CreatedAt = response.CreatedAt,
            UpdatedAt = response.UpdatedAt
        };
    }
}
