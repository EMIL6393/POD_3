using Microsoft.EntityFrameworkCore;
using POD_3.BLL.Repositories.Repository;
using POD_3.Context;
using POD_3.DAL.Entity.SupportModule;

namespace POD_3.BLL.Repositories.Impl
{
    public class SupportTicketRepository: ISupportTicketRepository
    {
        private readonly DefaultContext _dbContext;

        public SupportTicketRepository(DefaultContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SupportTicket> GetByIdAsync(int ticketId)
        {
            return await _dbContext.Set<SupportTicket>().FindAsync(ticketId);
        }

        public async Task<List<SupportTicket>> GetAllAsync()
        {
            return await _dbContext.Set<SupportTicket>().ToListAsync();
        }

        public async Task<int> AddAsync(SupportTicket ticket)
        {
            var newTicket = new SupportTicket
            {
                TicketId = ticket.TicketId,
                RaisedByUserName = ticket.RaisedByUserName,
                CreatedOn = DateTime.UtcNow,
                ExpectedResolutionOn = ticket.ExpectedResolutionOn,
                TicketSummary = ticket.TicketSummary,
                TicketDetails = ticket.TicketDetails,
                TicketStatus = "Open",
                TicketType = ticket.TicketType,
            };

            await _dbContext.Set<SupportTicket>().AddAsync(newTicket);
            await _dbContext.SaveChangesAsync();
            return newTicket.TicketId; ;
        }


        public async Task UpdateAsync(SupportTicket ticket)
        {
            _dbContext.Entry(ticket).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }


        public async Task<List<SupportTicket>> GetByUserAsync(string userName)
        {
            return await _dbContext.Set<SupportTicket>()
                .Where(t => t.RaisedByUserName == userName)
                .ToListAsync();
        }

        public async Task<List<SupportTicket>> GetByUserAsync(string userName, DateTime startDate, DateTime endDate)
        {
            return await _dbContext.Set<SupportTicket>()
                .Where(t => t.RaisedByUserName == userName && t.CreatedOn >= startDate && t.CreatedOn <= endDate)
                .ToListAsync();
        }

        public async Task<List<SupportTicket>> GetOpenTicketsAsync()
        {
            return await _dbContext.Set<SupportTicket>()
                .Where(t => t.TicketStatus == "Open")
                .ToListAsync();
        }
    }
}
