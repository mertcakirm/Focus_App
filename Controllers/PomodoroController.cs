using Focus_App.Models;
using Focus_App.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Focus_App.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PomodoroController : ControllerBase
{
    private readonly IPomodoroRepository _pomodoroRepository;
    public PomodoroController(IPomodoroRepository pomodoroRepository) => _pomodoroRepository = pomodoroRepository;

    private int GetUserId() => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

    [HttpPost("start")]
    public async Task<IActionResult> Start([FromBody] PomodoroSession session)
    {
        session.UserId = GetUserId();
        var result = await _pomodoroRepository.StartSessionAsync(session);
        return Ok(result);
    }

    [HttpPost("end/{id}")]
    public async Task<IActionResult> End(int id)
    {
        var result = await _pomodoroRepository.EndSessionAsync(id, GetUserId());
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet("history")]
    public async Task<IActionResult> GetAll()
    {
        var data = await _pomodoroRepository.GetUserSessionsAsync(GetUserId());
        return Ok(data);
    }
}