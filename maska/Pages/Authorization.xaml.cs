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

namespace maska.Pages
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        public Authorization()
        {
            InitializeComponent();
        }
        public int vx = 0;

        List<Users> users = new List<Users>();

        private void reg_Click(object sender, RoutedEventArgs e)
        {
            Manager.frame.Navigate(new Registration());
        }

        private void Entre_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string klients = login.Text;
            string pas = password.Password;
            int count = CurrentList.db.Users.Count();
            users = CurrentList.db.Users.ToList();
            for (int i = 0; i < count; i++)
            {
                if (users[i].login == klients)
                {
                    if (users[i].password == pas)
                    {
                        CurrentList.user = users[i];
                        Manager.frame.Navigate(new Home());
                        vx = 1;
                        break;
                    }
                }
            }
            if (vx == 0)
            {
                MessageBox.Show("Неверный логин или пароль");
            }
        }

        private void Reg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Manager.frame.Navigate(new Registration());
        }

        private void Gues_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Manager.frame.Navigate(new Home());
        }
    }
}
