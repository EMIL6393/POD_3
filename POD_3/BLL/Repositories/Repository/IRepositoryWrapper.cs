namespace POD_3.BLL.Repositories.Repository
{
    public interface IRepositoryWrapper
    {
        public ISubscriptionCancellationRepository SubscriptionCancellationRepository { get; }
        public ISubscriptionPlanRepository SubscriptionPlanRepository { get; }
        public IUserSubscriptionRepository UserSubscriptionRepository { get; }

        public IUserRepository UserRepository { get; }

        public ILoginRepository LoginRepository { get; }

        public ISocialAccountTypesRepository SocialAccountTypesRepository { get; }

        public IUserSocialAccountRepository UserSocialAccountRepository { get; }

        public ISocialAccountTrackerRepository SocialAccountTrackerRepository { get; }
        Task<int> SaveAsync();

    }
}
