using POD_3.DAL.Entity.SubscriptionManagementMod;

namespace POD_3.BLL.Repositories.Repository
{
    public interface ISubscriptionCancellationRepository
    {

        Task AddAsync (SubscriptionCancellation cancellation);
        Task<SubscriptionCancellation> GetAsync (string username);
    }
}
