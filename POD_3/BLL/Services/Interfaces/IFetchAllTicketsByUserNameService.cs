using POD_3.DAL.Entity.SupportModule;

namespace POD_3.BLL.Services.Interfaces
{
    public interface IFetchAllTicketsByUserNameService
    {
        Task<List<SupportTicket>> GetTicketsByUsernameAsync(string userName);

        Task<int> GetTicketCountForUserAsync(string userName, DateTime startDate, DateTime endDate);
    }
}
