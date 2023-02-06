using maska.Pages;
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
    /// Логика взаимодействия для Maski.xaml
    /// </summary>
    public partial class Masks : Page
    { 
        MaskiLABEntities db = MaskiLABEntities.GetContext();
        public Masks()
        {
            InitializeComponent();
            var allTypes = db.ProductType.ToList();
            allTypes.Insert(0, new ProductType
            {
                Title = "Все типы"
            });
            var current = db.Product.ToList(); 
            LViewTours.ItemsSource = current;

            
        }
        private void UpdateMaski()
        {
            var currentTours = MaskiLABEntities.GetContext().Product.ToList();
        }
        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (search.Text != "" && LViewTours != null)
            {
                var filter_name = db.Product.ToList().Where(t => t.Title.ToLower().Contains(search.Text.ToLower()));
                LViewTours.ItemsSource = filter_name;
            }
            else
            {
                if(LViewTours != null)
                {
                    var current = db.Product.ToList();
                    LViewTours.ItemsSource = current;
                }
                
            }
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateMaski();
        }

        private void CheckActual_Checked(object sender, RoutedEventArgs e)
        {
            UpdateMaski();
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            Manager.frame.Navigate(new Home());
        }

        private void Basket_Click(object sender, RoutedEventArgs e)
        {
            Manager.frame.Navigate(new Basket());
        }

    }
}
