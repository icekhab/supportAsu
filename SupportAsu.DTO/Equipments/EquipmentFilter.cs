using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportAsu.DTO.Equipments
{
    public class EquipmentFilter
    {
        public string Number { get; set; }
        public List<int> Auditories { get; set; }

        //public List<int> AuditotyList => !string.IsNullOrWhiteSpace(Auditories) ? Auditories.Split(',').Select(int.Parse).ToList() : new List<int>();
    }
}
