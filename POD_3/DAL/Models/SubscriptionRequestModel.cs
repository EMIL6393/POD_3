using System.ComponentModel.DataAnnotations;

namespace POD_3.DAL.Models
{
    public class SubscriptionRequestModel
    {
        [StringLength(10)]
        public string UserName { get; set; } = null!;

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [StringLength(150, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public string PlanName{ get; set; }

        public int planDuration { get; set; }

        public double AmountPaid { get; set; }

        [StringLength(50)]
        public string PaymentMode { get; set; } = null!;
    }
}
