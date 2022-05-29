using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DailyPlanner.Models
{
    public class DailyPlannerDB : DbContext
    {
        public DailyPlannerDB() : base("DefaultConnection")
        {

        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Subtask> Subtasks { get; set; }

        public static User CurrentUser;
    }
}