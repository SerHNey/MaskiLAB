using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Windows.Shell;

namespace maska.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditProducts.xaml
    /// </summary>
    public partial class AddEditProducts : Page
    {
        string imagePath;
        string pathTo;
        Product thisproduct;
        bool add = false;
        public AddEditProducts(Product product)
        {
            InitializeComponent();
            if(product != null )
            {
                Title.Text = product.Title;
                Cost.Text = product.Cost.ToString();
                CountPack.Text = product.ProductionPersonCount.ToString();
                int i = 0;
                foreach (var item in CurrentList.db.ProductType.ToList())
                {
                    Type.Items.Add(item.Title);
                    if(item.ID == product.ProductTypeID)
                        Type.SelectedIndex= i;
                    i++;
                }
                if(product.Image != null | product.Image == "")
                {
                string imagepath = product.Image;
                imagepath = imagepath.Replace("\\", "/");
                Regex reg = new Regex("/");
                imagepath = reg.Replace(imagepath, "../", 1);
                imagepath = System.IO.Path.GetFullPath(imagepath);
                imagepath = imagepath.Replace("\\bin", "");
                Image.ImageSource = BitmapFromUri(new Uri(imagepath));
                }
                thisproduct = product;
            }
            else
            {
                add = true;
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            Manager.frame.GoBack();
        }

        private void Title_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Title.Text == "Title")
                Title.Text = "";
            else if (Title.Text == "")
                Title.Text = "Title";
        }
        private void Count_GotFocus(object sender, RoutedEventArgs e)
        {
            if (CountPack.Text == "Count in pack")
                CountPack.Text = "";
        }
        private void Cost_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Cost.Text == "Cost")
                Cost.Text = "";
        }

        private void loadPicture_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Выберите изображение";
            openFileDialog.Filter = "Графические изображения|*.jpg;*.jpeg;*.png|" +
        "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
        "Portable Network Graphic (*.png)|*.png";
            if (openFileDialog.ShowDialog() == true)
            {
                imagePath = openFileDialog.FileName;
                Image.ImageSource = BitmapFromUri(new Uri(imagePath));
            }
        }

        private void SaveImage()
        {
            try
            {
                pathTo = "\\products\\" + thisproduct.ID + ".jpg";
                string path = pathTo.Replace("\\", "/");
                Regex reg = new Regex("/");
                path = reg.Replace(path, "../", 1);
                path = System.IO.Path.GetFullPath(path);
                path = path.Replace("\\bin", "");
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create((BitmapSource)Image.ImageSource));
                using (FileStream stream = new FileStream(path, FileMode.Create))encoder.Save(stream);
            }
            catch { }
        }
            
        public static ImageSource BitmapFromUri(Uri source)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = source;
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            bitmap.Freeze();
            return bitmap;
        }

        private void CountPack_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = ((TextBox)sender).Text;
            if (text == "" || text == "Count in pack" || text == "Cost")
                return;
            try
            {
                int.Parse(text[text.Length-1].ToString());
            }
            catch
            {
                ((TextBox)sender).Text = text.TrimEnd(text[text.Length - 1]);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string imagepath = System.IO.Path.GetFullPath("../Image/заглушка.jpg");
            imagepath = imagepath.Replace("\\bin", "");
            Image.ImageSource = BitmapFromUri(new Uri(imagepath));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            thisproduct.Title = Title.Text;
            thisproduct.Cost = decimal.Parse(Cost.Text);
            thisproduct.ProductionPersonCount = int.Parse(CountPack.Text);
            thisproduct.ProductTypeID = CurrentList.db.ProductType.ToList().Where(x => x.Title== Type.SelectedValue).FirstOrDefault().ID;
            if(pathTo != null)
            {
                SaveImage();
                thisproduct.Image = pathTo;
            }
            else
                thisproduct.Image = null;
            if (add)
            {
                CurrentList.db.Product.Add(thisproduct);
                CurrentList.db.SaveChangesAsync();
            }
            else
            {
                CurrentList.db.SaveChangesAsync();
            }
            MessageBox.Show("OK");
        }
    }
}
