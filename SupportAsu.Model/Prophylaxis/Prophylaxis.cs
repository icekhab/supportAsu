using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportAsu.Model
{
    public class Prophylaxis : EntityMobile
    {
        [Required(ErrorMessage ="Поле 'Аудиторія' обов'язкове для заповнення")]
        public int AuditoryId { get; set; }
        [ForeignKey("AuditoryId")]
        public virtual DictionaryValue Auditory { get; set; }

        [Required(ErrorMessage = "Поле 'Відповідальний' обов'язкове для заповнення")]
        public int ResponsibleId { get; set; }

        [ForeignKey("ResponsibleId")]
        public User Responsible { get; set; }

        [Required(ErrorMessage = "Поле 'День тижня' обов'язкове для заповнення")]
        public int DayId { get; set; }
        [ForeignKey("DayId")]
        public virtual DictionaryValue Day { get; set; }

        [Required(ErrorMessage = "Поле 'Пара' обов'язкове для заповнення")]
        public int LessonId { get; set; }
        [ForeignKey("LessonId")]
        public virtual DictionaryValue Lesson { get; set; }

        public string Note { get; set; }
    }
}
