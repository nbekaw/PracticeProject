using Application.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> CreateUserDto(RegisterDto dto)
    {
        await _userService.RegisterAsync(dto);
        return Ok("Registered");
    }

    [HttpPost("login")]
    public async Task<IActionResult> AuthResponseDto(LoginDto dto)
    {
        var token = await _userService.LoginAsync(dto);
        return Ok(new { token });
    }
}
