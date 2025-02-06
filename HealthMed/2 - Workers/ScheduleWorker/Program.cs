using Domain.Repositories.Relational;
using Infra.Migrations;
using Infra.Persistence.Sql.Context;
using Infra.Persistence.Sql.Repositories;
using Infra.Services.Messages;
using Microsoft.EntityFrameworkCore;

namespace ScheduleWorker;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTIONSTRING");
        if (string.IsNullOrEmpty(connectionString))
        {
            Console.WriteLine("Erro: DB_CONNECTIONSTRING não foi configurada.");
        }

        builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));
        builder.Services.AddScoped<DataContext>();

        builder.Services.AddSingleton<IAppointmentRepository, AppointmentRepository>();
        builder.Services.AddSingleton<IDoctorRepository, DoctorRepository>();
        builder.Services.AddSingleton<IPatientRepository, PatientRepository>();

        builder.Services.AddSingleton<IRabbitMqProducerService, RabbitMqProducerService>();

        builder.Services.AddSingleton<IUseCase, UseCase>();

        builder.Services.AddHostedService<Worker>();

        var host = builder.Build();
        MigrationHelper.ApplyMigrations<DataContext>(host);
        host.Run();
    }
}
