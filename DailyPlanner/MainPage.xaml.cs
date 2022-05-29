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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DailyPlanner
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public static MainPage mainPage;
        public MainPage()
        {
            InitializeComponent();
            mainPage = this;
            calendar.SelectedDate = DateTime.Now;
            
        }

        private void bt_AddTask_Click(object sender, RoutedEventArgs e)
        {
            AddTask addTask = new AddTask();
            addTask.ShowDialog();

            try
            {
                lv_ListTasks.ItemsSource = TaskService.GetTasksbyDate(dateTime: calendar.SelectedDate.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                lv_ListTasks.ItemsSource = TaskService.GetTasksbyDate(calendar.SelectedDate.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bt_DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TaskService.GetTaskToDelete((Models.Task)lv_ListTasks.SelectedItem);
                MessageBox.Show("Успешно!");

                try
                {
                    lv_ListTasks.ItemsSource = TaskService.GetTasksbyDate(dateTime: calendar.SelectedDate.Value);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bt_DeleteSubtask_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SubtaskService.GetSubtaskToDelete((Subtask)lv_ListSubtasks.SelectedItem);
                MessageBox.Show("Успешно!");

                try
                {
                    lv_ListSubtasks.ItemsSource = SubtaskService.GetSubtasksbyTask((Models.Task)lv_ListTasks.SelectedItem);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bt_AddSubtask_Click(object sender, RoutedEventArgs e)
        {
            AddSubtask addSubtask = new AddSubtask();
            addSubtask.ShowDialog();

            try
            {
                lv_ListSubtasks.ItemsSource = SubtaskService.GetSubtasksbyTask((Models.Task)lv_ListTasks.SelectedItem);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public void lv_ListTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                lv_ListSubtasks.ItemsSource = SubtaskService.GetSubtasksbyTask((Models.Task)lv_ListTasks.SelectedItem);
            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void EditTask_Click(object sender, RoutedEventArgs e)
        {
            Models.Task task = (sender as Button)?.DataContext as Models.Task;
            EditTask editTask = new EditTask(task);
            editTask.ShowDialog();

            try
            {
                lv_ListTasks.ItemsSource = TaskService.GetTasksbyDate(dateTime: calendar.SelectedDate.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cb_Done_Checked(object sender, RoutedEventArgs e)
        {
            Models.Task task = (sender as CheckBox)?.DataContext as Models.Task;
            TaskService.DoneTask(task);
        }

        private void cb_NotDone_Unchecked(object sender, RoutedEventArgs e)
        {
            Models.Task task = (sender as CheckBox)?.DataContext as Models.Task;
            TaskService.NotDoneTask(task);
        }

        private void cb_DoneSubtask_Checked(object sender, RoutedEventArgs e)
        {
            Subtask subtask = (sender as CheckBox)?.DataContext as Subtask;
            SubtaskService.DoneSubtask(subtask);
        }

        private void cb_NotDoneSubtask_Unchecked(object sender, RoutedEventArgs e)
        {
            Subtask subtask = (sender as CheckBox)?.DataContext as Subtask;
            SubtaskService.NotDoneSubtask(subtask);
        }

        private void EditSubtask_Click(object sender, RoutedEventArgs e)
        {
            Subtask subtask = (sender as Button)?.DataContext as Subtask;
            EditSubtask editTask = new EditSubtask(subtask);
            editTask.ShowDialog();

            try
            {
                lv_ListSubtasks.ItemsSource = SubtaskService.GetSubtasksbyTask((Models.Task)lv_ListTasks.SelectedItem);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}