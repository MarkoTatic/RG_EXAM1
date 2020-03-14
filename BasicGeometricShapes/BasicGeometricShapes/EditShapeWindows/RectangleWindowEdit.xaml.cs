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
    /// Interaction logic for RectangleWindowEdit.xaml
    /// </summary>
    public partial class RectangleWindowEdit : Window
    {
        public static Canvas activeDrawTable;
        public static Rectangle currentRectangle;
        public static double getLeft;
        public static double getTop;

        public RectangleWindowEdit(Canvas activeCanvas, Rectangle rectangle)
        {
            InitializeComponent();
            activeDrawTable = activeCanvas;
            currentRectangle = rectangle;
            rectangleWidth.Text = currentRectangle.Width.ToString();
            rectangleHeight.Text = currentRectangle.Height.ToString();
            rectangleThickness.Text = currentRectangle.StrokeThickness.ToString();

            Brush br = rectangle.Fill;
            rectangleFillColor.SelectedColor = ConvertBrushToColor(br);
            br = rectangle.Stroke;
            rectangleBroderColor.SelectedColor = ConvertBrushToColor(br);
            getLeft = Canvas.GetLeft(currentRectangle);
            getTop = Canvas.GetTop(currentRectangle);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            activeDrawTable.Children.Remove(currentRectangle);
            Rectangle rectangle = new Rectangle();
            rectangle.Width = Double.Parse(rectangleWidth.Text);
            rectangle.Height = Double.Parse(rectangleHeight.Text);
            var stringFillColor = rectangleFillColor.SelectedColorText;
            rectangle.Fill = (SolidColorBrush)new BrushConverter().ConvertFromString(stringFillColor);
            var BorderColor = rectangleBroderColor.SelectedColorText;
            rectangle.Stroke = (SolidColorBrush)new BrushConverter().ConvertFromString(BorderColor);
            rectangle.StrokeThickness = Double.Parse(rectangleThickness.Text);
            Canvas.SetLeft(rectangle, getLeft);
            Canvas.SetTop(rectangle, getTop);
            activeDrawTable.Children.Add(rectangle);

            MainWindow.canvasShapes.Add(rectangle);

            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private Color ConvertBrushToColor(Brush br)
        {
            byte a = ((Color)br.GetValue(SolidColorBrush.ColorProperty)).A;
            byte g = ((Color)br.GetValue(SolidColorBrush.ColorProperty)).G;
            byte r = ((Color)br.GetValue(SolidColorBrush.ColorProperty)).R;
            byte b = ((Color)br.GetValue(SolidColorBrush.ColorProperty)).B;
            Color cl = new Color();
            cl.A = a;
            cl.R = r;
            cl.G = g;
            cl.B = b;

            return cl;
        }
    }
}
