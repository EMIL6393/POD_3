using POD_3.DAL.Entity.SubscriptionManagementMod;
using System.ComponentModel.DataAnnotations;

namespace POD_3.DAL.Models
{
    public class SubscriptionPlanModel
    {

        [StringLength(100)]
        public string Name { get; set; } = null!;

        [Range(1, 10)]
        public int PricePerMonth { get; set; }

    }
}
