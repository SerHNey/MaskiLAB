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
    public partial class Home : Page
    {
        public Frame frame1;
        public Home(Frame frame)
        {
            InitializeComponent();
            frame1 = frame;
        }

        private void ProductsCatalog(object sender, RoutedEventArgs e)
        {
            Manager.frame.Navigate(new Masks(frame1));
        }

        private void MaterialsCatalog(object sender, RoutedEventArgs e)
        {
            Manager.frame.Navigate(new Materials());
        }
    }
}
