using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportAsu.Model
{
    public class PurchaseDetail : EntityMobile
    {
        [Required]
        public int PurchaseId { get; set; }
        [ForeignKey("PurchaseId")]
        public virtual Purchase Purchase { get; set; }

        [Required(ErrorMessage = "Поле 'Назва' обов'язкове для заповнення")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Поле 'Кількість' обов'язкове для заповнення")]
        public int Count { get; set; }
        public string Note { get; set; }
    }
}
