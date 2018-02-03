using System.ComponentModel.DataAnnotations;

namespace SupportAsu.Model
{
    public class ViewHistory : EntityMobile
    {
        [MaxLength(Constants.UserNameLength)]
        public string UserName { get; set; }
        public int ItemId { get; set; }
    }
}
