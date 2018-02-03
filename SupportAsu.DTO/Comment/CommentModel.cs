using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportAsu.DTO.Comment
{
    public class CommentModel
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        [Required(ErrorMessage ="Поле обов'язкове для заповнення")]
        public string Text { get; set; }
    }
}
