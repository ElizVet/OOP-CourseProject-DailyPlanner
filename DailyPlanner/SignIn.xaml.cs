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

namespace DailyPlanner
{
    /// <summary>
    /// Логика взаимодействия для SignIn.xaml
    /// </summary>
    public partial class SignIn : Page
    {
        public SignIn()
        {
            InitializeComponent();
        }

        private void ButtonSignIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(tb_login.Text))
                {
                    if (!String.IsNullOrEmpty(tb_password.Password))
                    {
                        try
                        {
                            UserService.GetUserbyLoginPassword(tb_login.Text, tb_password.Password);
                            MainWindow.mainWindow.MainFrame.Content = new MainPage();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ButtonSignUp_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.mainWindow.MainFrame.Content = new SignUp();
        }
    }
}