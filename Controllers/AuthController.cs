using Microsoft.AspNetCore.Mvc;
using backend_jwt_auth.Models.DTOs;
using backend_jwt_auth.Services;

namespace backend_jwt_auth.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase {
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService) {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request) {
        var success = await _authService.RegisterAsync(request);

        if (!success) {
            return BadRequest(new { message = "Email is already registered."});
        }

        return Ok(new { message = "User registered successfully."});
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request) {
        var token = await _authService.LoginAsync(request);
        if (token == null) {
            return Unauthorized(new { message = "Invalid credentials" });
        }

        return Ok(new { token });
    }
}