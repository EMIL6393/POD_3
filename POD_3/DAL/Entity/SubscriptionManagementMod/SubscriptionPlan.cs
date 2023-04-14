using System.ComponentModel.DataAnnotations;

namespace POD_3.DAL.Entity.SubscriptionManagementMod
{
    public class SubscriptionPlan
    {
        [Key]
        [MaxLength(10)]
        public int PlanId { get; set; }

        [StringLength(100)]
        public string Name { get; set; } = null!;

        [MaxLength(10)]
        public int PricePerMonth { get; set; }

        public virtual List<UserSubscription>UserSubscriptions{ get; set; }
    }
}
