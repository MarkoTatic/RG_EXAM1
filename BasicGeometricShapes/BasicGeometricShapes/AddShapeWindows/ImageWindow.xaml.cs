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
        public static string imgSource;
        private Canvas activeDrawTable;
        private Point points;
        public Canvas ActiveDrawTable { get => activeDrawTable; set => activeDrawTable = value; }
        public Point Points { get => points; set => points = value; }

        public ImageWindow(Point p, Canvas canvas)
        {
            InitializeComponent();
            points = p;
            activeDrawTable = canvas;
        }

        private void ChooseDialog(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Open Image";
            dialog.Filter = "All Files|*.*";
            dialog.ShowDialog();
            ImageWindow.imgSource = dialog.FileName.ToString();
        }

        private void DrawImage(object sender, RoutedEventArgs e)
        {
            Image image = new Image();
            image.Stretch = Stretch.Fill;//da bi lepo pokrio sve velicine slike
            image.Width = Double.Parse(imageWidth.Text);
            image.Height = Double.Parse(imageHeight.Text);
            image.Source = new ImageSourceConverter().ConvertFromString(ImageWindow.imgSource) as ImageSource;
            Canvas.SetTop(image, points.Y);
            Canvas.SetLeft(image, points.X);
            activeDrawTable.Children.Add(image);

            this.Close();
        }

        private void CloseDraw(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
