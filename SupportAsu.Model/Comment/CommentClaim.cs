using System.ComponentModel.DataAnnotations.Schema;

namespace SupportAsu.Model
{
    public class CommentClaim : Comment
    {
        [ForeignKey("ItemId")]
        public virtual Claim Claim { get; set; } 
    }
}
