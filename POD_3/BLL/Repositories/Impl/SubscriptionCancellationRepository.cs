using Microsoft.EntityFrameworkCore;
using POD_3.BLL.Repositories.Repository;
using POD_3.Context;
using POD_3.DAL.Entity.SubscriptionManagementMod;

namespace POD_3.BLL.Repositories.Impl
{
    public class SubscriptionCancellationRepository : ISubscriptionCancellationRepository
    {

        private readonly DefaultContext dbContext;

        public SubscriptionCancellationRepository(DefaultContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task AddAsync(SubscriptionCancellation cancellation)
        {
           await dbContext.SubscriptionCancellations.AddAsync(cancellation);
        }

        public async Task<SubscriptionCancellation> GetAsync(string username)
        {
            var dbEntity = await dbContext.UserSubscriptions.SingleAsync(x => x.UserName == username);
            if (dbEntity == null)
            {
                throw new Exception($"username {username} not found");
            }
            var cancelation = await dbContext.SubscriptionCancellations.SingleAsync(x => x.SubscriptionId == dbEntity.SubscriptionId);

            return cancelation;
        }
    }
}
