
using System.ComponentModel.DataAnnotations.Schema;

namespace SupportAsu.Model
{
    public class ClaimTask : EntityMobile
    {
        public int ClaimId { get; set; }
        [ForeignKey("ClaimId")]
        public Claim Claim { get; set; }

        public int TaskId { get; set; }
        [ForeignKey("TaskId")]
        public Task Task { get; set; }

    }
}
