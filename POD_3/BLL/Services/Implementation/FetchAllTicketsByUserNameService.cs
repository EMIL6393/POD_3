using POD_3.BLL.Repositories.Repository;
using POD_3.BLL.Services.Interfaces;
using POD_3.Context;
using POD_3.DAL.Entity.SupportModule;

namespace POD_3.BLL.Services.Implementation
{
    public class FetchAllTicketsByUserNameService : IFetchAllTicketsByUserNameService
    {
        private readonly ISupportTicketRepository _ticketRepository;
        private readonly DefaultContext _context;
        public FetchAllTicketsByUserNameService(ISupportTicketRepository repository, DefaultContext context)
        {
            _ticketRepository = repository;
            _context = context;
        }

        public async Task<List<SupportTicket>> GetTicketsByUsernameAsync(string userName)
        {
            return await _ticketRepository.GetByUserAsync(userName);
        }

        public async Task<int> GetTicketCountForUserAsync(string userName, DateTime startDate, DateTime endDate)
        {
            var tickets = await _ticketRepository.GetByUserAsync(userName, startDate, endDate);
            return tickets.Count();
        }
    }
}
