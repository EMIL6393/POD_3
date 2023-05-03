using POD_3.DAL.Entity.SupportModule;

namespace POD_3.BLL.Repositories.Repository
{
    public interface ISupportTicketRepository
    {
        Task<SupportTicket> GetByIdAsync(int ticketId);
        Task<List<SupportTicket>> GetAllAsync();

        Task<List<SupportTicket>> GetByUserAsync(string userName);

        Task<List<SupportTicket>> GetByUserAsync(string userName, DateTime startDate, DateTime endDate);
        Task<int> AddAsync(SupportTicket ticket);
        Task UpdateAsync(SupportTicket ticket);


     
        Task<List<SupportTicket>> GetOpenTicketsAsync();
    }
}
