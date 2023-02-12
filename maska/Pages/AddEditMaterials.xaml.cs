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
    /// Логика взаимодействия для AddEditMaterials.xaml
    /// </summary>
    public partial class AddEditMaterials : Page
    {
        string imagePath;
        string pathTo;
        Material thisMaterial;
        Materials Materials;
        bool add = false;
        public AddEditMaterials(Material material, Materials materials)
        {
            InitializeComponent();
            Materials = materials;
            if(material != null )
            {
                Title.Text = material.Title;
                Cost.Text = material.Cost.ToString();
                CountPack.Text = material.CountInPack.ToString();
                int i = 0;
                foreach (var item in CurrentList.db.MaterialType.ToList())
                {
                    Type.Items.Add(item.Title);
                    if(item.ID == material.MaterialTypeID)
                        Type.SelectedIndex= i;
                    i++;
                }
                if(material.Image != null | material.Image == "")
                {
                string imagepath = material.Image;
                imagepath = imagepath.Replace("\\", "/");
                Regex reg = new Regex("/");
                imagepath = reg.Replace(imagepath, "../", 1);
                imagepath = System.IO.Path.GetFullPath(imagepath);
                imagepath = imagepath.Replace("\\bin", "");
                Image.ImageSource = BitmapFromUri(new Uri(imagepath));
                }
                thisMaterial = material;
            }
            else
            {
                delete.Visibility = Visibility.Hidden;
                add = true;
                foreach (var item in CurrentList.db.MaterialType.ToList())
                {
                    Type.Items.Add(item.Title);
                }
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            Materials.LViewTours.ItemsSource = CurrentList.materials;
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

        private void SaveImage()
        {
            try
            {
                if(thisMaterial != null)
                    pathTo = "\\materials\\" + thisMaterial.ID + ".jpg";
                else
                    pathTo = "\\products\\" + CurrentList.db.Material.ToList().Count + 1 + ".jpg";
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
            if (add)
                thisMaterial = new Material();
            thisMaterial.Title = Title.Text;
            thisMaterial.Cost = decimal.Parse(Cost.Text);
            thisMaterial.CountInPack = int.Parse(CountPack.Text);
            thisMaterial.MaterialTypeID = CurrentList.db.MaterialType.ToList().Where(x => x.Title== Type.SelectedValue).FirstOrDefault().ID;
            if (pathTo != null)
            {
                thisMaterial.Image = pathTo;
            }
            else
                thisMaterial.Image = null;
            if (add)
            {
                CurrentList.db.Material.Add(thisMaterial);
                CurrentList.db.SaveChanges();
            }
            else
            {
                CurrentList.db.SaveChanges();
            }
            CurrentList.materials = CurrentList.db.Material.ToList();
            MessageBox.Show("OK");
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                CurrentList.db.Material.Remove(thisMaterial);
                Materials.LViewTours.ItemsSource = CurrentList.materials;
                Manager.frame.GoBack();
            }
        }
    }
}
