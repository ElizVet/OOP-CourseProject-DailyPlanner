using DailyPlanner.Data;
using DailyPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DailyPlanner
{
    /// <summary>
    /// Логика взаимодействия для EditTask.xaml
    /// </summary>
    public partial class EditTask : Window
    {
        public Models.Task task;
        public EditTask(Models.Task parTask)
        {
            InitializeComponent();
            task = parTask;
            cb_Group.ItemsSource = GroupService.GetGroups();

            dp_StartDate.SelectedDate = DateTime.Parse(task.StartDate.ToLongDateString());
            tp_StartTime.SelectedTime = DateTime.Parse(task.StartDate.ToLongTimeString());
            dp_EndDate.SelectedDate = DateTime.Parse(task.EndDate.ToLongDateString());
            tp_EndTime.SelectedTime = DateTime.Parse(task.EndDate.ToLongTimeString());
            tb_Description.Text = task.Description;
            cb_Group.Text = task.Group.Name;
            tb_Location.Text = task.Location;
        }

        private void bt_EditTask_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                task.Description = tb_Description.Text;
                task.Location = tb_Location.Text;

                task.StartDate = DateTime.Parse(dp_StartDate.Text + " " + Convert.ToDateTime(tp_StartTime.Text).ToLongTimeString());
                task.EndDate = DateTime.Parse(dp_EndDate.Text + " " + Convert.ToDateTime(tp_EndTime.Text).ToLongTimeString());
                task.User = UserService.GetCurrentUser();

                if (GroupService.GetGroupbyNameandUser(cb_Group.Text, task.User) != null) // если группа существует
                {
                    task.Group = GroupService.GetGroupbyNameandUser(cb_Group.Text, task.User); // берем ее
                }
                else // если группа не существует
                {
                    GroupService.GetGroupToAdd(new Group { Name = cb_Group.Text, User = UserService.GetCurrentUser() }); // добавляем группу
                    task.Group = GroupService.GetGroupbyNameandUser(cb_Group.Text, task.User);
                }

                TaskService.GetTaskToEdit(task);
                MessageBox.Show("Задача успешно изменена!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
