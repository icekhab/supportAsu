using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupportAsu.DTO.Dictionary;

namespace SupportAsu.DTO.Task
{
    public class TaskMobileDto
    {
        public int Id { get; set; }
        public Slave Responsible { get; set; }
        public List<Slave> Executors { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }

        public DicValueSmallModel Status { get; set; }
        public string Title { get; set; }
    }

    public class Slave
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
