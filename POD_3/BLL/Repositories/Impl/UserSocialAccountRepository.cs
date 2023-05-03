using Microsoft.EntityFrameworkCore;
using POD_3.BLL.Repositories.Repository;
using POD_3.Context;
using POD_3.DAL.Entity;
using POD_3.DAL.Entity.AccountManagementMod;

namespace POD_3.BLL.Repositories.Impl
{
    public class UserSocialAccountRepository : IUserSocialAccountRepository
    {
        private readonly DefaultContext dbContext;
        public UserSocialAccountRepository(DefaultContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddAsync(UserSocialAccount account)
        {
            await dbContext.UserSocialAccounts.AddAsync(account);

        }

        public async Task<bool> Delete(int id)
        {
            var result = await dbContext.UserSocialAccounts.FindAsync(id);
            if (result != null)
            {
                dbContext.UserSocialAccounts.Remove(result);
                await dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<UserSocialAccount?> GetByIdAsync(int id)
        {
            var account = await dbContext.UserSocialAccounts.FindAsync(id);
            return account;
        }

        public async Task<UserSocialAccount?> GetByUsernameAsync(string username)
        {
            var account = await dbContext.UserSocialAccounts.SingleOrDefaultAsync(x => x.UserName == username);
            return account;
        }

        public async Task<int> GetByUsernameAsyncCount(string username)
        {
            var account = await dbContext.UserSocialAccounts.Where(a=>a.UserName == username).ToListAsync();
            return account.Count;

        }

        public async Task UpdatePlanAsync(UserSocialAccount updatedSocialAccount)
        {
            var dbEntity = await dbContext.UserSocialAccounts.SingleAsync(x => x.UserName==updatedSocialAccount.UserName);
            if (dbEntity == null)
                throw new Exception($"Active subscription for {updatedSocialAccount.UserName} Not Found!");
            if (dbEntity.Id != updatedSocialAccount.Id)
            {
                throw new Exception($"Subscription Id missmatch");
            }
            dbContext.Entry(dbEntity).CurrentValues.SetValues(updatedSocialAccount);
        }
    }
}
