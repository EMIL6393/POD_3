using POD_3.DAL.Entity;

namespace POD_3.BLL.Repositories.Repository
{
    public interface IUserRepository
    {

        Task AddAsync(User user);

        Task<int> GetByNameAsync(string email);
    }
}
