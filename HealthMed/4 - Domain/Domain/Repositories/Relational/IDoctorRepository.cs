
using Domain.DomainObjects.Filters;
using Domain.Entities;

namespace Domain.Repositories.Relational;

public interface IDoctorRepository
{
    Task<long> SaveAsync(Doctor doctor, CancellationToken cancellationToken = default);
    Task UpdateAsync(Doctor doctor, CancellationToken cancellationToken = default);
    Task<Doctor?> GetByIdAsync(long id, CancellationToken cancellationToken = default, bool isEnabled = true);
    Task<bool> Exists(string crm, CancellationToken cancellationToken);
    Task<PaginationResult<Doctor>> SearchAsync(DoctorFilter filter, CancellationToken cancellationToken = default);
    Task DeleteAsync(Doctor doctor, CancellationToken cancellationToken = default);
    Task PermanentDelete(Doctor doctor, CancellationToken cancellationToken = default);
}
