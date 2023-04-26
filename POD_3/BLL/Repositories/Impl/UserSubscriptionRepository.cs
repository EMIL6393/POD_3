using Microsoft.EntityFrameworkCore;
using POD_3.BLL.Repositories.Repository;
using POD_3.Context;
using POD_3.DAL.Entity.SubscriptionManagementMod;
using System.Collections.Generic;

namespace POD_3.BLL.Repositories.Impl
{
    public class UserSubscriptionRepository : IUserSubscriptionRepository
    {
        private readonly DefaultContext dbContext;
        public UserSubscriptionRepository(DefaultContext dbContext)
        {
            this.dbContext = dbContext; 
        }
        
        public async Task AddAsync(UserSubscription subscription)
        {
           //var result =
           await dbContext.UserSubscriptions.AddAsync(subscription);
            //return result.Entity;
        }

        public async Task<UserSubscription?> GetByIdAsync(int subscriptionId)
        {
            var subscription = await dbContext.UserSubscriptions.Where(s=>s.SubscriptionId==subscriptionId).SingleOrDefaultAsync(); 
            return subscription;
        }

        public async Task<UserSubscription?> GetByUsernameAsync(string username)
        {
            var subscription = await dbContext.UserSubscriptions.SingleOrDefaultAsync(x => x.UserName == username && x.SubscriptionStatus!="Cancelled");
            return subscription;
        }


        public async Task UpdatePlanAsync(UserSubscription updatedSubscription)
        {
            var dbEntity = await dbContext.UserSubscriptions.SingleAsync(x => x.UserName == updatedSubscription.UserName && x.SubscriptionStatus != "Cancelled");
            if (dbEntity == null)
                throw new Exception($"Active subscription for {updatedSubscription.UserName} Not Found!");
            if (dbEntity.SubscriptionId != updatedSubscription.SubscriptionId) 
            {
                throw new Exception($"Subscription Id missmatch");
            }
            dbContext.Entry(dbEntity).CurrentValues.SetValues(updatedSubscription);
        }
    }
}
