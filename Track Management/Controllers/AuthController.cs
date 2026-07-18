using Application.DTOs.Auth;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Track_Management.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly IConfiguration _configuration;

    public AuthController(ITokenService tokenService, IConfiguration configuration)
    {
        _tokenService = tokenService;
        _configuration = configuration;
    }

    [HttpPost("token")]
    public ActionResult<TokenResponseDto> Token(LoginDto dto)
    {
        var username = _configuration["AuthCredentials:Username"];
        var password = _configuration["AuthCredentials:Password"];

        if (dto.Username != username || dto.Password != password)
            return Unauthorized(new { message = "Invalid credentials." });

        return Ok(_tokenService.GenerateToken(dto.Username));
    }
}
