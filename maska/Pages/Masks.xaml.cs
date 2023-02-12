using maska.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        IEnumerable<Product> currentList;
        public Masks()
        {
            InitializeComponent();
            currentList = CurrentList.products;
            LViewTours.ItemsSource = currentList;
        }
        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (search.Text != "" && LViewTours != null)
            {
                var filter_name = CurrentList.products.ToList().Where(t => t.Title.ToLower().Contains(search.Text.ToLower()));
                LViewTours.ItemsSource = filter_name;
            }
            else
            {
                if(LViewTours != null)
                {
                    var current = CurrentList.products.ToList();
                    LViewTours.ItemsSource = current;
                }
                
            }
        }

        protected void HandleDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var track = ((ListView)sender).SelectedItem as Product;
            if(track != null && CurrentList.user != null)
            {
                if(CurrentList.user.role == "admin")
                    Manager.frame.Navigate(new AddEditProducts(track));
            }
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
            currentList = currentList.OrderBy(product => product.Title);
            LViewTours.ItemsSource = currentList;
        }

        private void ReverseByАlphabet_Click(object sender, RoutedEventArgs e)
        {
            currentList = currentList.OrderBy(product => product.Title);
            LViewTours.ItemsSource = currentList;
        }

        private void SortByCost_Click(object sender, RoutedEventArgs e)
        {
            currentList = currentList.OrderBy(product => product.Cost);
            LViewTours.ItemsSource = currentList;
        }

        private void ReverseByCost_Click(object sender, RoutedEventArgs e)
        {
            currentList = currentList.OrderByDescending(product => product.Cost);
            LViewTours.ItemsSource = currentList;
        }

        private void HalfMasksFilter_Click(object sender, RoutedEventArgs e)
        {
            currentList = CurrentList.db.Product.ToList();
            currentList = currentList.Where(product => product.ProductTypeID == 1);
            LViewTours.ItemsSource = currentList;
        }

        private void BandagesFilter_Click(object sender, RoutedEventArgs e)
        {
            currentList = CurrentList.db.Product.ToList();
            currentList = currentList.Where(product => product.ProductTypeID == 2);
            LViewTours.ItemsSource = currentList;
        }

        private void MasksFilter_Click(object sender, RoutedEventArgs e)
        {
            currentList = CurrentList.db.Product.ToList();
            currentList = currentList.Where(product => product.ProductTypeID == 3);
            LViewTours.ItemsSource = currentList;
        }

        private void RespiratorsFilter_Click(object sender, RoutedEventArgs e)
        {
            currentList = CurrentList.db.Product.ToList();
            currentList = currentList.Where(product => product.ProductTypeID == 4);
            LViewTours.ItemsSource = currentList;
        }

        private void OnTheFaceFilter_Click(object sender, RoutedEventArgs e)
        {
            currentList = CurrentList.db.Product.ToList();
            currentList = currentList.Where(product => product.ProductTypeID == 5);
            LViewTours.ItemsSource = currentList;
        }

        private void FullFaceFilter_Click(object sender, RoutedEventArgs e)
        {
            currentList = CurrentList.db.Product.ToList();
            currentList = currentList.Where(product => product.ProductTypeID == 6);
            LViewTours.ItemsSource = currentList;
        }

        private void ReplacementPartsFilter_Click(object sender, RoutedEventArgs e)
        {
            currentList = CurrentList.db.Product.ToList();
            currentList = currentList.Where(product => product.ProductTypeID == 7);
            LViewTours.ItemsSource = currentList;
        }

        private void SparePartsFilter_Click(object sender, RoutedEventArgs e)
        {
            currentList = CurrentList.db.Product.ToList();
            currentList = currentList.Where(product => product.ProductTypeID == 8);
            LViewTours.ItemsSource = currentList;
        }

        private void HoldersFilter_Click(object sender, RoutedEventArgs e)
        {
            currentList = CurrentList.db.Product.ToList();
            currentList = currentList.Where(product => product.ProductTypeID == 9);
            LViewTours.ItemsSource = currentList;
        }

        private void PreFiltersFilter_Click(object sender, RoutedEventArgs e)
        {
            currentList = CurrentList.db.Product.ToList();
            currentList = currentList.Where(product => product.ProductTypeID == 10);
            LViewTours.ItemsSource = currentList;
        }

        private void ClearFilter_Click(object sender, RoutedEventArgs e)
        {
            currentList = CurrentList.db.Product.ToList();
            LViewTours.ItemsSource = currentList;
        }
    }
}
