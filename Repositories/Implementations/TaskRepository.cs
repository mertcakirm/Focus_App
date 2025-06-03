using Focus_App.Models;
using Focus_App.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Focus_App.Repositories.Implementations;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;
    public TaskRepository(AppDbContext context) => _context = context;

    public async Task<List<TaskItem>> GetUserTasksAsync(int userId)
    {
        return await _context.Tasks.Where(t => t.UserId == userId).ToListAsync();
    }

    public async Task<TaskItem?> GetByIdAsync(int id, int userId)
    {
        return await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
    }

    public async Task<TaskItem> AddTaskAsync(TaskItem task)
    {
        task.CreatedAt = DateTime.UtcNow;
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
        return task;
    }

    public async Task<TaskItem?> UpdateTaskAsync(TaskItem updatedTask)
    {
        var task = await GetByIdAsync(updatedTask.Id, updatedTask.UserId);
        if (task == null) return null;

        task.Title = updatedTask.Title;
        task.IsCompleted = updatedTask.IsCompleted;
        task.DueDate = updatedTask.DueDate;
        await _context.SaveChangesAsync();

        return task;
    }

    public async Task<bool> DeleteTaskAsync(int id, int userId)
    {
        var task = await GetByIdAsync(id, userId);
        if (task == null) return false;

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
        return true;
    }
}