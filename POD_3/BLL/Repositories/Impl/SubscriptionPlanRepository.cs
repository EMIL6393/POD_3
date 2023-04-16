using Microsoft.EntityFrameworkCore;
using POD_3.BLL.Repositories.Repository;
using POD_3.Context;
using POD_3.DAL.Entity.SubscriptionManagementMod;

namespace POD_3.BLL.Repositories.Impl
{
    public class SubscriptionPlanRepository : ISubscriptionPlanRepository
    {

        private readonly DefaultContext dbContext;

        public SubscriptionPlanRepository(DefaultContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<SubscriptionPlan>> GetAllAsync()
        {
            var result = await dbContext.SubscriptionPlans.ToListAsync();
            return result;
        }

        public async Task<SubscriptionPlan> GetByIdAsync(int planId)
        {
            var subscriptionPlan  = await dbContext.SubscriptionPlans.FindAsync(planId);
            return subscriptionPlan;
        }

        public async Task<int> GetByNameAsync(string name)
        {
            var subscriptionPlan = await dbContext.SubscriptionPlans.SingleAsync(x => x.Name == name);
            return subscriptionPlan.PlanId;
        }
    }
}
