using Application.Assessment.Search.Search;
using Application.UseCases.AppoAssessmentintment.Create;
using Application.UseCases.Assessment;
using Application.UseCases.Assessment.Create;
using Application.UseCases.Assessment.Create.Interfaces;
using Application.UseCases.Assessment.Delete;
using Application.UseCases.Assessment.Delete.Interfaces;
using Application.UseCases.Assessment.DeletePermanently;
using Application.UseCases.Assessment.DeletePermanently.Interfaces;
using Application.UseCases.Assessment.Get;
using Application.UseCases.Assessment.Get.Interfaces;
using Application.UseCases.Assessment.Interfaces;
using Application.UseCases.Assessment.Search.Interfaces;
using Application.UseCases.Doctor.Create;
using Application.UseCases.Doctor.Create.Interfaces;
using Application.UseCases.Doctor.Delete;
using Application.UseCases.Doctor.Delete.Interfaces;
using Application.UseCases.Doctor.DeletePermanently;
using Application.UseCases.Doctor.DeletePermanently.Interfaces;
using Application.UseCases.Doctor.Get;
using Application.UseCases.Doctor.Get.Interfaces;
using Application.UseCases.Doctor.Search;
using Application.UseCases.Doctor.Search.Interfaces;
using Application.UseCases.Doctor.Update.Interfaces;
using Application.UseCases.Patient.Create;
using Application.UseCases.Patient.Create.Interfaces;
using Application.UseCases.Patient.Delete;
using Application.UseCases.Patient.Delete.Interfaces;
using Application.UseCases.Patient.DeletePermanently;
using Application.UseCases.Patient.DeletePermanently.Interfaces;
using Application.UseCases.Patient.Get;
using Application.UseCases.Patient.Get.Interfaces;
using Application.UseCases.Patient.Search;
using Application.UseCases.Patient.Search.Interfaces;
using Application.UseCases.Patient.Update;
using Application.UseCases.Patient.Update.Interfaces;
using Application.UseCases.UpdateDoctor;
using Domain.Repositories.Relational;
using Infra.Persistence.Sql.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Assessments
builder.Services.AddSingleton<IAssessmentRepository, AssessmentRepository>();
builder.Services.AddSingleton<IGetAssessmentUseCase, GetAssessmentUseCase>();
builder.Services.AddSingleton<ISearchAssessmentUseCase, SearchAssessmentUseCase>();
builder.Services.AddSingleton<ICreateAssessmentProcessingUseCase, CreateAssessmentProcessingUseCase>();
builder.Services.AddScoped<ISendCreateAssessmentRequestUseCase, SendCreateAssessmentRequestUseCase>();
builder.Services.AddSingleton<IUpdateAssessmentProcessingUseCase, UpdateAssessmentProcessingUseCase>();
builder.Services.AddScoped<ISendUpdateAssessmentRequestUseCase, SendUpdateAssessmentRequestUseCase>();
builder.Services.AddSingleton<IDeleteAssessmentProcessingUseCase, DeleteAssessmentProcessingUseCase>();
builder.Services.AddScoped<ISendDeleteAssessmentRequestUseCase, SendDeleteAssessmentRequestUseCase>();
builder.Services.AddSingleton<IDeleteAssessmentPermanentlyProcessingUseCase, DeleteAssessmentPermanentlyProcessingUseCase>();
builder.Services.AddScoped<ISendDeleteAssessmentPermanentlyRequestUseCase, SendDeleteAssessmentPermanentlyProcessingUseCase>();

// Doctors
builder.Services.AddSingleton<IDoctorRepository, DoctorRepository>();
builder.Services.AddSingleton<IGetDoctorUseCase, GetDoctorUseCase>();
builder.Services.AddSingleton<ISearchDoctorUseCase, SearchDoctorUseCase>();
builder.Services.AddSingleton<ICreateDoctorProcessingUseCase, CreateDoctorProcessingUseCase>();
builder.Services.AddSingleton<IUpdateDoctorProcessingUseCase, UpdateDoctorProcessingUseCase>();
builder.Services.AddSingleton<IDeleteDoctorProcessingUseCase, DeleteDoctorProcessingUseCase>();
builder.Services.AddScoped<ISendDeleteDoctorRequestUseCase, SendDeleteDoctorRequestUseCase>();
builder.Services.AddSingleton<IDeleteDoctorPermanentlyProcessingUseCase, DeleteDoctorPermanentlyProcessingUseCase>();
builder.Services.AddScoped<ISendDeleteDoctorPermanentlyRequestUseCase, SendDeleteDoctorPermanentlyProcessingUseCase>();

// Patient
builder.Services.AddSingleton<IPatientRepository, PatientRepository>();
builder.Services.AddSingleton<IGetPatientUseCase, GetPatientUseCase>();
builder.Services.AddSingleton<ISearchPatientUseCase, SearchPatientUseCase>();
builder.Services.AddSingleton<ICreatePatientProcessingUseCase, CreatePatientProcessingUseCase>();
builder.Services.AddScoped<ISendCreatePatientRequestUseCase, SendCreatePatientRequestUseCase>();
builder.Services.AddSingleton<IUpdatePatientProcessingUseCase, UpdatePatientProcessingUseCase>();
builder.Services.AddScoped<ISendUpdatePatientRequestUseCase, SendUpdatePatientRequestUseCase>();
builder.Services.AddSingleton<IDeletePatientProcessingUseCase, DeletePatientProcessingUseCase>();
builder.Services.AddScoped<ISendDeletePatientRequestUseCase, SendDeletePatientRequestUseCase>();
builder.Services.AddSingleton<IDeletePatientPermanentlyProcessingUseCase, DeletePatientPermanentlyProcessingUseCase>();
builder.Services.AddScoped<ISendDeletePatientPermanentlyProcessingUseCase, SendDeletePatientPermanentlyProcessingUseCase>();

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
