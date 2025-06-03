using Focus_App.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Focus_App.Repositories.Interfaces
{
    public interface ITaskItemRepository
    {
        Task<List<TaskItem>> GetTasksByUserIdAsync(int userId);
        Task AddTaskAsync(TaskItem task);
        Task<TaskItem?> UpdateTaskAsync(TaskItem updatedTask);
        Task<bool> DeleteTaskAsync(int taskId, int userId);
    }
}