using POD_3.DAL.Entity.SupportModule;

namespace POD_3.BLL.Services.Interfaces
{
    public interface IFetchTicketByIdService
    {
        Task<SupportTicket> GetByIdAsync(int ticketId);
    }
}
