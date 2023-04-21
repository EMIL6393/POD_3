using System.ComponentModel.DataAnnotations;

namespace POD_3.DAL.Models
{
    public class LoginRequestModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
