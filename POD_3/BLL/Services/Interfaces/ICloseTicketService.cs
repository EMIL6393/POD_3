using POD_3.DAL.Entity.SupportModule;

namespace POD_3.BLL.Services.Interfaces
{
    public interface ICloseTicketService
    {
        Task<SupportTicket> CloseTicketAsync(int ticketId, string resolvedByUserName, DateTime resolvedOn, string resolutionDetails);
    }
}
