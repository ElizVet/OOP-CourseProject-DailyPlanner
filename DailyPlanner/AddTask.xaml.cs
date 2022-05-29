using System;
using System.Collections.Generic;
using System.Globalization;
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
using DailyPlanner.Data;
using DailyPlanner.Models;

namespace DailyPlanner
{
    /// <summary>
    /// Логика взаимодействия для AddTask.xaml
    /// </summary>
    public partial class AddTask : Window
    {
        public AddTask()
        {
            InitializeComponent();
            dp_StartDate.SelectedDate = MainPage.mainPage.calendar.SelectedDate;
            cb_Group.ItemsSource = GroupService.GetGroups();
        }

        private void bt_AddTask_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Models.Task task = new Models.Task();

                task.Description = tb_Description.Text;
                task.Location = tb_Location.Text;
                task.Done = false;

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
                
                TaskService.GetTaskToAdd(task);
                MessageBox.Show("Задача успешно добавлена!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
