using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Domain.UseCases;
using App.Views;
using Domain.Models;

namespace App.Controllers;

[ApiController]
[Route("doctor")]
public class DoctorController : ControllerBase
{
    private readonly DoctorService _doctorService;

    public DoctorController(DoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    [HttpGet("{id}")]
    public ActionResult<DoctorView> GetDoctor(int id)
    {
        var result = _doctorService.GetDoctor(id);

        if (result.IsFailure)
        {
            return Problem(statusCode: 404, detail: result.Error);
        }

        return Ok(new DoctorView(result.Value!));
    }

    [Authorize(Roles = "Administator")]
    [HttpPost]
    public ActionResult<DoctorView> CreateDoctor(
        string name,
        string? secondname,
        string? surname,
        int specializationId,
        int appointmentTimeMinutes
    )
    {
        var doctor = new Doctor
        {
            Name = name,
            Secondname = secondname,
            Surname = surname,
            SpecializationId = specializationId,
            AppointmentTimeMinutes = appointmentTimeMinutes
        };

        var result = _doctorService.CreateDoctor(doctor);

        if (result.IsFailure)
        {
            return Problem(statusCode: 404, detail: result.Error);
        }

        return Ok(new DoctorView(result.Value!));
    }

    [Authorize(Roles = "Administator")]
    [HttpPut("{id}")]
    public ActionResult<DoctorView> UpdateDoctor(
        int id,
        string name,
        string secondname,
        string surname,
        int specializationId,
        int appointmentTimeMinutes
    )
    {
        var doctor = new Doctor
        {
            Id = id,
            Name = name,
            Secondname = secondname,
            Surname = surname,
            SpecializationId = specializationId,
            AppointmentTimeMinutes = appointmentTimeMinutes
        };

        var result = _doctorService.UpdateDoctor(doctor);

        if (result.IsFailure)
        {
            return Problem(statusCode: 404, detail: result.Error);
        }

        return Ok(new DoctorView(result.Value!));
    }

    [Authorize(Roles = "Administator")]
    [HttpDelete("{id}")]
    public ActionResult<DoctorView> DeleteDoctor(int id)
    {
        var result = _doctorService.DeleteDoctor(id);

        if (result.IsFailure)
        {
            return Problem(statusCode: 404, detail: result.Error);
        }

        return Ok(new DoctorView(result.Value!));
    }

    [HttpGet("all")]
    public ActionResult<List<DoctorView>> GetAllDoctors()
    {
        var result = _doctorService.GetAllDoctors();

        if (result.IsFailure)
        {
            return Problem(statusCode: 404, detail: result.Error);
        }

        List<DoctorView> items = new List<DoctorView>();
        foreach (var i in result.Value!)
        {
            items.Add(new DoctorView(i));
        }

        return Ok(items);
    }

    [HttpGet("all")]
    public ActionResult<List<DoctorView>> GetAllDoctorsBySpecialization(int specializationId)
    {
        var result = _doctorService.GetAllDoctorsBySpecialization(specializationId);

        if (result.IsFailure)
        {
            return Problem(statusCode: 404, detail: result.Error);
        }

        List<DoctorView> items = new List<DoctorView>();
        foreach (var it in result.Value!)
        {
            items.Add(new DoctorView(it));
        }

        return Ok(items);
    }
}
