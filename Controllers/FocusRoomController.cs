using Focus_App.Models;
using Focus_App.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Focus_App.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class FocusRoomController : ControllerBase
{
private readonly IFocusRoomRepository _focusRoomRepository;

public FocusRoomController(IFocusRoomRepository focusRoomRepository)
{
    _focusRoomRepository = focusRoomRepository;
}

    [HttpGet]
    public async Task<IActionResult> GetAllRooms()
    {
        var rooms = await _focusRoomRepository.GetAllAsync();
        return Ok(rooms);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRoom([FromBody] FocusRoomCreateDto request)
    {
        var room = new FocusRoom
        {
            RoomName = request.RoomName,
            IsPrivate = request.IsPrivate,
            MaxUsers = request.MaxUsers,
            CreatedAt = DateTime.UtcNow
        };

		var result = await _focusRoomRepository.CreateAsync(room);

        return Ok(room);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRoom(int id)
    {
        var success = await _focusRoomRepository.DeleteAsync(id);
        return success ? Ok("Oda silindi") : NotFound();
    }
}