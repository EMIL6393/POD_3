using System.ComponentModel.DataAnnotations;

namespace POD_3.DAL.Entity.AccountManagementMod
{
    public class SocialAccountType
    {
        [Range(0,10)]
        public int Id { get; set; }

        [StringLength(25)]
        public string AccountType { get; set; } = null!;

        public virtual List<UserSocialAccount> UserSocialAccounts { get; set; }


    }
}
