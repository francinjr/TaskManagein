using TaskManagein.Models;

namespace TaskManagein.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<UserModel>> FindAll();

        Task<UserModel> FindById(int id);

        Task<UserModel> Add(UserModel user);

        Task<UserModel> Update(UserModel user);

        Task<bool> Delete(int id);

    }
}
