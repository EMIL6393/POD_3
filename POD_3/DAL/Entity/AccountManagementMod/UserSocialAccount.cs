using System.ComponentModel.DataAnnotations;

namespace POD_3.DAL.Entity.AccountManagementMod
{
    public class UserSocialAccount
    {
        [Required]
        [Range(1, 10)]
        public int Id { get; set; }

        [Range(1, 10)]
        public int SocialAccountTypeId { get; set; }

        [StringLength(100)]

        public string LoginId { get; set; }= null!;

        [StringLength(100)]
        public string EncryptedPassword { get; set; } = null!;

        [StringLength(10)]
        public string UserName { get; set; } = null!;

        [StringLength(25)]
        public string SubscriptionName { get; set; }=null!;

        public virtual SocialAccountType SocialAccountType { get; set; }

        public virtual List<SocialAccountTracker> SocialAccountTrackers { get; set; }
    }
}
