using POD_3.BLL.Repositories.Repository;
using POD_3.BLL.Services.Interfaces;
using POD_3.Context;
using POD_3.DAL.Entity.SupportModule;

namespace POD_3.BLL.Services.Implementation
{
    public class CreateSupportTicketService : ICreateSupportTicketService
    {
        private readonly ISupportTicketRepository _repository;
        private readonly DefaultContext _context;
        public CreateSupportTicketService(ISupportTicketRepository repository, DefaultContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<SupportTicket> CreateTicketAsync(SupportTicket ticket)
        {
            await _repository.AddAsync(ticket);
            return ticket;
        }
    }
}
