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
        Masks Masks;
        bool add = false;
        public AddEditProducts(Product product, Masks masks)
        {
            InitializeComponent();
            Masks = masks;
            if (product != null)
            {
                Title.Text = product.Title;
                Cost.Text = product.Cost.ToString();
                CountPack.Text = product.ProductionPersonCount.ToString();
                int i = 0;
                foreach (var item in CurrentList.db.ProductType.ToList())
                {
                    Type.Items.Add(item.Title);
                    if (item.ID == product.ProductTypeID)
                        Type.SelectedIndex = i;
                    i++;
                }
                if (product.Image != null | product.Image == "")
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
                delete.Visibility = Visibility.Hidden;
                add = true;
                foreach (var item in CurrentList.db.ProductType.ToList())
                {
                    Type.Items.Add(item.Title);
                }
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            Masks.LViewTours.ItemsSource = CurrentList.products;
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
            else if (CountPack.Text == "")
                CountPack.Text = "Count in pack";
        }
        private void Cost_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Cost.Text == "Cost")
                Cost.Text = "";
            else if (Cost.Text == "")
                Cost.Text = "Cost";
        }
        //Вспомогательный метод для открытие диалогового окна выбора картинки
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
            SaveImage();
        }
        //Метод сохранения картинки
        private void SaveImage()
        {
            try
            {
                if (thisproduct != null)
                    pathTo = "\\products\\" + thisproduct.ID + ".jpg";
                else
                    pathTo = "\\products\\" + CurrentList.db.Product.ToList().Count + 1 + ".jpg";
                string path = pathTo.Replace("\\", "/");
                Regex reg = new Regex("/");
                path = reg.Replace(path, "../", 1);
                path = System.IO.Path.GetFullPath(path);
                path = path.Replace("\\bin", "");
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create((BitmapSource)Image.ImageSource));
                using (FileStream stream = new FileStream(path, FileMode.Create)) encoder.Save(stream);
            }
            catch { }
        }
        //Вспомогательный метод конвертер
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
                int.Parse(text[text.Length - 1].ToString());
            }
            catch
            {
                ((TextBox)sender).Text = text.TrimEnd(text[text.Length - 1]);
            }
        }
        //Метод удаления картинки и подстановки картинки-заглушки
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string imagepath = System.IO.Path.GetFullPath("../Image/заглушка.jpg");
            imagepath = imagepath.Replace("\\bin", "");
            Image.ImageSource = BitmapFromUri(new Uri(imagepath));
        }
        //Функция добавления/изменения продукта
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Проверка того, какое действие выбрано: создание или редактирование
            if (add)
                thisproduct = new Product();

            //Если редактирование вносятся данные данные в экземпляр класса Product
            thisproduct.Title = Title.Text;
            thisproduct.Cost = decimal.Parse(Cost.Text);
            thisproduct.ProductionPersonCount = int.Parse(CountPack.Text);
            thisproduct.ProductTypeID = CurrentList.db.ProductType.ToList().Where(x => x.Title == Type.SelectedValue).FirstOrDefault().ID;

            if (pathTo != null)
            {
                thisproduct.Image = pathTo;
            }
            else
                thisproduct.Image = null;

            //Добавление продукта
            if (add)
            {
                CurrentList.db.Product.Add(thisproduct);
                CurrentList.db.SaveChanges();
            }
            else //Редактирование продукта
            {
                CurrentList.db.SaveChanges();
            }
            CurrentList.products = CurrentList.db.Product.ToList();
            MessageBox.Show("OK");
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            //Вывод диалогового окна для подтверждения действия от пользователя
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                //Удаление продукта из БД
                CurrentList.db.Product.Remove(thisproduct);
                CurrentList.db.SaveChanges();

                //Обновление списка продуктов
                CurrentList.products = CurrentList.db.Product.ToList();
                Masks.LViewTours.ItemsSource = CurrentList.products;
                Manager.frame.GoBack();
            }
        }

    }
}
