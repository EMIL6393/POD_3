using POD_3.BLL.Repositories.Repository;
using POD_3.BLL.Services.Interfaces;
using POD_3.Context;
using POD_3.DAL.Entity.SupportModule;

namespace POD_3.BLL.Services.Implementation
{
    public class CloseTicketService : ICloseTicketService
    {
        private readonly ISupportTicketRepository _ticketRepository;
        private readonly ITicketSolutionRepository _solutionRepository;
        private readonly DefaultContext _context;

        public CloseTicketService(ISupportTicketRepository ticketRepository, ITicketSolutionRepository solutionRepository, DefaultContext context)
        {
            _ticketRepository = ticketRepository;
            _solutionRepository = solutionRepository;
            _context = context;
        }

        public async Task<SupportTicket> CloseTicketAsync(int ticketId, string resolvedByUserName, DateTime resolvedOn, string resolutionDetails)
        {
            var ticket = await _ticketRepository.GetByIdAsync(ticketId);

            if (ticket == null)
            {
                throw new ArgumentException($"Ticket with ID {ticketId} not found.");
            }

            ticket.TicketStatus = "Closed";
            ticket.ExpectedResolutionOn = DateTime.UtcNow;

            await _ticketRepository.UpdateAsync(ticket);
            await _solutionRepository.UpdateTicketWithResolutionAsync(ticketId, resolvedByUserName, resolvedOn, resolutionDetails);

            return ticket;
        }

    }
}
