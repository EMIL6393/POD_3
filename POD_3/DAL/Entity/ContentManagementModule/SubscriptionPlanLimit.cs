using System.ComponentModel.DataAnnotations;

namespace POD_3.DAL.Entity.ContentManagementModule
{
    public class SubscriptionPlanLimit
    {
        [Range(1,10)]
        public int Id { get; set; }

        [StringLength(100)]
        public string PlanName { get; set; } = null!;

        [Range(1, 10)]
        public int MonthlyScheduledPostLimit { get; set; }
    }
}
