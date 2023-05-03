using POD_3.BLL.Repositories.Repository;
using POD_3.Context;
using POD_3.DAL.Entity.SupportModule;

namespace POD_3.BLL.Repositories.Impl
{
    public class TicketSolutionRepository : ITicketSolutionRepository
    {

        private readonly DefaultContext _dbContext;

        public TicketSolutionRepository(DefaultContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TicketSolution> GetByIdAsync(int id)
        {
            return await _dbContext.Set<TicketSolution>().FindAsync(id);
        }

  

        public async Task<int> AddAsync(TicketSolution solution)
        {
            await _dbContext.Set<TicketSolution>().AddAsync(solution);
            await _dbContext.SaveChangesAsync();

            return solution.Id;
        }

        public async Task UpdateAsync(TicketSolution solution)
        {
            _dbContext.Set<TicketSolution>().Update(solution);
            await _dbContext.SaveChangesAsync();
        }

      
        public async Task UpdateTicketWithResolutionAsync(int ticketId, string resolvedByUserName, DateTime resolvedOn, string resolutionDetails)
        {
           
            var ticketSolution = new TicketSolution
            {
                SupportTicketId = ticketId,
                ResolvedByUserName = resolvedByUserName,
                ResolvedOn = resolvedOn,
                ResolutionDetails = resolutionDetails
            };
            await AddAsync(ticketSolution);
           
        }
    }
}
