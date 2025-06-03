using Focus_App.DTOs;
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
    private readonly IPomodoroSessionRepository _pomodoroRepository;
    private readonly IFocusInsightRepository _insightRepo; // Ekleme

    public PomodoroController(IPomodoroSessionRepository pomodoroRepository, IFocusInsightRepository insightRepo)
    {
        _pomodoroRepository = pomodoroRepository;
        _insightRepo = insightRepo; // Constructor içinde ata
    }

    private int GetUserId() => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

    [HttpPost("start")]
    public async Task<IActionResult> Start([FromBody] PomodoroSessionDto dto)
    {
        var session = new PomodoroSession
        {
            UserId = GetUserId(),
            StartTime = DateTime.UtcNow,
            BreakUsed = dto.BreakUsed
        };

        var result = await _pomodoroRepository.StartSessionAsync(session);
        return Ok(result);
    }

[HttpPost("end/{id}")]
public async Task<IActionResult> End(int id)
{
    var result = await _pomodoroRepository.EndSessionAsync(id, GetUserId());
    if (result == null) return NotFound();

    await _insightRepo.UpdateMinutesAsync(GetUserId(), result.DurationMinutes);

    var responseDto = new PomodoroSessionResponseDto
    {
        Id = result.Id,
        BreakUsed = result.BreakUsed,
        StartTime = result.StartTime,
        EndTime = result.EndTime,
        DurationMinutes = result.DurationMinutes
    };

    return Ok(responseDto);
}

    [HttpGet("history")]
    public async Task<IActionResult> GetAll()
    {
        var data = await _pomodoroRepository.GetUserSessionsAsync(GetUserId());
        return Ok(data);
    }
}