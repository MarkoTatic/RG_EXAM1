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
    /// Interaction logic for PolygonWindow.xaml
    /// </summary>
    public partial class PolygonWindow : Window
    {
        public static Canvas activeDrawTable;
        public PolygonWindow(Canvas canvas)
        {
            InitializeComponent();
            activeDrawTable = canvas;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Polygon polygon = new Polygon();
            foreach (Point point in MainWindow.polygonPoints)
            {
                polygon.Points.Add(point);
            }

            polygon.StrokeThickness = Double.Parse(polygonThickness.Text);
            var stringFillColor = polygonFillColor.SelectedColorText;
            polygon.Fill = (SolidColorBrush)new BrushConverter().ConvertFromString(stringFillColor);
            var BorderColor = polygonBroderColor.SelectedColorText;
            polygon.Stroke = (SolidColorBrush)new BrushConverter().ConvertFromString(BorderColor);
            polygon.FillRule = FillRule.Nonzero;//da preklapa lepo
            
            activeDrawTable.Children.Add(polygon);

            MainWindow.canvasShapes.Add(polygon);
            MainWindow.polygonPoints.Clear();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
