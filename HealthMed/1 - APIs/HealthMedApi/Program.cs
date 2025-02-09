using Application.UseCases.Doctor.Create.Interfaces;
using Application.UseCases.Doctor.Create;
using Application.UseCases.Doctor.Delete.Interfaces;
using Application.UseCases.Doctor.Delete;
using Application.UseCases.Doctor.DeletePermanently.Interfaces;
using Application.UseCases.Doctor.DeletePermanently;
using Application.UseCases.Doctor.Get.Interfaces;
using Application.UseCases.Doctor.Get;
using Application.UseCases.Doctor.Search.Interfaces;
using Application.UseCases.Doctor.Search;
using Application.UseCases.Doctor.Update.Interfaces;
using Application.UseCases.Patient.Create.Interfaces;
using Application.UseCases.Patient.Create;
using Application.UseCases.Patient.Delete.Interfaces;
using Application.UseCases.Patient.Delete;
using Application.UseCases.Patient.DeletePermanently.Interfaces;
using Application.UseCases.Patient.DeletePermanently;
using Application.UseCases.Patient.Get.Interfaces;
using Application.UseCases.Patient.Get;
using Application.UseCases.Patient.Search.Interfaces;
using Application.UseCases.Patient.Search;
using Application.UseCases.Patient.Update.Interfaces;
using Application.UseCases.Patient.Update;
using Application.UseCases.UpdateDoctor;
using Domain.Repositories.Relational;
using Infra.Persistence.Sql.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Infra.Services.Messages;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// RabbitMQ
builder.Services.AddSingleton<IRabbitMqProducerService, RabbitMqProducerService>();

// Doctors
builder.Services.AddSingleton<IDoctorRepository, DoctorRepository>();
builder.Services.AddSingleton<IGetDoctorUseCase, GetDoctorUseCase>();
builder.Services.AddSingleton<ISearchDoctorUseCase, SearchDoctorUseCase>();
builder.Services.AddSingleton<ICreateDoctorProcessingUseCase, CreateDoctorProcessingUseCase>();
builder.Services.AddSingleton<IUpdateDoctorProcessingUseCase, UpdateDoctorProcessingUseCase>();
builder.Services.AddSingleton<IDeleteDoctorProcessingUseCase, DeleteDoctorProcessingUseCase>();
builder.Services.AddSingleton<ISendDeleteDoctorRequestUseCase, SendDeleteDoctorRequestUseCase>();
builder.Services.AddSingleton<IDeleteDoctorPermanentlyProcessingUseCase, DeleteDoctorPermanentlyProcessingUseCase>();
builder.Services.AddSingleton<ISendDeleteDoctorPermanentlyRequestUseCase, SendDeleteDoctorPermanentlyProcessingUseCase>();

// Patient
builder.Services.AddSingleton<IPatientRepository, PatientRepository>();
builder.Services.AddSingleton<IGetPatientUseCase, GetPatientUseCase>();
builder.Services.AddSingleton<ISearchPatientUseCase, SearchPatientUseCase>();
builder.Services.AddSingleton<ICreatePatientProcessingUseCase, CreatePatientProcessingUseCase>();
builder.Services.AddSingleton<IUpdatePatientProcessingUseCase, UpdatePatientProcessingUseCase>();
builder.Services.AddSingleton<IDeletePatientProcessingUseCase, DeletePatientProcessingUseCase>();
builder.Services.AddSingleton<ISendDeletePatientRequestUseCase, SendDeletePatientRequestUseCase>();
builder.Services.AddSingleton<IDeletePatientPermanentlyProcessingUseCase, DeletePatientPermanentlyProcessingUseCase>();
builder.Services.AddSingleton<ISendDeletePatientPermanentlyProcessingUseCase, SendDeletePatientPermanentlyProcessingUseCase>();

// JWT config
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer("Doctor", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt-Doctor:Issuer"],
        ValidAudience = builder.Configuration["Jwt-Doctor:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt-Doctor:Key"]!))
    };
})
.AddJwtBearer("Patient", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt-Patient:Issuer"],
        ValidAudience = builder.Configuration["Jwt-Patient:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt-Patient:Key"]!))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
