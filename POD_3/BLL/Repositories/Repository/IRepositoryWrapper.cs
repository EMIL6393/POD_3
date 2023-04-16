namespace POD_3.BLL.Repositories.Repository
{
    public interface IRepositoryWrapper
    {
        public ISubscriptionCancellationRepository SubscriptionCancellationRepository { get; }
        public ISubscriptionPlanRepository SubscriptionPlanRepository { get; }
        public IUserSubscriptionRepository UserSubscriptionRepository { get; }
        Task<int> SaveAsync();

    }
}
