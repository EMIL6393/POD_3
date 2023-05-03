﻿using System.ComponentModel.DataAnnotations;

namespace POD_3.DAL.Entity.SupportModule
{
    public class SupportTicket
    {
        [Key]
        [Range(1, 10)]
        public int TicketId { get; set; }

        [StringLength(10)]
        public string RaisedByUserName { get; set; } = null!;

        [DataType(DataType.DateTime)]
        public DateTime CreatedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime ExpectedResolutionOn { get; set; }

        [StringLength(100)]
        public string? TicketSummary { get; set; }

        [StringLength(1000)]
        public string TicketDetails { get; set; }= null!;

        [StringLength(10)]
        public string TicketStatus { get; set; } = null!;

        [StringLength(20)]
        public string TicketType { get; set; } = null!;

        public virtual TicketSolution? TicketSolution { get; set; }
    }
}
