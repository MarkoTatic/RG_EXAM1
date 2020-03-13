using BasicGeometricShapes.ExtandWindows;
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

namespace BasicGeometricShapes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string selectedMenuItem = "";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ActiveCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Elipse.IsChecked)
            {
                selectedMenuItem = "Elipse";
                Point p = Mouse.GetPosition(ActiveCanvas);
                ElipseWindow elipseWindow = new ElipseWindow(p, ActiveCanvas);
                elipseWindow.ShowDialog();
            }
            else if (Rectangle.IsChecked)
            {
                selectedMenuItem = "Rectangle";
            }
            else if (Polygon.IsChecked)
            {
                selectedMenuItem = "Polygon";
            }
            else if (Image.IsChecked)
            {
                selectedMenuItem = "Image";
            }          
        }

        private void Elipse_Click(object sender, RoutedEventArgs e)
        {
            Rectangle.IsChecked = false;
            Polygon.IsChecked = false;
            Image.IsChecked = false;
        }

        private void Rectangle_Click(object sender, RoutedEventArgs e)
        {
            Elipse.IsChecked = false;
            Polygon.IsChecked = false;
            Image.IsChecked = false;
        }

        private void Polygon_Click(object sender, RoutedEventArgs e)
        {
            Rectangle.IsChecked = false;
            Elipse.IsChecked = false;
            Image.IsChecked = false;
        }

        private void Image_Click(object sender, RoutedEventArgs e)
        {
            Rectangle.IsChecked = false;
            Polygon.IsChecked = false;
            Elipse.IsChecked = false;
        }
    }
}
