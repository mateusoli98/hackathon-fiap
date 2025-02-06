
using Domain.DomainObjects.Filters;
using Domain.Entities;

namespace Domain.Repositories.Relational;

public interface IAppointmentRepository
{
    Task<long> SaveAsync(Appointments appointment, CancellationToken cancellationToken = default);
    Task UpdateAsync(Appointments appointment, CancellationToken cancellationToken = default);
    Task<Appointments?> GetByIdAsync(long id, CancellationToken cancellationToken = default, bool IsEnable = true);
    Task<PaginationResult<Appointments>> SearchAsync(AppointmentFilter filter, CancellationToken cancellationToken = default);
    Task DeleteAsync(Appointments appointment, CancellationToken cancellationToken = default);
    Task PermanentDelete(Appointments appointment, CancellationToken cancellationToken = default);
    Task<bool> Exists(long doctorId, long patientId, DateTime appointmentDate, CancellationToken cancellationToken);
}
