using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyPlanner.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        //public string PasswordConfirmation { get; set; }
        public string Email { get; set; }
        public string PhotoPath { get; set; }
        public virtual IEnumerable<Task> Tasks { get; set; }
        public virtual IEnumerable<Group> Groups { get; set; }
    }
}