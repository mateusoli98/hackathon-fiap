using Application.Appointment.Search.Search;
using Application.UseCases.Appointment.Create.Interfaces;
using Application.UseCases.Appointment.Create;
using Application.UseCases.Appointment.Delete.Interfaces;
using Application.UseCases.Appointment.Delete;
using Application.UseCases.Appointment.DeletePermanently.Interfaces;
using Application.UseCases.Appointment.DeletePermanently;
using Application.UseCases.Appointment.Get.Interfaces;
using Application.UseCases.Appointment.Get;
using Application.UseCases.Appointment.Interfaces;
using Application.UseCases.Appointment.Search.Interfaces;
using Application.UseCases.Appointment;
using Domain.Repositories.Relational;
using Infra.Persistence.Sql.Repositories;
using Infra.Services.Messages;
using Application.UseCases.Doctor.Get.Interfaces;
using Application.UseCases.Doctor.Get;
using Application.UseCases.Patient.Get.Interfaces;
using Application.UseCases.Patient.Get;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// RabbitMQ
builder.Services.AddSingleton<IRabbitMqProducerService, RabbitMqProducerService>();

// Appointments
builder.Services.AddSingleton<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddSingleton<IGetAppointmentUseCase, GetAppointmentUseCase>();
builder.Services.AddSingleton<ISearchAppointmentUseCase, SearchAppointmentUseCase>();
builder.Services.AddSingleton<ICreateAppointmentProcessingUseCase, CreateAppointmentProcessingUseCase>();
builder.Services.AddSingleton<ISendCreateAppointmentRequestUseCase, SendCreateAppointmentRequestUseCase>();
builder.Services.AddSingleton<IUpdateAppointmentProcessingUseCase, UpdateAppointmentProcessingUseCase>();
builder.Services.AddSingleton<ISendUpdateAppointmentRequestUseCase, SendUpdateAppointmentRequestUseCase>();
builder.Services.AddSingleton<IDeleteAppointmentProcessingUseCase, DeleteAppointmentProcessingUseCase>();
builder.Services.AddSingleton<ISendDeleteAppointmentRequestUseCase, SendDeleteAppointmentRequestUseCase>();
builder.Services.AddSingleton<IDeleteAppointmentPermanentlyProcessingUseCase, DeleteAppointmentPermanentlyProcessingUseCase>();
builder.Services.AddSingleton<ISendDeleteAppointmentPermanentlyRequestUseCase, SendDeleteAppointmentPermanentlyProcessingUseCase>();

// Doctor
builder.Services.AddSingleton<IDoctorRepository, DoctorRepository>();
builder.Services.AddSingleton<IGetDoctorUseCase, GetDoctorUseCase>();

// Patient
builder.Services.AddSingleton<IPatientRepository, PatientRepository>();
builder.Services.AddSingleton<IGetPatientUseCase, GetPatientUseCase>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
