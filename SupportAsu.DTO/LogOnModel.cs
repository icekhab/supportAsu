using System.ComponentModel.DataAnnotations;

namespace SupportAsu.DTO
{
    public class LogOnModel
    {
        [Required(ErrorMessage ="Поле 'Логін' є обв'язковим")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Поле 'Пароль' є обв'язковим")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
