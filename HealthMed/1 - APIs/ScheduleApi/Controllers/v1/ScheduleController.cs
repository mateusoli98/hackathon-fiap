using Application.Appointment.Update.Common;
using Application.UseCases.Appointment.Common;
using Application.UseCases.Appointment.Create.Commom;
using Application.UseCases.Appointment.Create.Interfaces;
using Application.UseCases.Appointment.Delete.Interfaces;
using Application.UseCases.Appointment.DeletePermanently.Interfaces;
using Application.UseCases.Appointment.Get.Common;
using Application.UseCases.Appointment.Get.Interfaces;
using Application.UseCases.Appointment.Interfaces;
using Application.UseCases.Appointment.Search.Common;
using Application.UseCases.Appointment.Search.Interfaces;
using CreateAPI.Controllers;
using Domain.DomainObjects.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ScheduleApi.Controllers.v1;

[Route("api/[controller]")]
[ApiController]
public class ScheduleController(
    ICreateAppointmentProcessingUseCase createAppointmentProcessingUseCase,
    IGetAppointmentUseCase getAppointmentUseCase,
    ISearchAppointmentUseCase searchAppointmentUseCase,
    IUpdateAppointmentProcessingUseCase updateAppointmentProcessingUseCase,
    ISendDeleteAppointmentRequestUseCase deleteAppointmentProcessingUseCase,
    ISendDeleteAppointmentPermanentlyRequestUseCase deleteAppointmentPermanentlyUsecase
    ) : BaseController
{
    private readonly ICreateAppointmentProcessingUseCase _createAppointmentProcessingUsecase = createAppointmentProcessingUseCase;
    private readonly IGetAppointmentUseCase _getAppointmentUsecase = getAppointmentUseCase;
    private readonly ISearchAppointmentUseCase _searchAppointmentUse = searchAppointmentUseCase;
    private readonly IUpdateAppointmentProcessingUseCase _updateAppointmentProcessingUseCase = updateAppointmentProcessingUseCase;
    private readonly ISendDeleteAppointmentRequestUseCase _deleteAppointmentProcessingUseCase = deleteAppointmentProcessingUseCase;
    private readonly ISendDeleteAppointmentPermanentlyRequestUseCase _deleteAppointmentPermanentlyUsecase = deleteAppointmentPermanentlyUsecase;    

    [HttpPost]
    public async Task<ActionResult<CreateAppointmentResponse>> CreateAppointment([FromBody] CreateAppointmentRequest request)
    {
        return await Result(_createAppointmentProcessingUsecase.Execute(request));
    }

    [HttpGet]
    public async Task<ActionResult<PaginationResult<SearchAppointmentResponse>>> GetAppointments([FromQuery] AppointmentFilter filter)
    {
        return await Result(_searchAppointmentUse.Execute(filter));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetAppointmentResponse>> GetAppointment(long id)
    {
        return await Result(_getAppointmentUsecase.Execute(id));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UpdateAppointmentResponse>> UpdateAppointment(long id, [FromBody] UpdateAppointmentRequest request)
    {
        return await Result(_updateAppointmentProcessingUseCase.Execute(id, request));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAppointment(long id)
    {
        return await Result(_deleteAppointmentProcessingUseCase.Execute(id));
    }

    [HttpDelete("{id}/hard")]
    public async Task<ActionResult> DeleteAppointmentPermanently(long id)
    {
        return await Result(_deleteAppointmentPermanentlyUsecase.Execute(id));
    }
}
