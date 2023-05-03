using System.ComponentModel.DataAnnotations;

namespace POD_3.DAL.Models
{
    public class CloseDetailsModel
    {
        [StringLength(10)]
        public string ResolvedByUserName { get; set; } = null!;

        [DataType(DataType.DateTime)]
        public DateTime ResolvedOn { get; set; }

        [StringLength(1000)]
        public string ResolutionDetails { get; set; } = null!;

        [Range(1, 10)]
        public int SupportTicketId { get; set; }
    }
}
