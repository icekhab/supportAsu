using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportAsu.DTO.User
{
    public class UserModel
    {
        [Required(ErrorMessage ="Поле 'Логін' обов'язкове для заповнення")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Поле 'Ім'я' обов'язкове для заповнення")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Поле 'Прізвище' обов'язкове для заповнення")]
        public string SurName { get; set; }
        public string DisplayName { get; set; }
        [Required(ErrorMessage = "Поле 'Електронна пошта' обов'язкове для заповнення")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Не вірний формат поля 'Електронна пошта'")]
        //[EmailAddress(ErrorMessage = "Не вірний формат поля 'Електронна пошта'")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Поле 'Телефон' обов'язкове для заповнення")]
        public string Phone { get; set; }
    }
}
