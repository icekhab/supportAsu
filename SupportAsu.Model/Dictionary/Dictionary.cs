using System.ComponentModel.DataAnnotations;

namespace SupportAsu.Model
{
    public class Dictionary : EntityMobile
    {
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Code { get; set; }
    }
}
