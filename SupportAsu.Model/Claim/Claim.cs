using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupportAsu.Model
{
    public class Claim : EntityMobile
    {
        public int? UserId { get; set; }
        [ForeignKey("UserId ")]
        public virtual User User { get; set; }

        [Required(ErrorMessage = "Поле 'Тема' обов'язкове для заповнення")]
        [MaxLength(Constants.TitleLength,ErrorMessage = "Макстмальна довжина поля 'Тема' 60 символів")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Поле 'Опис проблеми' обов'язкове для заповнення")]
        [MaxLength(int.MaxValue)]
        public string Text { get; set; }
        public DateTime? CloseDate { get; set; }
        public int? AuditoryId { get; set; }
        [ForeignKey("AuditoryId")]
        public virtual DictionaryValue Auditory { get; set; }
        public int StatusId { get; set; }
        [ForeignKey("StatusId")]
        public virtual DictionaryValue Status { get; set; }

        [Required(ErrorMessage = "Поле 'Категорія' обов'язкове для заповнення")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual DictionaryValue Category { get; set; }
    }
}
