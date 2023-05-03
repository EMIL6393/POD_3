using Microsoft.EntityFrameworkCore;
using POD_3.BLL.Repositories.Repository;
using POD_3.Context;
using POD_3.DAL.Entity.SupportModule;

namespace POD_3.BLL.Repositories.Impl
{
    public class SubscriptionPlanSLARepository: ISubscriptionPlanSLARepository
    {
        private readonly DefaultContext _dbContext;

        public SubscriptionPlanSLARepository(DefaultContext dbContext)
         {
            _dbContext = dbContext;
        }

         public async Task<SubscriptionPlanSLA> GetByIdAsync(int id)
         {
            return await _dbContext.Set<SubscriptionPlanSLA>().FindAsync(id);
         }

        public async Task<List<SubscriptionPlanSLA>> GetAllAsync()
        {
            return await _dbContext.Set<SubscriptionPlanSLA>().ToListAsync();
        }


        public int GetSLADays(string planName)
        {
        
            var subscriptionPlanSLA = _dbContext.Set<SubscriptionPlanSLA>().SingleOrDefault(s => s.PlanName == planName);
       
            return subscriptionPlanSLA.ExpectedSLAsInDays;
        }
    

    }
}
