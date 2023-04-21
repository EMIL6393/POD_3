using POD_3.DAL.Entity.SubscriptionManagementMod;
using System.ComponentModel.DataAnnotations;

namespace POD_3.DAL.Entity
{
    public class User
    {

        [Key]
        public int Id { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;

        [StringLength(150, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        public string Role { get; set; } = "Customer";

        public virtual UserSubscription? UserSubscription { get; set; }
    }
}
