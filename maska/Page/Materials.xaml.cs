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
        public Frame frame1;
        public Materials(Frame frame)
        {
            InitializeComponent();
            var allTypes = MaskiLABEntities.GetContext().MaterialType.ToList();
            allTypes.Insert(0, new MaterialType
            {
                Title = "Все типы"
            });
            ComboType.ItemsSource = allTypes;
            ComboType.SelectedIndex = 0;
            frame = frame1;
            var current = MaskiLABEntities.GetContext().Material.ToList();
            LViewTours.ItemsSource = current;
        }
        private void UpdateMaterial()
        {
            var currentTours = MaskiLABEntities.GetContext().Material.ToList();
        }
        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateMaterial();
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateMaterial();
        }

        private void CheckActual_Checked(object sender, RoutedEventArgs e)
        {
            UpdateMaterial();
        }
    }
}
