using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportAsu.DTO.User
{
    public class ChangePasswordModel
    {
        public string Login { get; set; }
        [Required(ErrorMessage = "Поле 'Старий пароль' обов'язкове для заповнення")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "Поле 'Новий пароль' обов'язкове для заповнення")]
        public string NewPassword { get; set; }
    }
}
