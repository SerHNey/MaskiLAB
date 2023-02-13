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
    /// Логика взаимодействия для Materials.xaml
    /// </summary>
    public partial class Materials : Page
    {
        IEnumerable<Material> currentList;
        public Materials()
        {
            InitializeComponent();
            if (CurrentList.user != null)
            {
                if (CurrentList.user.role == 1)
                    Add.Visibility = Visibility.Visible;
            }
            currentList = CurrentList.materials.ToList();
            LViewTours.ItemsSource = currentList;
        }
        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (search.Text != "" && LViewTours != null)
            {
                var filter_name = currentList.Where(t => t.Title.ToLower().Contains(search.Text.ToLower()));
                LViewTours.ItemsSource = filter_name;
            }
            else
            {
                if (LViewTours != null)
                {
                    var current = currentList.ToList();
                    LViewTours.ItemsSource = current;
                }

            }
        }

        protected void HandleDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var track = ((ListView)sender).SelectedItem as Material;
            if (track != null && CurrentList.user != null)
            {
                if (CurrentList.user.role == 1)
                    Manager.frame.Navigate(new AddEditMaterials(track, this));
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Manager.frame.Navigate(new AddEditMaterials(null, this));
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            Manager.frame.Navigate(new Home());
        }
        private void Buy_click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null)
                return;
            Material item = button.DataContext as Material;
            BasketList.materials.Add(item);
        }

        private void Basket_Click(object sender, RoutedEventArgs e)
        {
            Manager.frame.Navigate(new Basket());
        }

        private void SortByАlphabet_Click(object sender, RoutedEventArgs e)
        {
            currentList = currentList.OrderBy(material => material.Title);
            LViewTours.ItemsSource = currentList;
        }

        private void ReverseByАlphabet_Click(object sender, RoutedEventArgs e)
        {
            currentList = currentList.OrderBy(material => material.Title);
            LViewTours.ItemsSource = currentList;
        }

        private void SortByCost_Click(object sender, RoutedEventArgs e)
        {
            currentList = currentList.OrderBy(material => material.Cost);
            LViewTours.ItemsSource = currentList;
        }

        private void ReverseByCost_Click(object sender, RoutedEventArgs e)
        {
            currentList = currentList.OrderByDescending(material => material.Cost);
            LViewTours.ItemsSource = currentList;
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

        private void ClearFilter_Click(object sender, RoutedEventArgs e)
        {
            currentList = CurrentList.db.Material.ToList();
            LViewTours.ItemsSource = currentList;
        }

        private void FilterByType_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi = e.OriginalSource as MenuItem;
            FilterByTypeName(mi.Header.ToString());
        }

        private void FilterByTypeName(string type)
        {
            currentList = CurrentList.db.Material.ToList();
            currentList = currentList.Where(material => material.MaterialType.Title == $"{type}\r\n");
            LViewTours.ItemsSource = currentList;
        }
    }
}
