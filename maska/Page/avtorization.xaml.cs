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

namespace maska
{
    /// <summary>
    /// Логика взаимодействия для avtorization.xaml
    /// </summary>
    public partial class avtorization : Page
    {
             public Frame frame1;
        public int vx = 0;
        public avtorization(Frame frame)
        {
            frame1 = frame;
            InitializeComponent();
        }

        private void login_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void password_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        List<maska.Users> users = new List<maska.Users>();

        private void reg_Click(object sender, RoutedEventArgs e)
        {
            frame1.Navigate(new Registration(frame1));
        }

        private void Entre_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string klients = login.Text;
            string pas = password.Password;
            int count = Entities.GetContext().Users.Count();
            users = Entities.GetContext().Users.ToList();
            for (int i = 0; i < count; i++)
            {
                if (users[i].login == klients)
                {
                    if (users[i].password == pas)
                    {
                        frame1.Navigate(new Glavnaya(users[i].login, frame1));
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
            frame1.Navigate(new Registration(frame1));
        }

        private void Gues_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            frame1.Navigate(new Glavnaya("Glavn", frame1));
        }
    }
}

