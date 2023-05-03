using POD_3.DAL.Entity.SupportModule;

namespace POD_3.BLL.Services.Interfaces
{
    public interface ICreateSupportTicketService
    {
        Task<SupportTicket> CreateTicketAsync(SupportTicket ticket);
    }
}
