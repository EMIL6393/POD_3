using Microsoft.EntityFrameworkCore;
using POD_3.BLL.Repositories.Repository;
using POD_3.Context;
using POD_3.DAL.Entity.AccountManagementMod;

namespace POD_3.BLL.Repositories.Impl
{
    public class SocialAccountTrackerRepository : ISocialAccountTrackerRepository
    {
        private readonly DefaultContext dbContext;

        public SocialAccountTrackerRepository(DefaultContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddAsync(SocialAccountTracker accountTracker)
        {
            await dbContext.SocialAccountTrackers.AddAsync(accountTracker);
        }

        public async Task<SocialAccountTracker> GetAsync(string username)
        {
            var dbEntity = await dbContext.UserSocialAccounts.SingleAsync(x => x.UserName == username);
            if (dbEntity == null)
            {
                throw new Exception($"username {username} not found");
            }
            var track = await dbContext.SocialAccountTrackers.SingleAsync(x => x.AccountId == dbEntity.Id);

            return track;
        }
    }
}
