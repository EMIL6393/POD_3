using POD_3.BLL.Repositories.Repository;
using POD_3.BLL.Services.Interfaces;
using POD_3.Context;
using POD_3.DAL.Entity.SupportModule;

namespace POD_3.BLL.Services.Implementation
{
    public class FetchOpenTicketsService : IFetchOpenTicketsService
    {
        private readonly ISupportTicketRepository _ticketRepository;
        private readonly DefaultContext _context;

        public FetchOpenTicketsService(ISupportTicketRepository repository, DefaultContext context)
        {
            _ticketRepository = repository;
            _context = context;
        }

        public async Task<List<SupportTicket>> GetOpenTicketsAsync()
        {
            var tickets = await _ticketRepository.GetOpenTicketsAsync();
            return tickets;
        }
    }
}
