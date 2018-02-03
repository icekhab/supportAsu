using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupportAsu.Model
{
    public class Task : EntityMobile
    {
        [Required]
        public int ResponsibleId { get; set; }

        [ForeignKey("ResponsibleId")]
        public User Responsible { get; set; }
        [MaxLength(Constants.TitleLength)]
        public string Title { get; set; }
        [MaxLength(int.MaxValue)]
        public string Text { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int? MainTaskId { get; set; }
        [ForeignKey("MainTaskId")]
        public virtual Task MainTask { get; set; }
        public int? AuditoryId { get; set; }
        [ForeignKey("AuditoryId")]
        public virtual DictionaryValue Auditory { get; set; }
        public int StatusId { get; set; }
        [ForeignKey("StatusId")]
        public virtual DictionaryValue Status { get; set; }
    }
}
