using POD_3.DAL.Entity;

namespace POD_3.BLL.Repositories.Repository
{
    public interface ILoginRepository
    {
        Task<User> UserLogin(string email, string password);

    }
}
