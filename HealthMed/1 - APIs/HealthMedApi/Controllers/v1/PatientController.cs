using Application.UseCases.Patient.Create.Commom;
using Application.UseCases.Patient.Create.Interfaces;
using Application.UseCases.Patient.Delete.Interfaces;
using Application.UseCases.Patient.DeletePermanently.Interfaces;
using Application.UseCases.Patient.Get.Common;
using Application.UseCases.Patient.Get.Interfaces;
using Application.UseCases.Patient.Login;
using Application.UseCases.Patient.Update.Common;
using Application.UseCases.Patient.Update.Interfaces;
using CreateAPI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace HealthMedApi.Controllers.v1;

[Route("api/patient")]
[ApiController]
public class PatientController : BaseController
{
    private readonly ICreatePatientProcessingUseCase _createPatientProcessingUsecase;
    private readonly IGetPatientUseCase _getPatientUsecase;
    private readonly IUpdatePatientProcessingUseCase _updatePatientProcessingUseCase;
    private readonly ISendDeletePatientRequestUseCase _deletePatientProcessingUseCase;
    private readonly ISendDeletePatientPermanentlyProcessingUseCase _deletePatientPermanentlyUsecase;
    private readonly IPatientLoginUsecase _patientLoginUsecase;
    private readonly IConfiguration _configuration;

    public PatientController(
        ICreatePatientProcessingUseCase createPatientProcessingUseCase,
        IGetPatientUseCase getPatientUseCase,
        IUpdatePatientProcessingUseCase updatePatientProcessingUseCase,
        ISendDeletePatientRequestUseCase deletePatientProcessingUseCase,
        ISendDeletePatientPermanentlyProcessingUseCase deletePatientPermanentlyUsecase,
        IPatientLoginUsecase patientLoginUsecase,
        IConfiguration configuration)
    {
        _createPatientProcessingUsecase = createPatientProcessingUseCase;
        _getPatientUsecase = getPatientUseCase;
        _updatePatientProcessingUseCase = updatePatientProcessingUseCase;
        _deletePatientProcessingUseCase = deletePatientProcessingUseCase;
        _deletePatientPermanentlyUsecase = deletePatientPermanentlyUsecase;
        _patientLoginUsecase = patientLoginUsecase;
        _configuration = configuration;
    }

    [HttpPost]
    public async Task<ActionResult<CreatePatientResponse>> CreatePatient([FromBody] CreatePatientRequest request)
    {
        return await Result(_createPatientProcessingUsecase.Execute(request));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetPatientResponse>> GetPatient(long id)
    {
        return await Result(_getPatientUsecase.Execute(id));
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Patient")]
    public async Task<ActionResult<UpdatePatientResponse>> UpdatePatient(long id, [FromBody] UpdatePatientRequest request)
    {
        return await Result(_updatePatientProcessingUseCase.Execute(id, request));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Patient")]
    public async Task<ActionResult> SoftDeletePatient(long id)
    {
        return await Result(_deletePatientProcessingUseCase.Execute(id));
    }

    [HttpDelete("{id}/hard")]
    [Authorize(Roles = "Patient")]
    public async Task<ActionResult> HardDeletePatient(long id)
    {
        return await Result(_deletePatientPermanentlyUsecase.Execute(id));
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var result = await _patientLoginUsecase.ValidateCredentialsAsync(loginRequest);
        if (result is null)
        {
            return Error("Login failed", HttpStatusCode.Unauthorized);
        }

        var token = GenerateJwtToken(result.Id, result.Email);
        return Ok(new { Token = token });
    }

    private string GenerateJwtToken(long id, string email)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, id.ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, "Patient")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt-Patient:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt-Patient:Issuer"],
            audience: _configuration["Jwt-Patient:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
