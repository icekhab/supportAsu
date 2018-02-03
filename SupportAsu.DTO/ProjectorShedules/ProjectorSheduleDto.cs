using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupportAsu.DTO.Dictionary;

namespace SupportAsu.DTO.ProjectorShedules
{
    public class ProjectorSheduleDto
    {
        public int Id { get; set; }
        public DicValueSmallModel Auditory { get; set; }
        public DicValueSmallModel Lesson { get; set; }
        public DicValueSmallModel Day { get; set; }
        public DicValueSmallModel Week { get; set; }
        public int ResponsibleId { get; set; }
        public string ResponsibleName { get; set; }
        public string Teacher { get; set; }
        public string Note { get; set; }
    }
}
