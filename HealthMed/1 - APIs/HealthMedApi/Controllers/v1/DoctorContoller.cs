using Application.UseCases.Doctor.Create.Commom;
using Application.UseCases.Doctor.Create.Interfaces;
using Application.UseCases.Doctor.Delete.Interfaces;
using Application.UseCases.Doctor.DeletePermanently.Interfaces;
using Application.UseCases.Doctor.Get.Common;
using Application.UseCases.Doctor.Get.Interfaces;
using Application.UseCases.Doctor.Login;
using Application.UseCases.Doctor.Update.Common;
using Application.UseCases.Doctor.Update.Interfaces;
using CreateAPI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using LoginRequest = Application.UseCases.Doctor.Login.LoginRequest;

namespace HealthMedApi.Controllers.v1;

[Route("api/doctor")]
[ApiController]
public class DoctorContoller(
    ICreateDoctorProcessingUseCase createDoctorProcessingUseCase,
    IGetDoctorUseCase getDoctorUseCase,
    IUpdateDoctorProcessingUseCase updateDoctorProcessingUseCase,
    ISendDeleteDoctorRequestUseCase deleteDoctorProcessingUseCase,
    ISendDeleteDoctorPermanentlyRequestUseCase deleteDoctorPermanentlyUsecase,
    IPatientLoginUsecase doctorLoginUsecase,
    IConfiguration configuration) : BaseController
{
    private readonly ICreateDoctorProcessingUseCase _createDoctorProcessingUsecase = createDoctorProcessingUseCase;
    private readonly IGetDoctorUseCase _getDoctorUsecase = getDoctorUseCase;
    private readonly IUpdateDoctorProcessingUseCase _updateDoctorProcessingUseCase = updateDoctorProcessingUseCase;
    private readonly ISendDeleteDoctorRequestUseCase _deleteDoctorProcessingUseCase = deleteDoctorProcessingUseCase;
    private readonly ISendDeleteDoctorPermanentlyRequestUseCase _deleteDoctorPermanentlyUsecase = deleteDoctorPermanentlyUsecase;
    private readonly IPatientLoginUsecase _doctorLoginUsecase = doctorLoginUsecase;
    private readonly IConfiguration _configuration = configuration;

    [HttpPost]
    public async Task<ActionResult<CreateDoctorResponse>> CreateDoctor([FromBody] CreateDoctorRequest request)
    {
        return await Result(_createDoctorProcessingUsecase.Execute(request));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetDoctorResponse>> GetDoctor(long id)
    {
        return await Result(_getDoctorUsecase.Execute(id));
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Doctor")]
    public async Task<ActionResult<UpdateDoctorResponse>> UpdateDoctor(long id, [FromBody] UpdateDoctorRequest request)
    {
        return await Result(_updateDoctorProcessingUseCase.Execute(id, request));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Doctor")]
    public async Task<ActionResult> SoftDeleteDoctor(long id)
    {
        return await Result(_deleteDoctorProcessingUseCase.Execute(id));
    }

    // Método para deletar um médico (hard delete)
    [HttpDelete("{id}/hard")]
    [Authorize(Roles = "Doctor")]
    public async Task<ActionResult> HardDeleteDoctor(long id)
    {
        return await Result(_deleteDoctorPermanentlyUsecase.Execute(id));
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var result = await _doctorLoginUsecase.ValidateCredentialsAsync(loginRequest);
        if (result is null)
        {
            return Error("Login failed", HttpStatusCode.Unauthorized);
        }

        var token = GenerateJwtToken(result.Id, result.CRM);
        return Ok(new { Token = token });
    }

    private string GenerateJwtToken(long id, string crm)
    {
        var claims = new[]
        {
            new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Sub, id.ToString()),
            new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Sub, crm),
            new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt-Doctor:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt-Doctor:Issuer"],
            audience: _configuration["Jwt-Doctor:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
