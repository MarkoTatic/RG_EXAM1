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

namespace BasicGeometricShapes.AddShapeWindows
{
    /// <summary>
    /// Interaction logic for ImageWindow.xaml
    /// </summary>
    public partial class ImageWindow : Window
    {
        public static Canvas activeDrawTable;
        public static Point points;
        private static string imgSource;
        public ImageWindow(Point p, Canvas canvas)
        {
            InitializeComponent();
            points = p;
            activeDrawTable = canvas;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Open Image";
            dlg.Filter = "All Files|*.*";
            //PictureBox1.Image = Image.FromFile(dlg.Filename);
            dlg.ShowDialog();
            ImageWindow.imgSource = dlg.FileName.ToString();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Image image = new Image();
            image.Width = Double.Parse(imageWidth.Text);
            image.Height = Double.Parse(imageHeight.Text);
            image.Source = new ImageSourceConverter().ConvertFromString(ImageWindow.imgSource) as ImageSource;
            Canvas.SetTop(image, points.Y);
            Canvas.SetLeft(image, points.X);
            activeDrawTable.Children.Add(image);

            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
