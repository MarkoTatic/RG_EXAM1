using BasicGeometricShapes.AddShapeWindows;
using Microsoft.Win32;
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
using System.Windows.Shapes;

namespace BasicGeometricShapes.EditShapeWindows
{
    /// <summary>
    /// Interaction logic for ImageWindowEdit.xaml
    /// </summary>
    public partial class ImageWindowEdit : Window
    {
        private Image currentImage;
        public Image CurrentImage { get => currentImage; set => currentImage = value; }

        public ImageWindowEdit(Image image)
        {
            InitializeComponent();
            currentImage = image;
            currentImage.Stretch = image.Stretch;
            currentImage.Height = image.Height;
            currentImage.Width = image.Width;
            ImageWindow.imgSource = image.Source.ToString();
        }

        private void ChooseDialog(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Open Image";
            dialog.Filter = "All Files|*.*";
            dialog.ShowDialog();
            ImageWindow.imgSource = dialog.FileName.ToString();
        }

        private void EditImage(object sender, RoutedEventArgs e)
        {
            currentImage.Source = new ImageSourceConverter().ConvertFromString(ImageWindow.imgSource) as ImageSource;
            currentImage.Height = currentImage.Height;
            currentImage.Width = currentImage.Width;
            currentImage.Stretch = currentImage.Stretch;

            this.Close();
        }

        private void CloseImage(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
