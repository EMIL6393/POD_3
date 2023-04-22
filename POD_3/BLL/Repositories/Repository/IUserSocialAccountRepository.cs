using POD_3.DAL.Entity.AccountManagementMod;
using POD_3.DAL.Entity.SubscriptionManagementMod;

namespace POD_3.BLL.Repositories.Repository
{
    public interface IUserSocialAccountRepository
    {
        Task<UserSocialAccount?> GetByUsernameAsync(string username);
        Task<UserSocialAccount?> GetByIdAsync(int id);
        Task AddAsync(UserSocialAccount socialAccount);
        Task<bool> Delete(int id);
        Task UpdatePlanAsync (UserSocialAccount updatedSocialAccount);

    }
}
