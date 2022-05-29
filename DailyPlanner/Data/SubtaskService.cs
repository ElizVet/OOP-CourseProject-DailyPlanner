using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DailyPlanner.Models;
using System.ComponentModel;
using System.Windows;

namespace DailyPlanner.Data
{
    public class SubtaskService
    {
        public static Subtask[] GetSubtasksbyTask(Models.Task task)
        {
            try
            {
                DailyPlannerDB DB = new DailyPlannerDB();
                DB.Configuration.ProxyCreationEnabled = false;

                return DB.Subtasks.Where(el => el.Task.Id == task.Id).ToArray();
            }
            catch(Exception)
            {
                return null;
            }
        }

        public static void GetSubtaskToAdd(Models.Subtask subtask)
        {
            using (DailyPlannerDB dailyPlanner = new DailyPlannerDB())
            {
                subtask.Task = dailyPlanner.Tasks.SingleOrDefault(s => s.Id == subtask.Task.Id);
                dailyPlanner.Subtasks.Add(subtask);

                dailyPlanner.SaveChanges();
            }
        }

        public static void GetSubtaskToDelete(Subtask subtask)
        {
            using (DailyPlannerDB dailyPlanner = new DailyPlannerDB())
            {
                Subtask subtask1 = new Subtask();
                subtask1 = dailyPlanner.Subtasks.Where(el => el.Id == subtask.Id).FirstOrDefault();
                dailyPlanner.Subtasks.Attach(subtask1);
                dailyPlanner.Subtasks.Remove(subtask1);

                dailyPlanner.SaveChanges();
            }
        }

        public static void GetSubtasksbyTaskToDelete(Models.Task task)
        {
            using (DailyPlannerDB dailyPlanner = new DailyPlannerDB())
            {
                foreach (Subtask subtask in dailyPlanner.Subtasks.Where(el => el.Task.Id == task.Id))
                    dailyPlanner.Subtasks.Remove(subtask);

                dailyPlanner.SaveChanges();
            }
        }

        public static void DoneSubtask(Subtask subtask)
        {
            using (DailyPlannerDB dailyPlanner = new DailyPlannerDB())
            {
                var subtask1 = dailyPlanner.Subtasks.Where(el => el.Id == subtask.Id).FirstOrDefault();
                subtask1.Done = true;

                dailyPlanner.SaveChanges();
            }
        }

        public static void NotDoneSubtask(Subtask subtask)
        {
            using (DailyPlannerDB dailyPlanner = new DailyPlannerDB())
            {
                var subtask1 = dailyPlanner.Subtasks.Where(el => el.Id == subtask.Id).FirstOrDefault();
                subtask1.Done = false;

                dailyPlanner.SaveChanges();
            }
        }

        public static void GetSubtaskToEdit(Subtask subtask)
        {
            using (DailyPlannerDB dailyPlanner = new DailyPlannerDB())
            {
                var subtask1 = dailyPlanner.Subtasks.Where(el => el.Id == subtask.Id).FirstOrDefault();

                subtask1.Description = subtask.Description;
                subtask1.Done = subtask.Done;
                subtask1.StartTime = subtask.StartTime;
                subtask1.EndTime = subtask.EndTime;
                subtask1.Id = subtask.Id;
                subtask1.Task = dailyPlanner.Tasks.SingleOrDefault(el => el.Id == subtask.Task.Id);

                dailyPlanner.SaveChanges();
            }
        }

    }
}
