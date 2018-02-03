using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupportAsu.Model
{
    public class DictionaryValue : EntityMobile
    {
        public int DictionaryId { get; set; }
        [ForeignKey("DictionaryId")]
        public virtual Dictionary Dictionary { get; set; }
        [MaxLength(int.MaxValue)]
        public string Value { get; set; }
        [MaxLength(50)]
        public string Code { get; set; }
    }
}
