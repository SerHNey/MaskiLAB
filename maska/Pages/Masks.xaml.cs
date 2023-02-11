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
        private static MaskiLABEntities db = MaskiLABEntities.GetContext();
        public Masks(Frame frame1)
        {
            InitializeComponent();
            
            LViewTours.ItemsSource = db.Product.ToList();
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
            Manager.frame.GoBack();
        }

        private void Basket_Click(object sender, RoutedEventArgs e)
        {
            Manager.frame.Navigate(new Basket());
        }

        private void Buy_click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null)
                return;
            Product item = button.DataContext as Product;
            BasketList.products.Add(item);
        }

        private void FilerBtn_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).ContextMenu.IsEnabled = true;
            (sender as Button).ContextMenu.PlacementTarget = (sender as Button);
            (sender as Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            (sender as Button).ContextMenu.IsOpen = true;
        }

        private void SortBtn_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).ContextMenu.IsEnabled = true;
            (sender as Button).ContextMenu.PlacementTarget = (sender as Button);
            (sender as Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            (sender as Button).ContextMenu.IsOpen = true;

        }

        private void SortByАlphabet_Click(object sender, RoutedEventArgs e)
        {
            LViewTours.ItemsSource = db.Product.ToList().OrderBy(product => product.Title);
        }

        private void ReverseByАlphabet_Click(object sender, RoutedEventArgs e)
        {
            LViewTours.ItemsSource = db.Product.ToList().OrderByDescending(product => product.Title);
        }

        private void SortByCost_Click(object sender, RoutedEventArgs e)
        {
            LViewTours.ItemsSource = db.Product.ToList().OrderBy(product => product.Cost);
        }

        private void ReverseByCost_Click(object sender, RoutedEventArgs e)
        {
            LViewTours.ItemsSource = db.Product.ToList().OrderByDescending(product => product.Cost);
        }

        private void HalfMasksFilter_Click(object sender, RoutedEventArgs e)
        {
            LViewTours.ItemsSource = db.Product.ToList().Where(product => product.ProductTypeID == 1);
        }

        private void BandagesFilter_Click(object sender, RoutedEventArgs e)
        {
            LViewTours.ItemsSource = db.Product.ToList().Where(product => product.ProductTypeID == 2);
        }

        private void MasksFilter_Click(object sender, RoutedEventArgs e)
        {
            LViewTours.ItemsSource = db.Product.ToList().Where(product => product.ProductTypeID == 3);
        }

        private void RespiratorsFilter_Click(object sender, RoutedEventArgs e)
        {
            LViewTours.ItemsSource = db.Product.ToList().Where(product => product.ProductTypeID == 4);

        }

        private void OnTheFaceFilter_Click(object sender, RoutedEventArgs e)
        {
            LViewTours.ItemsSource = db.Product.ToList().Where(product => product.ProductTypeID == 5);
        }

        private void FullFaceFilter_Click(object sender, RoutedEventArgs e)
        {
            LViewTours.ItemsSource = db.Product.ToList().Where(product => product.ProductTypeID == 6);
        }

        private void ReplacementPartsFilter_Click(object sender, RoutedEventArgs e)
        {
            LViewTours.ItemsSource = db.Product.ToList().Where(product => product.ProductTypeID == 7);
        }

        private void SparePartsFilter_Click(object sender, RoutedEventArgs e)
        {
            LViewTours.ItemsSource = db.Product.ToList().Where(product => product.ProductTypeID == 8);
        }

        private void HoldersFilter_Click(object sender, RoutedEventArgs e)
        {
            LViewTours.ItemsSource = db.Product.ToList().Where(product => product.ProductTypeID == 9);
        }

        private void PreFiltersFilter_Click(object sender, RoutedEventArgs e)
        {
            LViewTours.ItemsSource = db.Product.ToList().Where(product => product.ProductTypeID == 10);
        }

        private void ClearFilter_Click(object sender, RoutedEventArgs e)
        {
            LViewTours.ItemsSource = db.Product.ToList();
        }
    }
}
