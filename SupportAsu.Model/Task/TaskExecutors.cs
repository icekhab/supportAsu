using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupportAsu.Model
{
    public class TaskExecutors : EntityMobile
    {
        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        public int TaskId { get; set; }
        [ForeignKey("TaskId")]
        public virtual Task Task { get; set; }
    }
}
