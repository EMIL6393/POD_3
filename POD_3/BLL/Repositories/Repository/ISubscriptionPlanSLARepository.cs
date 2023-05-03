using POD_3.DAL.Entity.SupportModule;

namespace POD_3.BLL.Repositories.Repository
{
    public interface ISubscriptionPlanSLARepository
    {
        Task<SubscriptionPlanSLA> GetByIdAsync(int id);
        Task<List<SubscriptionPlanSLA>> GetAllAsync();
        int GetSLADays(string planName);
    }
}
