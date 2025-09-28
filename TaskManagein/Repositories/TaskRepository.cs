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

        public async Task<List<TaskModel>> FindAll()
        {
            return await _dbContext.Tasks
                .Include(x => x.User)
                .ToListAsync();
        }

        public async Task<TaskModel> FindById(int id)
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

        public async Task<TaskModel> Update(TaskModel task)
        {
            _dbContext.Tasks.Update(task);
            await _dbContext.SaveChangesAsync();
            return task;
        }

        public async Task<bool> Delete(int id)
        {
            var task = new TaskModel { Id = id };

            _dbContext.Tasks.Attach(task);
            _dbContext.Tasks.Remove(task);

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ExistsById(int id)
        {
            return await _dbContext.Tasks.AnyAsync(task => task.Id == id);
        }

        public async Task<bool> ExistsByName(string name)
        {
            return await _dbContext.Tasks.AnyAsync(task => task.Name == name);
        }
    }
}
