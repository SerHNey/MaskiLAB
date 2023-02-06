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
    }
}
