using Day1Task1.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;


namespace Day1Task1.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;

    public AuthController(IConfiguration config)
    {
        _config = config;
    }

    [HttpPost("login")]
    public IActionResult Login(LoginDTO dto)
    {
        if (dto.Email != "test@test.com" || dto.Password != "password123")
        {
            return Unauthorized(new ProblemDetails
            {
                Status = StatusCodes.Status401Unauthorized,
                Title = "Invalid credentials"
            });
        }

        var claims = new[]
        {
            new Claim("userId", "1"),
            new Claim(ClaimTypes.Email, dto.Email),
            new Claim(ClaimTypes.Role, "Admin")
        };

        var secret = _config["Jwt:SecretKey"]
            ?? throw new InvalidOperationException("JWT secret not configured.");

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(secret));

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(8),
            signingCredentials: new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256));

        var tokenString =
            new JwtSecurityTokenHandler().WriteToken(token);

        return Ok(new { token = tokenString });
    }
    
[Authorize]
[HttpGet("me")]
public IActionResult Me()
{
    var userId = User.FindFirstValue("userId");
    var email = User.FindFirstValue(ClaimTypes.Email);
    var role = User.FindFirstValue(ClaimTypes.Role);

    return Ok(new
    {
        userId,
        email,
        role
    });
}
}