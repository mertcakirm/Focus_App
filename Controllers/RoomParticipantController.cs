using Focus_App.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Focus_App.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RoomParticipantController : ControllerBase
{
    private readonly IRoomParticipantRepository _participantRepo;
    public RoomParticipantController(IRoomParticipantRepository participantRepo) => _participantRepo = participantRepo;

    private int GetUserId() => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

    [HttpPost("join/{roomId}")]
    public async Task<IActionResult> Join(int roomId)
    {
        var result = await _participantRepo.JoinRoomAsync(roomId, GetUserId());
        return Ok(result);
    }

    [HttpPost("leave/{roomId}")]
    public async Task<IActionResult> Leave(int roomId)
    {
        var success = await _participantRepo.LeaveRoomAsync(roomId, GetUserId());
        return success ? Ok("Odadan çıkıldı") : NotFound();
    }

    [HttpGet("room/{roomId}")]
    public async Task<IActionResult> ListParticipants(int roomId)
    {
        var list = await _participantRepo.GetActiveParticipantsAsync(roomId);
        return Ok(list);
    }
}