using Focus_App.Models;
using Focus_App.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Focus_App.Repositories.Interfaces;

namespace Focus_App.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class FocusRoomController : ControllerBase
{
    private readonly IFocusRoomRepository _roomRepository;
    public FocusRoomController(IFocusRoomRepository roomRepository) => _roomRepository = roomRepository;

    [HttpGet]
    public async Task<IActionResult> GetAllRooms()
    {
        var rooms = await _roomRepository.GetAllAsync();
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

        await _focusRoomRepository.CreateRoomAsync(room);

        return Ok(room);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRoom(int id)
    {
        var success = await _roomRepository.DeleteAsync(id);
        return success ? Ok("Oda silindi") : NotFound();
    }
}