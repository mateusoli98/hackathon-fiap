using Domain.DomainObjects.Filters;
using Domain.Entities;
using Domain.Repositories.Relational;
using Infra.Extensions;
using Infra.Persistence.Sql.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace Infra.Persistence.Sql.Repositories;

public class AssessmentRepository: IAssessmentRepository
{
    private readonly DataContext _dataContext;
    public AssessmentRepository(IServiceScopeFactory scopeFactory)
    {
        _dataContext = scopeFactory.CreateScope().ServiceProvider.GetRequiredService<DataContext>();
    }

    public async Task<Assessment?> GetByIdAsync(long id, CancellationToken cancellationToken = default, bool isEnabled = true)
    {
        var query = _dataContext.Assessment.AsQueryable()
            .Where(q => q.Id == id && q.IsEnabled == isEnabled)
            .FirstOrDefaultAsync(cancellationToken);
        return await query;
    }

    public async Task<bool> Exists(long id, CancellationToken cancellationToken = default)
    {
        var query = _dataContext.Assessment.AsQueryable()
            .Where(q => q.Id == id && q.IsEnabled == true)
            .AnyAsync(cancellationToken);
        return await query;
    }

    public async Task<PaginationResult<Assessment>> SearchAsync(AssessmentFilter filter, CancellationToken cancellationToken = default)
    {
        var query = _dataContext.Assessment.AsQueryable();
        if (filter.Patient is not null)
        {
            query = query.Where(q => q.Patient.Id == filter.Patient.Id);
        }
        if (filter.Doctor is not null)
        {
            query = query.Where(q => q.Doctor.Id == filter.Doctor.Id);
        }
       
        if (filter.StartDate.HasValue)
        {
            query = query.Where(q => q.CreatedAt >= filter.StartDate);
        }
        if (filter.EndDate.HasValue)
        {
            query = query.Where(q => q.CreatedAt <= filter.EndDate);
        }

        var result = await query
            .Where(q => q.IsEnabled == true)
            .OrderBy(q => q.CreatedAt)
            .AsNoTracking()
            .ToPaginatedAsync(filter);
        return result;
    }

    public async Task<long> SaveAsync(Assessment assessment, CancellationToken cancellationToken = default)
    {
        await _dataContext.Assessment.AddAsync(assessment, cancellationToken);
        await _dataContext.SaveChangesAsync(cancellationToken);
        return assessment.Id;
    }

    public async Task UpdateAsync(Assessment assessment, CancellationToken cancellationToken = default)
    {
        assessment.UpdatedAt = DateTime.UtcNow;
        _dataContext.Assessment.Update(assessment);
        await _dataContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Assessment assessment, CancellationToken cancellationToken = default)
    {
        assessment.IsEnabled = false;
        await UpdateAsync(assessment, cancellationToken);
    }

    public async Task PermanentDelete(Assessment assessment, CancellationToken cancellationToken = default)
    {
        _dataContext.Assessment.Remove(assessment);
        await _dataContext.SaveChangesAsync(cancellationToken);
    }
}
