using SupportAsu.DTO.Comment;
using SupportAsu.DTO.Dictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportAsu.DTO.Claim
{
    public class ClaimViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public DateTime? CloseDate { get; set; }
        public string Author { get; set; }
        public bool isNeedApprove { get; set; }
        public DicValueSmallModel Status { get; set; }
        public List<CommentModel> Comments { get; set; }
        public string Text { get; set; }
        public DicValueSmallModel Auditory { get; set; }
    }
}
