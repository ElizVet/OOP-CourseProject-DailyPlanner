using DailyPlanner.Models;
using DailyPlanner.Data;
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
    /// Логика взаимодействия для EditSubtask.xaml
    /// </summary>
    public partial class EditSubtask : Window
    {
        public Subtask subtask;
        public EditSubtask(Subtask parSubtask)
        {
            InitializeComponent();
            subtask = parSubtask;

            tb_Description.Text = subtask.Description;
            dp_StartDate.SelectedDate = DateTime.Parse(subtask.StartTime.ToLongDateString());
            tp_StartTime.SelectedTime = DateTime.Parse(subtask.StartTime.ToLongTimeString());
            dp_EndDate.SelectedDate = DateTime.Parse(subtask.EndTime.ToLongDateString());
            tp_EndTime.SelectedTime = DateTime.Parse(subtask.EndTime.ToLongTimeString());
        }

        private void bt_EditSubtask_Click(object sender, RoutedEventArgs e)
        {
            try
            {
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

                SubtaskService.GetSubtaskToEdit(subtask);
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
