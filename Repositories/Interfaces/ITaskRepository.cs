using Focus_App.Models;

namespace Focus_App.Repositories.Interfaces;

public interface ITaskRepository
{
    Task<List<TaskItem>> GetUserTasksAsync(int userId);
    Task<TaskItem?> GetByIdAsync(int id, int userId);
    Task<TaskItem> AddTaskAsync(TaskItem task);
    Task<TaskItem?> UpdateTaskAsync(TaskItem updatedTask);
    Task<bool> DeleteTaskAsync(int id, int userId);
}