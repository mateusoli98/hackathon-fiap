using Domain.DomainObjects.Filters;
using Domain.Entities;
using Domain.Repositories.Relational;
using Infra.Extensions;
using Infra.Persistence.Sql.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace Infra.Persistence.Sql.Repositories;

public class PatientRepository: IPatientRepository
{
    private readonly DataContext _dataContext;
    public PatientRepository(IServiceScopeFactory scopeFactory)
    {
        _dataContext = scopeFactory.CreateScope().ServiceProvider.GetRequiredService<DataContext>();
    }

    public async Task<Patient?> GetByIdAsync(long id, CancellationToken cancellationToken = default, bool isEnabled = true)
    {
        var query = _dataContext.Patients.AsQueryable()
            .Where(q => q.Id == id && q.IsEnabled == isEnabled)
            .FirstOrDefaultAsync(cancellationToken);
        return await query;
    }
    
    public async Task<bool> Exists(string cpf, CancellationToken cancellationToken = default)
    {
        var query = _dataContext.Patients.AsQueryable()
            .Where(q => q.CPF == cpf && q.IsEnabled == true)
            .AnyAsync(cancellationToken);
        return await query;
    }

    public async Task<PaginationResult<Patient>> SearchAsync(PatientFilter filter, CancellationToken cancellationToken = default)
    {
        var query = _dataContext.Patients.AsQueryable();
        if (!string.IsNullOrWhiteSpace(filter.Name))
        {
            query = query.Where(q => q.Name.Contains(filter.Name));
        }
        if (!string.IsNullOrWhiteSpace(filter.CPF))
        {
            query = query.Where(q => q.CPF.Contains(filter.CPF));
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

    public async Task<long> SaveAsync(Patient patient, CancellationToken cancellationToken = default)
    {
        await _dataContext.Patients.AddAsync(patient, cancellationToken);
        await _dataContext.SaveChangesAsync(cancellationToken);
        return patient.Id;
    }

    public async Task UpdateAsync(Patient patient, CancellationToken cancellationToken = default)
    {
        patient.UpdatedAt = DateTime.UtcNow;
        _dataContext.Patients.Update(patient);
        await _dataContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Patient patient, CancellationToken cancellationToken = default)
    {
        patient.IsEnabled = false;
        await UpdateAsync(patient, cancellationToken);
    }

    public async Task PermanentDelete(Patient patient, CancellationToken cancellationToken = default)
    {
        _dataContext.Patients.Remove(patient);
        await _dataContext.SaveChangesAsync(cancellationToken);
    }   
}
