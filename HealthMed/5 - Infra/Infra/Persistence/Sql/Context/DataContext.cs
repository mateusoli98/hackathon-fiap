using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Persistence.Sql.Context;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    { }

    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Appointments> Appointments { get; set; }
    public DbSet<Assessment> Assessment { get; set; }
    public DbSet<DoctorAvailability> DoctorAvailability { get; set; }
    public DbSet<HealthCenter> HealthCenter { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
