using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupportAsu.Model
{
    public class Comment : EntityMobile
    {
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        [MaxLength(int.MaxValue)]
        public string Text { get; set; }
        public int ItemId { get; set; }
    }
}
