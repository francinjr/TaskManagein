using TaskManagein.Models;

namespace TaskManagein.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        Task<List<TaskModel>> findAll();

        Task<TaskModel> findById(int id);

        Task<TaskModel> Add(TaskModel task);

        Task<TaskModel> Update(TaskModel task, int id);

        Task<bool> Delete(int id);

    }
}
