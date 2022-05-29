using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyPlanner.Models
{
    public class Task
    {
        // задача, к-я не зависит от длительности дня.
        // может быть разбита на подзадачи
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }
        public bool Done { get; set; }
        public virtual User User { get; set; } // связь с таблицой Пользователи
        public virtual Group Group { get; set; } // связь с таблицой Группы
        public virtual IEnumerable<Subtask> Subtasks { get; set; } // связь с таблицой Цели
        //public bool IsDeleted { get; set; }
    }
}
