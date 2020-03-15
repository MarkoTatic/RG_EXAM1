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
    /// Interaction logic for RectangleWindow.xaml
    /// </summary>
    public partial class RectangleWindow : Window
    {
        private Canvas activeDrawTable;
        private Point points;
        public Canvas ActiveDrawTable { get => activeDrawTable; set => activeDrawTable = value; }
        public Point Points { get => points; set => points = value; }
        public RectangleWindow(Point point, Canvas activeCanvas)
        {
            InitializeComponent();
            points = point;
            activeDrawTable = activeCanvas;
        }

        private void DrawRectangle(object sender, RoutedEventArgs e)
        {
            Rectangle rectangle = new Rectangle();
            rectangle.Width = Double.Parse(rectangleWidth.Text);
            rectangle.Height = Double.Parse(rectangleHeight.Text);
            var stringFillColor = rectangleFillColor.SelectedColorText;
            rectangle.Fill = (SolidColorBrush)new BrushConverter().ConvertFromString(stringFillColor);
            var borderColor = rectangleBroderColor.SelectedColorText;
            rectangle.Stroke = (SolidColorBrush)new BrushConverter().ConvertFromString(borderColor);
            rectangle.StrokeThickness = Double.Parse(rectangleThickness.Text);
            Canvas.SetTop(rectangle, points.Y);
            Canvas.SetLeft(rectangle, points.X);

            activeDrawTable.Children.Add(rectangle);
            MainWindow.canvasShapes.Add(rectangle);

            this.Close();
        }

        private void CloseDraw(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
