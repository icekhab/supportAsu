using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportAsu.DTO.Task
{
    public class TaskClaimModel
    {
        public int ClaimId { get; set; }
        public int TaskId { get; set; }
        public string ClaimTitle { get; set; }
    }
}
