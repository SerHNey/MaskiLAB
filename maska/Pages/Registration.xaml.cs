﻿using System;
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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        public Frame frame1;
        public Registration(Frame frame)
        {
            InitializeComponent();
            frame1 = frame;
        }


        private void registration_Click(object sender, MouseButtonEventArgs e)
        {
            string log = login.Text;
            string pas = password.Text;
            string pas1 = password_Copy.Text;
            if (log != "")
            {
                if (pas != "")
                {
                    if (pas1 != "")
                    {
                        if (pas == pas1)
                        {
                            Users user = new Users();
                            int count = MaskiLABEntities.GetContext().Users.Count();
                            user.login = log;

                            user.password = pas;
                            MaskiLABEntities.GetContext().Users.Add(user);
                            MaskiLABEntities.GetContext().SaveChanges();
                            frame1.Navigate(new Authorization(frame1));
                        }
                        else
                        {
                            MessageBox.Show("Пароли не совпадают");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Повторите пароль");
                    }
                }
                else
                {
                    MessageBox.Show("Введите пароль");
                }
            }
            else
            {
                MessageBox.Show("Введите логин");
            }

        }

        private void login_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void password_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void password_Copy_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            frame1.Navigate(new Authorization(frame1));
        }

        private void registration_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            frame1.Navigate(new Authorization(frame1));
        }

    }
}
