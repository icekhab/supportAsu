using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportAsu.Model
{
    public class Equipment : EntityMobile
    {
        [Required(ErrorMessage = "Поле 'Назва' обов'язкове для заповнення")]
        [DisplayName("Назва")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Аудиторія")]
        public int AuditoryId { get; set; }
        [ForeignKey("AuditoryId ")]
        public virtual DictionaryValue Auditory { get; set; }
        [DisplayName("Стан")]
        public string State { get; set; }
        [DisplayName("Примітка")]
        public string Note { get; set; }
        [Required(ErrorMessage = "Поле 'Інвентарний номер' обов'язкове для заповнення")]
        [DisplayName("Інвентарний номер")]
        public string Number { get; set; }
    }
}
