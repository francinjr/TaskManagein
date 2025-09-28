using TaskManagein.Models;

namespace TaskManagein.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        Task<List<TaskModel>> FindAll();

        Task<TaskModel> FindById(int id);

        Task<TaskModel> Add(TaskModel task);

        Task<TaskModel> Update(TaskModel task);

        Task<bool> Delete(int id);

        Task<bool> ExistsById(int id);

        Task<bool> ExistsByName(string name);
    }
}
