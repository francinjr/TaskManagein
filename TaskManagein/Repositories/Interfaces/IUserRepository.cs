using TaskManagein.Models;

namespace TaskManagein.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<UserModel>> findAll();

        Task<UserModel> findById(int id);

        Task<UserModel> Add(UserModel user);

        Task<UserModel> Update(UserModel user, int id);

        Task<bool> Delete(int id);

    }
}
