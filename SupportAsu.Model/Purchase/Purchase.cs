using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportAsu.Model
{
    public class Purchase : EntityMobile
    {
        [Required(ErrorMessage = "Поле 'Назва' обов'язкове для заповнення")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле 'Дата' обов'язкове для заповнення")]
        public DateTime Date { get; set; }
        public string Note { get; set; }
    }
}
