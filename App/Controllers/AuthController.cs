using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using App.Views;
using Domain.UseCases;
using Domain.Models;

namespace App.Controllers;

[ApiController]
[Route("authorization")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly UserService _userService;
    private readonly RoleService _roleService;

    public AuthController(
        IConfiguration configuration,
        UserService userService,
        RoleService roleService
    )
    {
        _configuration = configuration;
        _userService = userService;
        _roleService = roleService;
    }

    [HttpGet("login")]
    public async Task<ActionResult<UserView>> LogIn(string phoneNumber, string password)
    {
        var user = await _userService.Authorization(phoneNumber, password);

        if (user.IsFailure)
        {
            return Problem(statusCode: 404, detail: user.Error);
        }

        var role = await _roleService.GetRole(user.Value!.RoleId);

        if (role.IsFailure)
        {
            return Problem(statusCode: 404, detail: role.Error);
        }

        return Ok(new AuthView(user.Value, GenerateJwt(user.Value, role.Value!)));
    }

    [HttpPost("signup")]
    public async Task<ActionResult> SingUp(
        string phoneNumber,
        string name,
        string? secondname,
        string? surname,
        string password
    )
    {
        var user = new User
        {
            PhoneNumber = phoneNumber,
            Name = name,
            Secondname = secondname,
            Surname = surname,
            RoleId = 1,
            Password = password
        };

        var result = await _userService.Registration(user);

        if (result.IsFailure)
        {
            return Problem(statusCode: 404, detail: result.Error);
        }

        return Ok();
    }

    private string GenerateJwt(User user, Role role)
    {
        var claims = new List<Claim>
        {
            new Claim("Id", user.Id.ToString()),
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.PhoneNumber!),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, role.Name!)
        };

        var securityKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(_configuration["Jwt:Key"])
        );

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
