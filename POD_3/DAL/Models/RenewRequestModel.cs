using System.ComponentModel.DataAnnotations;

namespace POD_3.DAL.Models
{
    public class RenewRequestModel
    {

        [StringLength(10)]
        public string UserName { get; set; } = null!;

        public string PlanName { get; set; }

        public int planDuration { get; set; }

        public int AmountPaid { get; set; }

        [StringLength(50)]
        public string PaymentMode { get; set; } = null!;

    }
}
