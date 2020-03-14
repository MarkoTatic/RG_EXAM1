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
        public static Canvas activeDrawTable;
        public static Image currentImage;
        public static double getLeft;
        public static double getTop;
        public ImageWindowEdit(Canvas activeCanvas, Image image)
        {
            InitializeComponent();
            activeDrawTable = activeCanvas;
            currentImage = image;
            currentImage.Stretch = image.Stretch;
            currentImage.Height = image.Height;
            currentImage.Width = image.Width;
            ImageWindow.imgSource = image.Source.ToString();
            getLeft = Canvas.GetLeft(currentImage);
            getTop = Canvas.GetTop(currentImage);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Open Image";
            dlg.Filter = "All Files|*.*";
            dlg.ShowDialog();
            ImageWindow.imgSource = dlg.FileName.ToString();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            activeDrawTable.Children.Remove(currentImage);
            Image image = new Image();
            image.Source = new ImageSourceConverter().ConvertFromString(ImageWindow.imgSource) as ImageSource;
            image.Height = currentImage.Height;
            image.Width = currentImage.Width;
            image.Stretch = currentImage.Stretch;
            Canvas.SetLeft(image, getLeft);
            Canvas.SetTop(image, getTop);
            activeDrawTable.Children.Add(image);

            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
