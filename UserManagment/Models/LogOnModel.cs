using System.ComponentModel.DataAnnotations;


namespace UserManagment.Models
{
    public class LogOnModel
    {
        [Required]
        [Display(Name ="Логін")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}