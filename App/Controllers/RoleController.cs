using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Domain.UseCases;
using App.Views;
using Domain.Models;

namespace App.Controllers;

[ApiController]
[Route("role")]
public class RoleController : ControllerBase
{
    private readonly RoleService _roleService;

    public RoleController(RoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet("{id}")]
    public ActionResult<RoleView> GetRole(int id)
    {
        var result = _roleService.GetRole(id);

        if (result.IsFailure)
        {
            return Problem(statusCode: 404, detail: result.Error);
        }

        return Ok(new RoleView(result.Value!));
    }

    [Authorize(Roles = "Administrator")]
    [HttpPost]
    public ActionResult<RoleView> CreateRole(string name)
    {
        var role = new Role { Name = name };

        var result = _roleService.CreateRole(role);

        if (result.IsFailure)
        {
            return Problem(statusCode: 404, detail: result.Error);
        }

        return Ok(new RoleView(result.Value!));
    }

    [Authorize(Roles = "Administrator")]
    [HttpPut("{id}")]
    public ActionResult<RoleView> UpdateRole(int id, string name)
    {
        var role = new Role { Id = id, Name = name };

        var result = _roleService.UpdateRole(role);

        if (result.IsFailure)
        {
            return Problem(statusCode: 404, detail: result.Error);
        }

        return Ok(new RoleView(result.Value!));
    }

    [Authorize(Roles = "Administrator")]
    [HttpDelete("{id}")]
    public ActionResult<RoleView> DeleteRole(int id)
    {
        var result = _roleService.DeleteRole(id);

        if (result.IsFailure)
        {
            return Problem(statusCode: 404, detail: result.Error);
        }

        return Ok(new RoleView(result.Value!));
    }
}
