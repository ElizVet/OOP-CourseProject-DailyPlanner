using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyPlanner.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public bool IsDeleted { get; set; }
        public virtual User User { get; set; }
        public virtual IEnumerable<Task> Tasks { get; set; }
    }
}