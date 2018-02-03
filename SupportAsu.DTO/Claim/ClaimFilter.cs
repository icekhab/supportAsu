using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportAsu.DTO.Claim
{
    public class ClaimFilter
    {
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string Author { get; set; }
        public List<int> Status { get; set; }
    }
}
