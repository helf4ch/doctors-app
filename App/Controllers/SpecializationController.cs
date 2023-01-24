using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Domain.UseCases;
using App.Views;
using Domain.Models;

namespace App.Controllers;

[ApiController]
[Route("specialization")]
public class SpecializationController : ControllerBase
{
    private readonly SpecializationService _specializationService;

    public SpecializationController(SpecializationService specializationService)
    {
        _specializationService = specializationService;
    }

    [HttpGet("{id}")]
    public ActionResult<SpecializationView> GetSpecialization(int id)
    {
        var result = _specializationService.GetSpecialization(id);

        if (result.IsFailure)
        {
            return Problem(statusCode: 404, detail: result.Error);
        }

        return Ok(new SpecializationView(result.Value!));
    }

    [Authorize(Roles = "Administrator")]
    [HttpPost]
    public ActionResult<SpecializationView> CreateSpecialization(string name)
    {
        var specialization = new Specialization { Name = name };

        var result = _specializationService.CreateSpecialization(specialization);

        if (result.IsFailure)
        {
            return Problem(statusCode: 404, detail: result.Error);
        }

        return Ok(new SpecializationView(result.Value!));
    }

    [Authorize(Roles = "Administrator")]
    [HttpPut("{id}")]
    public ActionResult<SpecializationView> UpdateSpecialization(int id, string name)
    {
        var specialization = new Specialization { Id = id, Name = name };

        var result = _specializationService.UpdateSpecialization(specialization);

        if (result.IsFailure)
        {
            return Problem(statusCode: 404, detail: result.Error);
        }

        return Ok(new SpecializationView(result.Value!));
    }

    [Authorize(Roles = "Administrator")]
    [HttpDelete("{id}")]
    public ActionResult<SpecializationView> DeleteSpecialization(int id)
    {
        var result = _specializationService.DeleteSpecialization(id);

        if (result.IsFailure)
        {
            return Problem(statusCode: 404, detail: result.Error);
        }

        return Ok(new SpecializationView(result.Value!));
    }
}
