using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POD_3.DAL.Entity.AccountManagementMod
{
    public class SocialAccountTracker
    {
        [Range(1,10)]
        public int Id { get; set; }

        [Range(1, 10)]
        public int AccountId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DataType(DataType.DateTime)]
        public DateTime? Date { get; set; }

        [StringLength(25)]
        public string Action { get; set; } = null!;

        public virtual UserSocialAccount UserSocialAccount { get; set; }
    }
}
