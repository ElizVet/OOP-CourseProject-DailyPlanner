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
using DailyPlanner.Models;
using DailyPlanner.Data;

namespace DailyPlanner
{
    /// <summary>
    /// Логика взаимодействия для AddSubtask.xaml
    /// </summary>
    public partial class AddSubtask : Window
    {
        public AddSubtask()
        {
            InitializeComponent();
        }

        private void bt_AddSubtask_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Subtask subtask = new Subtask();

                subtask.Description = tb_Description.Text;
                subtask.StartTime = DateTime.Parse(dp_StartDate.Text + " " +
                    Convert.ToDateTime(tp_StartTime.Text).ToLongTimeString());
                subtask.EndTime = DateTime.Parse(dp_EndDate.Text + " " +
                    Convert.ToDateTime(tp_EndTime.Text).ToLongTimeString());
                subtask.Done = false;

                try
                {
                    subtask.Task = TaskService.GetTaskbyId((Models.Task)MainPage.mainPage.lv_ListTasks.SelectedItem);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                SubtaskService.GetSubtaskToAdd(subtask);
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
