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
    public partial class Maski : Page
    {
        public Frame frame1;
        public Maski(Frame frame)
        {
            InitializeComponent();
            var allTypes = MaskiLABEntities.GetContext().ProductType.ToList();
            allTypes.Insert(0, new ProductType
            {
                Title = "Все типы"
            });
            ComboType.ItemsSource = allTypes;
            ComboType.SelectedIndex = 0;
            frame = frame1;
            var current = MaskiLABEntities.GetContext().Product.ToList();
            LViewTours.ItemsSource = current;
        }
        private void UpdateMaski()
        {
            var currentTours = MaskiLABEntities.GetContext().Product.ToList();
        }
        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateMaski();
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateMaski();
        }

        private void CheckActual_Checked(object sender, RoutedEventArgs e)
        {
            UpdateMaski();
        }
    }
}
