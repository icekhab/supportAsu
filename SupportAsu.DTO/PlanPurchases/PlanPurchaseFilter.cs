using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportAsu.DTO.PlanPurchases
{
    public class PlanPurchaseFilter
    {
        public string Name { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}
