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
using maska.Pages;

namespace maska
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Manager.frame = MainFrame;
            Manager.frame.Navigate(new Authorization());
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CurrentList.db = MaskiLABEntities.GetContext();
            CurrentList.products = CurrentList.db.Product.ToList();
            CurrentList.materials = CurrentList.db.Material.ToList();
        }
    }
}
