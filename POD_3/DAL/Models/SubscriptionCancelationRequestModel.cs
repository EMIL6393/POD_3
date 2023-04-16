using System.ComponentModel.DataAnnotations;

namespace POD_3.DAL.Models
{
    public class SubscriptionCancelationRequestModel
    {
        [StringLength(100)]
        public string CancellationReason { get; set; } = null!;
    }
}
