using Microsoft.EntityFrameworkCore;
using TaskManagein.Data;
using TaskManagein.Models;
using TaskManagein.Repositories.Interfaces;

namespace TaskManagein.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskManageinDbContext _dbContext;

        public TaskRepository(TaskManageinDbContext taskManageinDbContext)
        {
            _dbContext = taskManageinDbContext;
        }

        public async Task<List<TaskModel>> findAll()
        {
            return await _dbContext.Tasks
                .Include(x => x.User)
                .ToListAsync();
        }

        public async Task<TaskModel> findById(int id)
        {
            return await _dbContext.Tasks
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<TaskModel> Add(TaskModel task)
        {
            await _dbContext.Tasks.AddAsync(task);
            await _dbContext.SaveChangesAsync();

            return task;
        }

        public async Task<TaskModel> Update(TaskModel task, int id)
        {
            TaskModel taskById = await findById(id);

            if(taskById == null)
            {
                throw new Exception($"Tarefa com Id: {id} não foi encontrado");
            }

            taskById.Name = task.Name;
            taskById.Description = task.Description;
            taskById.Status = task.Status;
            taskById.UserId = task.UserId;

            _dbContext.Tasks.Update(taskById);
            await _dbContext.SaveChangesAsync();

            return taskById;
        }

        public async Task<bool> Delete(int id)
        {
            TaskModel taskById = await findById(id);

            if (taskById == null)
            {
                throw new Exception($"Tarefa com Id: {id} não foi encontrado");
            }

            _dbContext.Tasks.Remove(taskById);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
