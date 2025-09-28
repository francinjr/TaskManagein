using Microsoft.EntityFrameworkCore;
using TaskManagein.Data;
using TaskManagein.Models;
using TaskManagein.Repositories.Interfaces;

namespace TaskManagein.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TaskManageinDbContext _dbContext;

        public UserRepository(TaskManageinDbContext taskManageinDbContext)
        {
            _dbContext = taskManageinDbContext;
        }

        public async Task<List<UserModel>> FindAll()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<UserModel> FindById(int id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<UserModel> Add(UserModel user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<UserModel> Update(UserModel user)
        {
            UserModel userById = await FindById(user.Id);

            if(userById == null)
            {
                throw new Exception($"Usuário com Id: {user.Id} não foi encontrado");
            }

            userById.Name = user.Name;
            userById.Email = user.Email;

            _dbContext.Users.Update(userById);
            await _dbContext.SaveChangesAsync();

            return userById;
        }

        public async Task<bool> Delete(int id)
        {
            UserModel userById = await FindById(id);

            if (userById == null)
            {
                throw new Exception($"Usuário com Id: {id} não foi encontrado");
            }

            _dbContext.Users.Remove(userById);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
