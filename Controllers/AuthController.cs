using Focus_App.Models;
using Focus_App.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace Focus_App.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _config;

    public AuthController(IUserRepository userRepository, IConfiguration config)
    {
        _userRepository = userRepository;
        _config = config;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto request)
    {
        if (await _userRepository.EmailExistsAsync(request.Email))
            return BadRequest("Email zaten kayıtlı.");

        string hashedPassword = ComputeSha256Hash(request.Password);

        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = hashedPassword,
            JoinDate = DateTime.UtcNow,
            IsPremium = false
        };

        await _userRepository.RegisterAsync(user);

        return Ok("Kayıt başarılı.");
    }

	[AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user == null)
            return Unauthorized("Email veya şifre hatalı.");

        string hashedInput = ComputeSha256Hash(request.Password);

        if (user.PasswordHash != hashedInput)
            return Unauthorized("Email veya şifre hatalı.");

        var token = GenerateJwtToken(user);

        return Ok(new { token });
    }

    private string ComputeSha256Hash(string rawData)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
        return Convert.ToBase64String(bytes);
    }

private string GenerateJwtToken(User user)
{
    var keyString = _config["Jwt:Key"];
    var key = Encoding.UTF8.GetBytes(keyString);

    var tokenHandler = new JwtSecurityTokenHandler();
    var claims = new[]
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Name, user.Username)
    };

var tokenDescriptor = new SecurityTokenDescriptor
{
    Subject = new ClaimsIdentity(claims),
    Expires = DateTime.UtcNow.AddDays(7),
    Issuer = _config["Jwt:Issuer"],      
    Audience = _config["Jwt:Audience"],   
    SigningCredentials = new SigningCredentials(
        new SymmetricSecurityKey(key),
        SecurityAlgorithms.HmacSha256Signature
    )
};

    var token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);
}
}