using Domain.DomainObjects.Filters;
using Domain.Entities;
using Domain.Repositories.Relational;
using Infra.Extensions;
using Infra.Persistence.Sql.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Persistence.Sql.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
    private DataContext _dataContext;

    public AppointmentRepository(IServiceScopeFactory scopeFactory)
    {
        _dataContext = scopeFactory.CreateScope().ServiceProvider.GetRequiredService<DataContext>();
    }

    public async Task<Appointments?> GetByIdAsync(long id, CancellationToken cancellationToken = default, bool IsEnable = true)
    {
        var query = _dataContext.Appointments.AsQueryable()
            .Where(q => q.Id == id && q.IsEnabled == true)
            .FirstOrDefaultAsync(cancellationToken);

        return await query;
    }

    public async Task<PaginationResult<Appointments>> SearchAsync(AppointmentFilter filter, CancellationToken cancellationToken = default)
    {
        var query = _dataContext.Appointments.AsQueryable();

        if (filter.Status.HasValue)
        {
            query = query.Where(q => q.Status == filter.Status);
        }

        if (filter.Doctor is not null)
        {
            query = query.Where(q => q.Doctor.Id == filter.Doctor.Id);
        }

        if (filter.Patient is not null)
        {
            query = query.Where(q => q.Patient.Id == filter.Patient.Id);
        }

        if (filter.AppointmentDate.HasValue)
        {
            var appointmentDate = filter.AppointmentDate.Value.Date;
            query = query.Where(q => q.AppointmentDate.Date == appointmentDate);
        }

        var result = await query
            .OrderBy(q => q.CreatedAt)
            .AsNoTracking()
            .ToPaginatedAsync(filter);

        return result;
    }

    public async Task<long> SaveAsync(Appointments appointment, CancellationToken cancellationToken = default)
    {
        await _dataContext.Appointments.AddAsync(appointment, cancellationToken);
        await _dataContext.SaveChangesAsync(cancellationToken);

        return appointment.Id;
    }

    public async Task UpdateAsync(Appointments appointment, CancellationToken cancellationToken = default)
    {
        appointment.UpdatedAt = DateTime.UtcNow;
        _dataContext.Appointments.Update(appointment);
        await _dataContext.SaveChangesAsync(cancellationToken);
    }

    public Task DeleteAsync(Appointments appointment, CancellationToken cancellationToken = default)
    {
        appointment.IsEnabled = false;
        return UpdateAsync(appointment, cancellationToken);
    }

    public async Task PermanentDelete(Appointments appointment, CancellationToken cancellationToken = default)
    {
        _dataContext.Remove(appointment);
        await _dataContext.SaveChangesAsync(cancellationToken);
    }
}
