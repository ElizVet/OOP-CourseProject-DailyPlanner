using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Text.RegularExpressions;
using DailyPlanner.Models;
using System.Data.Entity.Core.Objects;

namespace DailyPlanner.Data
{
    public class TaskService
    {
        [Obsolete]
        public static Models.Task[] GetTasksbyDate(DateTime dateTime)
        {
            DailyPlannerDB DB = new DailyPlannerDB();
            DB.Configuration.ProxyCreationEnabled = false;

            return DB.Tasks.Include(g => g.Group).Where(
                el => el.User.Id == DailyPlannerDB.CurrentUser.Id 
                && EntityFunctions.TruncateTime(dateTime) >= EntityFunctions.TruncateTime(el.StartDate) &&
                EntityFunctions.TruncateTime(dateTime) <= EntityFunctions.TruncateTime(el.EndDate)).ToArray();
        }

        public static void GetTaskToAdd(Models.Task task)
        {
            using (DailyPlannerDB dailyPlanner = new DailyPlannerDB())
            {
                task.Group = dailyPlanner.Groups.SingleOrDefault(g => g.Id == task.Group.Id) ?? new Models.Group { Name = task.Group.Name };
                task.User = dailyPlanner.Users.SingleOrDefault(u => u.Id == task.User.Id);

                dailyPlanner.Tasks.Add(task);

                dailyPlanner.SaveChanges();
            }
        }

        internal static Models.Task GetTaskbyId(Models.Task task)
        {
            using (DailyPlannerDB dailyPlanner = new DailyPlannerDB())
            {
                dailyPlanner.Configuration.ProxyCreationEnabled = false;

                return dailyPlanner.Tasks.Include(g => g.Group).Where(
                el => el.User.Id == DailyPlannerDB.CurrentUser.Id && 
                el.Id == task.Id).FirstOrDefault();
            }
        }

        public static void GetTaskToDelete(Models.Task task)
        {
            SubtaskService.GetSubtasksbyTaskToDelete(task);
            using (DailyPlannerDB dailyPlanner = new DailyPlannerDB())
            {
                Models.Task task1 = new Models.Task();
                task1 = dailyPlanner.Tasks.Where(el => el.User.Id == DailyPlannerDB.CurrentUser.Id &&
                el.Id == task.Id).FirstOrDefault();
                dailyPlanner.Tasks.Attach(task1);
                dailyPlanner.Tasks.Remove(task1);

                dailyPlanner.SaveChanges();
            }
        }

        public static void GetTasksbyGroupToDelete(Models.Group group)
        {
            using (DailyPlannerDB dailyPlanner = new DailyPlannerDB())
            {
                foreach (Models.Task task in dailyPlanner.Tasks.Where(el => el.Group.Id == group.Id))
                    dailyPlanner.Tasks.Remove(task);

                dailyPlanner.SaveChanges();
            }
        }

        public static void DoneTask(Models.Task task)
        {
            using (DailyPlannerDB dailyPlanner = new DailyPlannerDB())
            {
                var task1 = dailyPlanner.Tasks.Where(el => el.User.Id == DailyPlannerDB.CurrentUser.Id &&
                el.Id == task.Id).FirstOrDefault();
                task1.Done = true;

                dailyPlanner.SaveChanges();
            }
        }

        public static void NotDoneTask(Models.Task task)
        {
            using (DailyPlannerDB dailyPlanner = new DailyPlannerDB())
            {
                var task1 = dailyPlanner.Tasks.Where(el => el.User.Id == DailyPlannerDB.CurrentUser.Id &&
                el.Id == task.Id).FirstOrDefault();
                task1.Done = false;

                dailyPlanner.SaveChanges();
            }
        }

        public static void GetTaskToEdit(Models.Task task)
        {
            using (DailyPlannerDB dailyPlanner = new DailyPlannerDB())
            {
                var task1 = dailyPlanner.Tasks.Where(el => el.Id == task.Id).FirstOrDefault();

                task1.Location = task.Location;
                task1.Description = task.Description;
                task1.Done = task.Done;
                task1.StartDate = task.StartDate;
                task1.EndDate = task.EndDate;
                task1.Group = dailyPlanner.Groups.SingleOrDefault(g => g.Id == task.Group.Id) ?? new Models.Group { Name = task.Group.Name };
                task1.Id = task.Id;

                dailyPlanner.SaveChanges();
            }
        }
    }
}