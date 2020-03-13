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

namespace BasicGeometricShapes.ExtandWindows
{
    /// <summary>
    /// Interaction logic for ElipseWindow.xaml
    /// </summary>
    public partial class ElipseWindow : Window
    {
        public static Canvas activeDrawTable;
        public static Point points;

        public ElipseWindow(Point point, Canvas activeCanvas)
        {
            InitializeComponent();
            points = point;
            activeDrawTable = activeCanvas;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Width = Double.Parse(elipseWidth.Text);
            ellipse.Height = Double.Parse(elipseHeight.Text);
            var stringFillColor = elipseFillColor.SelectedColorText;
            ellipse.Fill = (SolidColorBrush)new BrushConverter().ConvertFromString(stringFillColor);
            var BorderColor = ellipseBroderColor.SelectedColorText;
            ellipse.Stroke = (SolidColorBrush)new BrushConverter().ConvertFromString(BorderColor);
            ellipse.StrokeThickness = Double.Parse(elipseThickness.Text);
            Canvas.SetTop(ellipse, points.Y);
            Canvas.SetLeft(ellipse, points.X);
            activeDrawTable.Children.Add(ellipse);

            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
