
using Domain.DomainObjects.Filters;
using Domain.Entities;

namespace Domain.Repositories.Relational;

public interface IPatientRepository
{
    Task<long> SaveAsync(Patient patient, CancellationToken cancellationToken = default);
    Task UpdateAsync(Patient patient, CancellationToken cancellationToken = default);
    Task<Patient?> GetByIdAsync(long id, CancellationToken cancellationToken = default, bool isEnabled = true);
    Task<bool> Exists(string cpf, CancellationToken cancellationToken);
    Task<PaginationResult<Patient>> SearchAsync(PatientFilter filter, CancellationToken cancellationToken = default);
    Task DeleteAsync(Patient patient, CancellationToken cancellationToken = default);
    Task PermanentDelete(Patient patient, CancellationToken cancellationToken = default);
}
