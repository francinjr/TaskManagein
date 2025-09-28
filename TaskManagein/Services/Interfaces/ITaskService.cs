using TaskManagein.Exceptions.Handler;
using TaskManagein.Models;

namespace TaskManagein.Services.Interfaces
{
    public interface ITaskService
    {
        Task<List<TaskModel>> FindAll();
        Task<TaskModel> FindById(int id);
        Task<TaskModel> Create(TaskModel task);
        Task<TaskModel> Update(int id, TaskModel task);
        Task<bool> Delete(int id);
    }
}
