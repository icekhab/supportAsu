using SupportAsu.DTO.ClaimTask;
using SupportAsu.DTO.Dictionary;
using SupportAsu.DTO.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportAsu.DTO.Task
{
    public class TaskModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле 'Назва' обов'язкове для заповнення")]
        public string Title { get; set; }
        //[Required(ErrorMessage = "Поле 'Опис' обов'язкове для заповнення")]
        public string Text { get; set; }
        [Required(ErrorMessage = "Поле 'Дата з' обов'язкове для заповнення")]
        public DateTime DateStart { get; set; }
        [Required(ErrorMessage = "Поле 'Дата по' обов'язкове для заповнення")]
        public DateTime DateEnd { get; set; }
        public DicValueSmallModel Auditory { get; set; }
        //public int? AuditoryId { get; set; }
        public DicValueSmallModel Status { get; set; }
        public UserSmallModel Resposible { get; set; }
        public List<UserSmallModel> Executors { get; set; }
        public List<TaskOrClaimModel> Claims { get; set; }
        public TaskOrClaimModel MainTask { get; set; }
        public List<TaskModel> ChildTasks { get; set; }
        public bool IsMainTask => MainTask == null;
    }
}
