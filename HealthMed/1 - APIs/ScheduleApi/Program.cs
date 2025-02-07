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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Appointments
builder.Services.AddSingleton<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddSingleton<IGetAppointmentUseCase, GetAppointmentUseCase>();
builder.Services.AddSingleton<ISearchAppointmentUseCase, SearchAppointmentUseCase>();
builder.Services.AddSingleton<ICreateAppointmentProcessingUseCase, CreateAppointmentProcessingUseCase>();
builder.Services.AddScoped<ISendCreateAppointmentRequestUseCase, SendCreateAppointmentRequestUseCase>();
builder.Services.AddSingleton<IUpdateAppointmentProcessingUseCase, UpdateAppointmentProcessingUseCase>();
builder.Services.AddScoped<ISendUpdateAppointmentRequestUseCase, SendUpdateAppointmentRequestUseCase>();
builder.Services.AddSingleton<IDeleteAppointmentProcessingUseCase, DeleteAppointmentProcessingUseCase>();
builder.Services.AddScoped<ISendDeleteAppointmentRequestUseCase, SendDeleteAppointmentRequestUseCase>();
builder.Services.AddSingleton<IDeleteAppointmentPermanentlyProcessingUseCase, DeleteAppointmentPermanentlyProcessingUseCase>();
builder.Services.AddScoped<ISendDeleteAppointmentPermanentlyRequestUseCase, SendDeleteAppointmentPermanentlyProcessingUseCase>();

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
