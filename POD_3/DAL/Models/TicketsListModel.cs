using System.ComponentModel.DataAnnotations;

namespace POD_3.DAL.Models
{
    public class TicketsListModel
    {
        [Key]
        [Range(1, 10)]
        public int TicketId { get; set; }
    }
}
