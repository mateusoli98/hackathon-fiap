
using Domain.DomainObjects.Filters;
using Domain.Entities;
using Domain.Repositories.Relational;
using Infra.Extensions;
using Infra.Persistence.Sql.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Persistence.Sql.Repositories;

public class DoctorRepository : IDoctorRepository
{
    private readonly DataContext _dataContext;

    public DoctorRepository(IServiceScopeFactory scopeFactory)
    {
        _dataContext = scopeFactory.CreateScope().ServiceProvider.GetRequiredService<DataContext>();
    }

    public async Task<Doctor?> GetByIdAsync(long id, CancellationToken cancellationToken = default, bool isEnabled = true)
    {
        var query = _dataContext.Doctors.AsQueryable()
            .Where(q => q.Id == id && q.IsEnabled == isEnabled)
            .FirstOrDefaultAsync(cancellationToken);

        return await query;
    }

    public async Task<bool> Exists(string crm, CancellationToken cancellationToken = default)
    {
        var query = _dataContext.Doctors.AsQueryable()
            .Where(q => q.CRM == crm && q.IsEnabled == true)
            .AnyAsync(cancellationToken);
        return await query;
    }

    public async Task<PaginationResult<Doctor>> SearchAsync(DoctorFilter filter, CancellationToken cancellationToken = default)
    {
        var query = _dataContext.Doctors.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Name))
        {
            query = query.Where(q => q.Name.Contains(filter.Name));
        }
        if (!string.IsNullOrWhiteSpace(filter.CRM))
        {
            query = query.Where(q => q.CRM.Contains(filter.CRM));
        }
        if (filter.Specialty is not null)
        {
            query = query.Where(q => q.Specialty == filter.Specialty);
        }
        if (!string.IsNullOrWhiteSpace(filter.Email))
        {
            query = query.Where(q => q.Email.Contains(filter.Email));
        }
        var result = await query
            .Where(q => q.IsEnabled == true)
            .OrderBy(q => q.CreatedAt)
            .AsNoTracking()
            .ToPaginatedAsync(filter);
        return result;
    }

    public async Task<long> SaveAsync(Doctor doctor, CancellationToken cancellationToken = default)
    {
        await _dataContext.Doctors.AddAsync(doctor, cancellationToken);
        await _dataContext.SaveChangesAsync(cancellationToken);

        return doctor.Id;
    }

    public async Task UpdateAsync(Doctor doctor, CancellationToken cancellationToken = default)
    {
        doctor.UpdatedAt = DateTime.UtcNow;
        _dataContext.Doctors.Update(doctor);
        await _dataContext.SaveChangesAsync(cancellationToken);
    }

    public Task DeleteAsync(Doctor doctor, CancellationToken cancellationToken = default)
    {
        doctor.IsEnabled = false;
        return UpdateAsync(doctor, cancellationToken);
    }

    public async Task PermanentDelete(Doctor doctor, CancellationToken cancellationToken = default)
    {
        _dataContext.Remove(doctor);
        await _dataContext.SaveChangesAsync(cancellationToken);
    }
}
