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

namespace maska.Pages
{
    /// <summary>
    /// Логика взаимодействия для Basket.xaml
    /// </summary>
    public partial class Basket : Page
    {
        public Basket()
        {
            InitializeComponent();
            LViewTours.ItemsSource = GetItems();
        }

        private List<BasketItem> GetItems()
        {
            List<BasketItem> items = new List<BasketItem>();
            foreach (var item in BasketList.products)
            {
                items.Add(new BasketItem(item.Title, item.Cost,item.Image));
            }
            foreach (var item in BasketList.materials)
            {
                items.Add(new BasketItem(item.Title, item.Cost, item.Image));
            }
            count.Content = items.Count;
            decimal pr = 0;
            foreach (var item in items)
            {
                pr += item.Cost;
            }
            prize.Content = pr.ToString();
            return items;
        }
        class BasketItem
        {
            public string Title { get; set; }
            public string Image { get; set; }

            public decimal Cost { get; set; }
            public BasketItem(string title, decimal cost, string image)
            {
                Title = title;
                Cost = cost;
                Image = image;
            }
        }
        private void Back(object sender, RoutedEventArgs e)
        {
            Manager.frame.GoBack();
        }
    }
}
