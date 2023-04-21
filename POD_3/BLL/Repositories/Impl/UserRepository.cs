using Microsoft.EntityFrameworkCore;
using POD_3.BLL.Repositories.Repository;
using POD_3.Context;
using POD_3.DAL.Entity;

namespace POD_3.BLL.Repositories.Impl
{
    public class UserRepository : IUserRepository
    {

        private readonly DefaultContext dbContext;
        public UserRepository(DefaultContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddAsync(User user)
        {
            await dbContext.Users.AddAsync(user);
        }

        public async Task<int> GetByNameAsync(string email)
        {
            var user = await dbContext.Users.SingleOrDefaultAsync(x => x.Email == email);
            return user.Id;
        }
    }
}
