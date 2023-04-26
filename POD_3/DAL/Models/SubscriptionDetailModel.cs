using System.ComponentModel.DataAnnotations;

namespace POD_3.DAL.Models
{
    public class SubscriptionDetailModel
    {
        [Key]
        [MaxLength(10)]
        public int SubscriptionId { get; set; }

        [StringLength(10)]
        public string UserName { get; set; } = null!;

        public SubscriptionPlanModel Plan { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime SubscriptionStartDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime SubscriptionEndDate { get; set; }

        [Range(1, 10)]
        public double AmountPaid { get; set; }

        [StringLength(50)]
        public string PaymentMode { get; set; } = null!;

        [StringLength(12)]
        public string SubscriptionStatus { get; set; } = null!;
    }
}
