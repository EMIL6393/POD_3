using System.ComponentModel.DataAnnotations;

namespace POD_3.DAL.Entity.SubscriptionManagementMod
{
    public class SubscriptionCancellation
    {
        [MaxLength(10)]
        public int Id { get; set; }

        [MaxLength(10)]
        public int SubscriptionId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CancellationDate { get; set; }

        [StringLength(100)]
        public string CancellationReason { get; set; }=null!;

        public virtual UserSubscription UserSubscription { get; set; }
    }

}
