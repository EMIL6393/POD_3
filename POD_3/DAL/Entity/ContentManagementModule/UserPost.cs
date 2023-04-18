using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POD_3.DAL.Entity.ContentManagementModule
{
    public class UserPost
    {
        [Range(1,10)]
        public int Id { get; set; }

        [DataType(DataType.DateTime)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime PostedOn { get; set; } = DateTime.Now;

        public bool IsScheduledPost { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublishOnDate { get; set; }

        [DataType(DataType.Time)]
        public DateTime PublishOnTime { get; set; }

        [StringLength(10)]
        public string PostType { get; set; } = null!;


        public string? PostContentText { get; set; }

        [Url]
        [StringLength(200)]
        public string? PostAttachmentURL { get; set; }


        [StringLength(10)]
        public string? PostStatus { get; set; }


        [StringLength(10)]
        public string? UserName { get; set; }

        [StringLength(20)]
        public string? SocialNetworkType { get; set; }
    }
}
