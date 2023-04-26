using System.ComponentModel.DataAnnotations;

namespace POD_3.DAL.Models
{
    public class AccountRequestModel
    {
        [StringLength(10)]
        public string UserName { get; set; } = null!;

        [StringLength(100)]
        public string LoginId { get; set; } = null!;

        [StringLength(150, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public string SocialAccount { get; set; }


    }
}
