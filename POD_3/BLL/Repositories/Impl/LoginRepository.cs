using Microsoft.EntityFrameworkCore;
using POD_3.BLL.Repositories.Repository;
using POD_3.Context;
using POD_3.DAL.Entity;

namespace POD_3.BLL.Repositories.Impl
{
    public class LoginRepository : ILoginRepository

    {
       private readonly DefaultContext dbContext;

        public LoginRepository(DefaultContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<User> UserLogin(string email, string password)
        {
            var custLogin = await dbContext.Users.Where(x => x.Email == email && x.Password == password).SingleOrDefaultAsync();
            return custLogin;
        }
    }
    
}
