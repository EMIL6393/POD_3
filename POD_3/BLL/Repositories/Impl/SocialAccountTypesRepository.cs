using Microsoft.EntityFrameworkCore;
using POD_3.BLL.Repositories.Repository;
using POD_3.Context;
using POD_3.DAL.Entity.AccountManagementMod;
using System.Xml.Linq;

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
             var result = await dbContext.SocialAccountTypes.ToListAsync();
            return result;
        }

        public async Task<SocialAccountType> GetByIdAsync(int Id)
        {
            var accountType = await dbContext.SocialAccountTypes.FindAsync(Id);
            return accountType;
        }

        public async Task<int> GetByNameAsync(string accountType)
        {
            var account = await dbContext.SocialAccountTypes.SingleAsync(x => x.AccountType == accountType);
            return account.Id;
        }
    }
}
