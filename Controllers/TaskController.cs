using Focus_App.Models;
using Focus_App.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Focus_App.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TaskController : ControllerBase
{
    private readonly ITaskRepository _taskRepository;
    public TaskController(ITaskRepository taskRepository) => _taskRepository = taskRepository;

    private int GetUserId() => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

    [HttpGet]
    public async Task<IActionResult> GetTasks()
    {
        var tasks = await _taskRepository.GetUserTasksAsync(GetUserId());
        return Ok(tasks);
    }

    [HttpPost]
    public async Task<IActionResult> AddTask([FromBody] TaskItem task)
    {
        task.UserId = GetUserId();
        var result = await _taskRepository.AddTaskAsync(task);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskItem update)
    {
        update.Id = id;
        update.UserId = GetUserId();
        var updated = await _taskRepository.UpdateTaskAsync(update);
        if (updated == null) return NotFound();
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var success = await _taskRepository.DeleteTaskAsync(id, GetUserId());
        if (!success) return NotFound();
        return Ok("Silindi");
    }
}