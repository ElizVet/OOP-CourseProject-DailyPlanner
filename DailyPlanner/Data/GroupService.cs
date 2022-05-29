using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Text.RegularExpressions;
using System.Net.Mail;
using DailyPlanner.Models;

namespace DailyPlanner.Data
{
    public class GroupService
    {
        public static Models.Group[] GetGroups()
        {
            DailyPlannerDB dB = new DailyPlannerDB();
            return dB.Groups.Where(el => el.User.Id == DailyPlannerDB.CurrentUser.Id).ToArray();
        }

        public static Models.Group GetGroupbyNameandUser(string groupname, User user)
        {
            DailyPlannerDB dB = new DailyPlannerDB();
            return dB.Groups.Where(g => g.Name == groupname && 
            g.User.Id == user.Id).FirstOrDefault();
        }

        public static void GetGroupToAdd(Models.Group group)
        {
            DailyPlannerDB dB = new DailyPlannerDB();
            if (dB.Groups.FirstOrDefault(q => q.Name == group.Name &&
            q.User.Id == DailyPlannerDB.CurrentUser.Id) == null)
            {
                group.User = dB.Users.SingleOrDefault(g => g.Id == group.User.Id);
                
                dB.Groups.Add(group);
                dB.SaveChanges();
            }
        }

        public static void DeleteGroupByName(Models.Group group)
        {
            TaskService.GetTasksbyGroupToDelete(group);

            using (DailyPlannerDB dailyPlanner = new DailyPlannerDB())
            {
                Models.Group group1 = new Models.Group();
                group1 = dailyPlanner.Groups.Where(el => el.User.Id == DailyPlannerDB.CurrentUser.Id &&
                el.Id == group.Id).FirstOrDefault();

                dailyPlanner.Groups.Attach(group1);
                dailyPlanner.Groups.Remove(group1);

                dailyPlanner.SaveChanges();
            }

        }
    }
}