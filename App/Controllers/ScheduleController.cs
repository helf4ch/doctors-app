using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Domain.UseCases;
using App.Views;
using Domain.Models;

namespace App.Controllers;

[ApiController]
[Route("schedule")]
public class ScheduleController : ControllerBase
{
    private readonly ScheduleService _scheduleService;

    public ScheduleController(ScheduleService scheduleService)
    {
        _scheduleService = scheduleService;
    }

    [HttpGet]
    public async Task<ActionResult<ScheduleView>> GetScheduleByDate(int doctorId, DateOnly date)
    {
        var result = await _scheduleService.GetScheduleByDate(doctorId, date);

        if (result.IsFailure)
        {
            return Problem(statusCode: 404, detail: result.Error);
        }

        return Ok(new ScheduleView(result.Value!));
    }

    [Authorize(Roles = "Administrator")]
    [HttpPost]
    public async Task<ActionResult<ScheduleView>> CreateSchedule(
        int doctorId,
        [FromQuery] DateOnly date,
        [FromQuery] TimeOnly startOfShift,
        [FromQuery] TimeOnly endOfShift
    )
    {
        var schedule = new Schedule
        {
            DoctorId = doctorId,
            Date = date,
            StartOfShift = startOfShift,
            EndOfShift = endOfShift
        };

        var result = await _scheduleService.CreateSchedule(schedule);

        if (result.IsFailure)
        {
            return Problem(statusCode: 404, detail: result.Error);
        }

        return Ok(new ScheduleView(result.Value!));
    }

    [Authorize(Roles = "Administrator")]
    [HttpPut("{id}")]
    public async Task<ActionResult<ScheduleView>> UpdateSchedule(
        int id,
        int doctorId,
        [FromQuery] DateOnly date,
        [FromQuery] TimeOnly startOfShift,
        [FromQuery] TimeOnly endOfShift
    )
    {
        var schedule = new Schedule
        {
            Id = id,
            DoctorId = doctorId,
            StartOfShift = startOfShift,
            EndOfShift = endOfShift
        };

        var result = await _scheduleService.UpdateSchedule(schedule);

        if (result.IsFailure)
        {
            return Problem(statusCode: 404, detail: result.Error);
        }

        return Ok(new ScheduleView(result.Value!));
    }

    [Authorize(Roles = "Administrator")]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ScheduleView>> DeleteSchedule(int id)
    {
        var result = await _scheduleService.DeleteSchedule(id);

        if (result.IsFailure)
        {
            return Problem(statusCode: 404, detail: result.Error);
        }

        return Ok(new ScheduleView(result.Value!));
    }
}
