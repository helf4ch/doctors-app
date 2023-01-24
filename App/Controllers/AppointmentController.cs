using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Domain.UseCases;
using App.Views;
using Domain.Models;

namespace App.Controllers;

[ApiController]
[Route("appointment")]
public class AppointmentController : ControllerBase
{
    private readonly AppointmentService _appointmentService;
    private readonly DoctorService _doctorService;
    private readonly ScheduleService _scheduleService;

    public AppointmentController(
        AppointmentService appointmentService,
        DoctorService doctorService,
        ScheduleService scheduleService
    )
    {
        _appointmentService = appointmentService;
        _doctorService = doctorService;
        _scheduleService = scheduleService;
    }

    [Authorize(Roles = "Administrator")]
    [HttpGet("{id}")]
    public async Task<ActionResult<AppointmentView>> GetAppointment(int id)
    {
        var result = await _appointmentService.GetAppointment(id);

        if (result.IsFailure)
        {
            return Problem(statusCode: 404, detail: result.Error);
        }

        return Ok(new AppointmentView(result.Value!));
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<AppointmentView>> CreateAppointment(
        int doctorId,
        [FromQuery] DateOnly date,
        [FromQuery] TimeOnly startTime
    )
    {
        var appointment = new Appointment
        {
            UserId = Int32.Parse(HttpContext.User.FindFirst("Id")!.Value),
            DoctorId = doctorId,
            Date = date,
            StartTime = startTime
        };

        var doctor = await _doctorService.GetDoctor(doctorId);

        if (doctor.IsFailure)
        {
            return Problem(statusCode: 404, detail: doctor.Error);
        }

        var schedule = await _scheduleService.GetScheduleByDate(doctorId, date);

        if (schedule.IsFailure)
        {
            return Problem(statusCode: 404, detail: schedule.Error);
        }

        var result = await _appointmentService.CreateAppointment(
            appointment,
            doctor.Value!,
            schedule.Value!
        );

        if (result.IsFailure)
        {
            return Problem(statusCode: 404, detail: result.Error);
        }

        return Ok(new AppointmentView(result.Value));
    }

    [Authorize(Roles = "Administrator")]
    [HttpPut("{id}")]
    public async Task<ActionResult<AppointmentView>> UpdateAppointment(
        int id,
        int userId,
        int doctorId,
        [FromQuery] DateOnly date,
        [FromQuery] TimeOnly startTime
    )
    {
        var appointment = new Appointment
        {
            Id = id,
            UserId = userId,
            DoctorId = doctorId,
            Date = date,
            StartTime = startTime
        };

        var result = await _appointmentService.UpdateAppointment(appointment);

        if (result.IsFailure)
        {
            return Problem(statusCode: 404, detail: result.Error);
        }

        return Ok(new AppointmentView(result.Value));
    }

    [Authorize(Roles = "Administrator")]
    [HttpDelete("{id}")]
    public async Task<ActionResult<AppointmentView>> DeleteAppointment(int id)
    {
        var result = await _appointmentService.DeleteAppointment(id);

        if (result.IsFailure)
        {
            return Problem(statusCode: 404, detail: result.Error);
        }

        return Ok(new AppointmentView(result.Value));
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<AppointmentView>>> GetAllAppointmentsBySpecialization(
        int specializationId,
        [FromQuery] DateOnly date
    )
    {
        var result = await _appointmentService.GetAllAppointmentsBySpecialization(
            specializationId,
            date
        );

        if (result.IsFailure)
        {
            return Problem(statusCode: 404, detail: result.Error);
        }

        List<AppointmentView> items = new List<AppointmentView>();
        foreach (var it in result.Value!)
        {
            items.Add(new AppointmentView(it));
        }

        return Ok(items);
    }
}
