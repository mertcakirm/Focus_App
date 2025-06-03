using Focus_App.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Focus_App.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class InsightController : ControllerBase
{
    private readonly IFocusInsightRepository _insightRepo;
    public InsightController(IFocusInsightRepository insightRepo) => _insightRepo = insightRepo;

    private int GetUserId() => int.Parse(User.FindFirstValue(System.Security.Claims.ClaimTypes.NameIdentifier));

    [HttpGet("weekly")]
    public async Task<IActionResult> GetWeeklyInsight()
    {
        var insight = await _insightRepo.GetOrCreateCurrentWeekAsync(GetUserId());
        return Ok(insight);
    }

    [HttpPost("update")]
    public async Task<IActionResult> UpdateMinutes([FromBody] int minutes)
    {
        var insight = await _insightRepo.UpdateMinutesAsync(GetUserId(), minutes);
        return Ok(insight);
    }
}