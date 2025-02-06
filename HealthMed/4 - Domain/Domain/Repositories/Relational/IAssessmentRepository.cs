
using Domain.DomainObjects.Filters;
using Domain.Entities;

namespace Domain.Repositories.Relational;

public interface IAssessmentRepository
{
    Task<long> SaveAsync(Assessment assessment, CancellationToken cancellationToken = default);
    Task UpdateAsync(Assessment assessment, CancellationToken cancellationToken = default);
    Task<Assessment?> GetByIdAsync(long id, CancellationToken cancellationToken = default, bool isEnabled = true);
    Task<PaginationResult<Assessment>> SearchAsync(AssessmentFilter filter, CancellationToken cancellationToken = default);
    Task DeleteAsync(Assessment assessment, CancellationToken cancellationToken = default);
    Task PermanentDelete(Assessment assessment, CancellationToken cancellationToken = default);
    Task<bool> Exists(long id, CancellationToken cancellationToken = default);
}
