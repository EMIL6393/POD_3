using System.ComponentModel.DataAnnotations;

namespace POD_3.DAL.Models
{
    public class CreateTicketModel
    {
        [StringLength(10)]
        public string RaisedByUserName { get; set; } = null!;

        [StringLength(1000)]
        public string TicketDetails { get; set; } = null!;

        [StringLength(20)]
        public string TicketType { get; set; } = null!;

        [StringLength(100)]
        public string? TicketSummary { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime ExpectedResolutionOn { get; set; }

    }
}
