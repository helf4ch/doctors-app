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
    public ActionResult<ScheduleView> GetScheduleByDate(int doctorId, DateOnly date)
    {
        var result = _scheduleService.GetScheduleByDate(doctorId, date);

        if (result.IsFailure)
        {
            return Problem(statusCode: 404, detail: result.Error);
        }

        return Ok(new ScheduleView(result.Value!));
    }

    [Authorize(Roles = "Administrator")]
    [HttpPost]
    public ActionResult<ScheduleView> CreateSchedule(
        int doctorId,
        [FromQuery] DateOnly date,
        [FromQuery] TimeOnly startOfShift,
        [FromQuery] TimeOnly endOfShift
    )
    {
        var schedule = new Schedule
        {
            DoctorId = doctorId,
            StartOfShift = startOfShift,
            EndOfShift = endOfShift
        };

        var result = _scheduleService.CreateSchedule(schedule);

        if (result.IsFailure)
        {
            return Problem(statusCode: 404, detail: result.Error);
        }

        return Ok(new ScheduleView(result.Value!));
    }

    [Authorize(Roles = "Administrator")]
    [HttpPut("{id}")]
    public ActionResult<ScheduleView> UpdateSchedule(
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

        var result = _scheduleService.UpdateSchedule(schedule);

        if (result.IsFailure)
        {
            return Problem(statusCode: 404, detail: result.Error);
        }

        return Ok(new ScheduleView(result.Value!));
    }

    [Authorize(Roles = "Administrator")]
    [HttpDelete("{id}")]
    public ActionResult<ScheduleView> DeleteSchedule(int id)
    {
        var result = _scheduleService.DeleteSchedule(id);

        if (result.IsFailure)
        {
            return Problem(statusCode: 404, detail: result.Error);
        }

        return Ok(new ScheduleView(result.Value!));
    }
}
