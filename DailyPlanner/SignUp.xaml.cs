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
using DailyPlanner.Models;
using DailyPlanner.Data;
using Microsoft.Win32;

namespace DailyPlanner
{
    /// <summary>
    /// Логика взаимодействия для SignUp.xaml
    /// </summary>
    public partial class SignUp : Page
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void OpenImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.ShowDialog();
            gd_img.Visibility = Visibility.Visible;
            this.UpdateLayout();
            tb_photopath.Text = file.FileName;
            if (!String.IsNullOrEmpty(tb_photopath.Text))
            {
                user_img.ImageSource = new BitmapImage(new Uri(tb_photopath.Text));
            }
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                User u = new User();
                u.Login = tb_login.Text;
                u.Password = tb_password.Password;
                u.Email = tb_email.Text;
                u.PhotoPath = tb_photopath.Text;

                UserService.AddUser(u);
                MessageBox.Show("Пользователь успешно добавлен!");
                MainWindow.mainWindow.MainFrame.Content = new SignIn();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.mainWindow.MainFrame.Content = new SignIn();
        }
    }
}