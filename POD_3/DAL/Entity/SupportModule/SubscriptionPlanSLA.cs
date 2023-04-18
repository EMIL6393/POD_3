using System.ComponentModel.DataAnnotations;

namespace POD_3.DAL.Entity.SupportModule
{
    public class SubscriptionPlanSLA
    {
        [Key]
        [Range(1, 10)]
        public int Id { get; set; }

        [StringLength(20)]
        public string PlanName { get; set; } = null!;

        [Range(1, 10)]
        public int ExpectedSLAsInDays { get; set; }
    }
}
