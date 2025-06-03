using Focus_App.Models;
using Focus_App.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Focus_App.DTOs; 

namespace Focus_App.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TaskController : ControllerBase
{
    private readonly ITaskItemRepository _taskRepository;

    public TaskController(ITaskItemRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    private int GetUserId() => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

    [HttpGet]
    public async Task<IActionResult> GetTasks()
    {
        var tasks = await _taskRepository.GetTasksByUserIdAsync(GetUserId());
        return Ok(tasks);
    }

    [HttpPost]
    public async Task<IActionResult> AddTask([FromBody] TaskItemDto dto)
    {
        var task = new TaskItem
        {
            Title = dto.Title,
            IsCompleted = dto.IsCompleted,
            DueDate = dto.DueDate,
            CreatedAt = DateTime.UtcNow,
            UserId = GetUserId()
        };

        await _taskRepository.AddTaskAsync(task);
        return Ok("Eklendi");
    }

	[HttpPut("{id}")]
	public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskItemDto dto)
	{
    	var update = new TaskItem
    	{
        	Id = id,
        	UserId = GetUserId(),
        	Title = dto.Title,
        	DueDate = dto.DueDate
    	};

    	var existingTasks = await _taskRepository.GetTasksByUserIdAsync(GetUserId());
    	var original = existingTasks.FirstOrDefault(t => t.Id == id);
    	if (original == null) return NotFound();

    	update.IsCompleted = original.IsCompleted; // mevcut durum korunur
    	var updated = await _taskRepository.UpdateTaskAsync(update);
    	return Ok(updated);
	}

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var success = await _taskRepository.DeleteTaskAsync(id, GetUserId());
        if (!success) return NotFound();
        return Ok("Silindi");
    }

	[HttpPatch("{id}/toggle-completion")]
	public async Task<IActionResult> UpdateCompletion(int id, [FromBody] bool isCompleted)
	{
    var existingTasks = await _taskRepository.GetTasksByUserIdAsync(GetUserId());
    var task = existingTasks.FirstOrDefault(t => t.Id == id);
    if (task == null) return NotFound();

    task.IsCompleted = isCompleted;
    await _taskRepository.UpdateTaskAsync(task);
    return Ok("Tamamlanma durumu güncellendi.");
	}
}