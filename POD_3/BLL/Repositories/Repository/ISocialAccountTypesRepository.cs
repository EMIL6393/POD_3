using POD_3.DAL.Entity.AccountManagementMod;
using POD_3.DAL.Entity.SubscriptionManagementMod;

namespace POD_3.BLL.Repositories.Repository
{
    public interface ISocialAccountTypesRepository
    {
        Task<List<SocialAccountType>> GetAllAsync();
        Task<SocialAccountType> GetByIdAsync(int planId);

    }
}
