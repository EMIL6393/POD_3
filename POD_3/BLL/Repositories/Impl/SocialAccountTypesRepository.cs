using Microsoft.EntityFrameworkCore;
using POD_3.BLL.Repositories.Repository;
using POD_3.Context;
using POD_3.DAL.Entity.AccountManagementMod;

namespace POD_3.BLL.Repositories.Impl
{
    public class SocialAccountTypesRepository : ISocialAccountTypesRepository
    {
        private readonly DefaultContext dbContext;

        public SocialAccountTypesRepository(DefaultContext dbContext) 
        {
            this.dbContext = dbContext;
        }

        public async Task<List<SocialAccountType>> GetAllAsync()
        {
            return await dbContext.SocialAccountTypes.ToListAsync();
        }

        public async Task<SocialAccountType> GetByIdAsync(int planId)
        {
            var accountType = await dbContext.SocialAccountTypes.FindAsync(planId);
            return accountType;
        }
    }
}
