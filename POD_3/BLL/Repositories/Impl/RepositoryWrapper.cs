using POD_3.BLL.Repositories.Repository;
using POD_3.Context;

namespace POD_3.BLL.Repositories.Impl
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly DefaultContext dbContext;
        public ISubscriptionCancellationRepository SubscriptionCancellationRepository { get; set; }

        public ISubscriptionPlanRepository SubscriptionPlanRepository { get; set; }

        public IUserSubscriptionRepository UserSubscriptionRepository { get; set; }

        public IUserRepository UserRepository { get; set; }

        public ILoginRepository LoginRepository { get; set; }

        public ISocialAccountTypesRepository SocialAccountTypesRepository { get; set; }

        public RepositoryWrapper(DefaultContext dbContext)
        {
            this.dbContext = dbContext;
            this.SubscriptionCancellationRepository = new SubscriptionCancellationRepository(dbContext);
            this.SubscriptionPlanRepository = new SubscriptionPlanRepository(dbContext);
            this.UserSubscriptionRepository = new UserSubscriptionRepository(dbContext);
            this.UserRepository = new UserRepository(dbContext);
            this.LoginRepository = new LoginRepository(dbContext);
            this.SocialAccountTypesRepository = new SocialAccountTypesRepository(dbContext);
        }



        public async Task<int> SaveAsync()
        {
            return await this.dbContext.SaveChangesAsync();
        }
    }
}
