using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportAsu.DTO.Claim
{
    public class ClaimMobileDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Date { get; set; }
        public string StatusName { get; set; }
        public string StatusColor { get; set; }
        public string Author { get; set; }
        public bool IsView { get; set; }
    }
}
