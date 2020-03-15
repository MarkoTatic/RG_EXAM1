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
    /// Interaction logic for ElipseWindow.xaml
    /// </summary>
    public partial class ElipseWindow : Window
    {
        private Canvas activeDrawTable;
        private Point points;

        public Canvas ActiveDrawTable { get => activeDrawTable; set => activeDrawTable = value; }
        public Point Points { get => points; set => points = value; }

        public ElipseWindow(Point point, Canvas activeCanvas)
        {
           InitializeComponent();
           points = point;
           activeDrawTable = activeCanvas;
        }


        private void DrawEllipse(object sender, RoutedEventArgs e)
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
            MainWindow.canvasShapes.Add(ellipse);

            this.Close();
        }

        private void CloseDraw(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Validate()
        {
        }
    }
}
