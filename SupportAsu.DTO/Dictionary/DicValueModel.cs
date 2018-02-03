using System.ComponentModel.DataAnnotations;

namespace SupportAsu.DTO.Dictionary
{
    public class DicValueModel
    {
        [Display(Name="Код")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Поле 'Значення' обов'язкове для заповнення")]
        [Display(Name = "Значення")]
        public string Value { get; set; }
        public int Id { get; set; }
        public int DicId { get; set; }
    }
}
