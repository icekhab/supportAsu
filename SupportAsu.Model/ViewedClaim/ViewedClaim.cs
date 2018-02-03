using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupportAsu.Model
{
    public class ViewedClaim : EntityMobile
    {
        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        [Required]
        public int ItemId { get; set; }

        [ForeignKey("ItemId")]
        public virtual Claim Claim { get; set; }
    }
}
