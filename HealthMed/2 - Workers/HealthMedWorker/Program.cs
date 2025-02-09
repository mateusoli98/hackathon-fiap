using Application.UseCases.Doctor.Delete;
using Application.UseCases.Doctor.Delete.Interfaces;
using Application.UseCases.Doctor.DeletePermanently;
using Application.UseCases.Doctor.DeletePermanently.Interfaces;
using Application.UseCases.Patient.Delete;
using Application.UseCases.Patient.Delete.Interfaces;
using Application.UseCases.Patient.DeletePermanently;
using Application.UseCases.Patient.DeletePermanently.Interfaces;
using Domain.Repositories.Relational;
using Infra.Migrations;
using Infra.Persistence.Sql.Context;
using Infra.Persistence.Sql.Repositories;
using Infra.Services.Messages;
using Microsoft.EntityFrameworkCore;

namespace HealthMedWorker;

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

        builder.Services.AddSingleton<IDoctorRepository, DoctorRepository>();
        builder.Services.AddSingleton<IPatientRepository, PatientRepository>();
        
        builder.Services.AddSingleton<IDeleteDoctorProcessingUseCase, DeleteDoctorProcessingUseCase>();
        builder.Services.AddSingleton<IDeleteDoctorPermanentlyProcessingUseCase, DeleteDoctorPermanentlyProcessingUseCase>();
        builder.Services.AddSingleton<IDeletePatientProcessingUseCase, DeletePatientProcessingUseCase>();
        builder.Services.AddSingleton<IDeletePatientPermanentlyProcessingUseCase, DeletePatientPermanentlyProcessingUseCase>();

        builder.Services.AddSingleton<IRabbitMqProducerService, RabbitMqProducerService>();
        
        builder.Services.AddHostedService<Worker>();

        var host = builder.Build();
        MigrationHelper.ApplyMigrations<DataContext>(host);
        host.Run();
    }
}
