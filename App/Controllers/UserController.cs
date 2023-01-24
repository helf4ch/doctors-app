using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Domain.UseCases;
using App.Views;

namespace App.Controllers;

[ApiController]
[Route("user")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<UserView>> GetUser()
    {
        var id = GetId();

        var result = await _userService.GetUser(id);

        if (result.IsFailure)
        {
            return Problem(statusCode: 404, detail: result.Error);
        }

        return new UserView(result.Value);
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult<UserView>> UpdateUser(
        string name,
        string secondname,
        string surname
    )
    {
        var user = await _userService.GetUser(GetId());

        if (user.IsFailure)
        {
            return Problem(statusCode: 404, detail: user.Error);
        }

        user.Value!.Name = name;
        user.Value.Secondname = secondname;
        user.Value.Surname = surname;

        var result = await _userService.UpdateUser(user.Value);

        if (result.IsFailure)
        {
            return Problem(statusCode: 404, detail: result.Error);
        }

        return new UserView(result.Value);
    }

    private int GetId()
    {
        return Int32.Parse(HttpContext.User.FindFirst("Id")?.Value!);
    }
}
