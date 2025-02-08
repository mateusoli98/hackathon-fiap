using Application.UseCases.Appointment.Get.Common;
using Application.UseCases.Appointment.Get.Interfaces;
using Domain.Repositories.Relational;
using ErrorOr;

namespace Application.UseCases.Appointment.Get;

public class GetAppointmentUseCase : IGetAppointmentUseCase
{
    private readonly IAppointmentRepository _appointmentRepository;

    public GetAppointmentUseCase(IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    public async Task<ErrorOr<GetAppointmentResponse>> Execute(long id, CancellationToken cancellationToken = default)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(id, cancellationToken);

        if (appointment is not null)
        {
            return GetAppointmentResponse.Create(appointment);
        }

        return Error.NotFound(description: $"Agendamento com id: {id} não encontrado. Revise o Id informado ou tente novamente mais tarde");
    }
}
