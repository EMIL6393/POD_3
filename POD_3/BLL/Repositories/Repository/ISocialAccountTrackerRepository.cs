using POD_3.DAL.Entity.AccountManagementMod;
using POD_3.DAL.Entity.SubscriptionManagementMod;

namespace POD_3.BLL.Repositories.Repository
{
    public interface ISocialAccountTrackerRepository
    {
        Task AddAsync(SocialAccountTracker accountTracker);
        Task<SocialAccountTracker> GetAsync(string username);
    }
}
