using POD_3.DAL.Entity.SubscriptionManagementMod;

namespace POD_3.BLL.Repositories.Repository
{
    public interface IUserSubscriptionRepository
    {
        Task<UserSubscription?> GetByUsernameAsync(string username);
        Task<UserSubscription?> GetByIdAsync(int subscriptionId);
        Task AddAsync(UserSubscription subscription);
        Task UpdatePlanAsync(UserSubscription updatedSubscription);
        

    }
}
