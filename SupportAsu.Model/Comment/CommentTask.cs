using System.ComponentModel.DataAnnotations.Schema;

namespace SupportAsu.Model
{
    public class CommentTask : Comment
    {
        [ForeignKey("ItemId")]
        public virtual Task Task { get; set; }
    }
}
