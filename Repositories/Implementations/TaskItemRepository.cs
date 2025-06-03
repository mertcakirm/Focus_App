using Focus_App.Models;
using Focus_App.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Focus_App.Repositories.Implementations;

public class TaskItemRepository : ITaskItemRepository
{
    private readonly AppDbContext _context;

    public TaskItemRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<TaskItem>> GetTasksByUserIdAsync(int userId)
    {
        return await _context.Tasks
            .Where(t => t.UserId == userId)
            .ToListAsync();
    }

    public async Task AddTaskAsync(TaskItem task)
    {
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
    }

    public async Task<TaskItem?> UpdateTaskAsync(TaskItem updatedTask)
    {
        var task = await _context.Tasks
            .FirstOrDefaultAsync(t => t.Id == updatedTask.Id && t.UserId == updatedTask.UserId);

        if (task == null) return null;

        task.Title = updatedTask.Title;
        task.IsCompleted = updatedTask.IsCompleted;
        task.DueDate = updatedTask.DueDate;

        await _context.SaveChangesAsync();
        return task;
    }

public async Task<bool> DeleteTaskAsync(int taskId, int userId)
{
    var task = await _context.Tasks
        .FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId);

    if (task == null) return false;

    task.IsDeleted = true;
    await _context.SaveChangesAsync();
    return true;
}
}