using POD_3.DAL.Entity.SubscriptionManagementMod;

namespace POD_3.BLL.Repositories.Repository
{
    public interface ISubscriptionPlanRepository
    {
        Task<List<SubscriptionPlan>> GetAllAsync();
        Task<SubscriptionPlan> GetByIdAsync(int planId);

        Task<int> GetByNameAsync(string name);
       
    }
}
