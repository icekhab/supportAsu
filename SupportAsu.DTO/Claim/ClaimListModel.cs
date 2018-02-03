using SupportAsu.DTO.Dictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportAsu.DTO.Claim
{
    public class ClaimListModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public string Author { get; set; }
        public DicValueSmallModel Status { get; set; }
        public bool IsView { get; set; }
    }
}
