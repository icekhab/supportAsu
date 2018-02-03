using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportAsu.DTO.Equipments
{
    public class EquipmentDto
    {
        public int Id { get; set; }

        [DisplayName("Назва")]
        public string Name { get; set; }
       
        public int AuditoryId { get; set; }

        [DisplayName("Аудиторія")]
        public string Auditory { get; set; }

        [DisplayName("Стан")]
        public string State { get; set; }

        [DisplayName("Примітка")]
        public string Note { get; set; }

        [DisplayName("Інвентарний номер")]
        public string Number { get; set; }
    }
}
