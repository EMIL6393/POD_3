using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POD_3.DAL.Entity.SubscriptionManagementMod
{
    public class UserSubscription
    {
       

        [Key]
        [Range(1, 10)]
        public int SubscriptionId { get; set; }
        [StringLength(10)]
        public string UserName { get; set; } = null!;

        public int UserId { get; set; }

        [Range(1, 10)]
        public int PlanId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime SubscriptionStartDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime SubscriptionEndDate { get; set; }

        [Range(1, 10)]
        [DataType(DataType.Currency)]

        public double AmountPaid { get; set; }

        [StringLength(50)]
        public string PaymentMode { get; set; }=null!;

        [StringLength(12)]
        public string SubscriptionStatus { get; set; }=null !;

        public virtual SubscriptionPlan SubscriptionPlan { get; set; }
        public virtual SubscriptionCancellation SubscriptionCancellation { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
