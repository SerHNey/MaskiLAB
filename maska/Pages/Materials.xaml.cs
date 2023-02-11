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
        public Frame frame;
        MaskiLABEntities db = MaskiLABEntities.GetContext();
        public Materials()
        {
            InitializeComponent();
            var allTypes = MaskiLABEntities.GetContext().MaterialType.ToList();
            allTypes.Insert(0, new MaterialType
            {
                Title = "Все типы"
            });
            var current = MaskiLABEntities.GetContext().Material.ToList();
            LViewTours.ItemsSource = current;
        }
        private void UpdateMaterial()
        {
            var currentTours = MaskiLABEntities.GetContext().Material.ToList();
        }
        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (search.Text != "" && LViewTours != null)
            {
                var filter_name = db.Material.ToList().Where(t => t.Title.ToLower().Contains(search.Text.ToLower()));
                LViewTours.ItemsSource = filter_name;
            }
            else
            {
                if (LViewTours != null)
                {
                    var current = db.Material.ToList();
                    LViewTours.ItemsSource = current;
                }

            }
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateMaterial();
        }

        private void CheckActual_Checked(object sender, RoutedEventArgs e)
        {
            UpdateMaterial();
        }
        private void Back(object sender, RoutedEventArgs e)
        {
            Manager.frame.Navigate(new Home(frame));
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
            LViewTours.ItemsSource = db.Material.ToList().OrderBy(material => material.Title);
        }

        private void ReverseByАlphabet_Click(object sender, RoutedEventArgs e)
        {
            LViewTours.ItemsSource = db.Material.ToList().OrderByDescending(material => material.Title);
        }

        private void SortByCost_Click(object sender, RoutedEventArgs e)
        {
            LViewTours.ItemsSource = db.Material.ToList().OrderBy(material => material.Cost);
        }

        private void ReverseByCost_Click(object sender, RoutedEventArgs e)
        {
            LViewTours.ItemsSource = db.Material.ToList().OrderByDescending(material => material.Cost);
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
            LViewTours.ItemsSource = db.Material.ToList();
        }

        private void ClothFilter_Click(object sender, RoutedEventArgs e)
        {
            LViewTours.ItemsSource = db.Material.ToList().Where(material => material.MaterialTypeID == 1);
        }

        private void KernelFilter_Click(object sender, RoutedEventArgs e)
        {
            LViewTours.ItemsSource = db.Material.ToList().Where(material => material.MaterialTypeID == 2);
        }

        private void SiliconeFilter_Click(object sender, RoutedEventArgs e)
        {
            LViewTours.ItemsSource = db.Material.ToList().Where(material => material.MaterialTypeID == 3);
        }

        private void RubberBandFilter_Click(object sender, RoutedEventArgs e)
        {
            LViewTours.ItemsSource = db.Material.ToList().Where(material => material.MaterialTypeID == 4);
        }

        private void CottonFilter_Click(object sender, RoutedEventArgs e)
        {
            LViewTours.ItemsSource = db.Material.ToList().Where(material => material.MaterialTypeID == 5);
        }
    }
}
