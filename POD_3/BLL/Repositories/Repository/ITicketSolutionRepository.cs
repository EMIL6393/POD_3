using POD_3.DAL.Entity.SupportModule;

namespace POD_3.BLL.Repositories.Repository
{
    public interface ITicketSolutionRepository
    {

        Task<TicketSolution> GetByIdAsync(int id);
        
        Task<int> AddAsync(TicketSolution solution);
        Task UpdateAsync(TicketSolution solution);
        
        Task UpdateTicketWithResolutionAsync(int ticketId, string resolvedByUserName, DateTime resolvedOn, string resolutionDetails);
    }
}
