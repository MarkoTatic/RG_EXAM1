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
        private Rectangle currentRectangle;
        public Rectangle CurrentRectangle { get => currentRectangle; set => currentRectangle = value; }

        public RectangleWindowEdit(Rectangle rectangle)
        {
            InitializeComponent();
            currentRectangle = rectangle;
            rectangleWidth.Text = currentRectangle.Width.ToString();
            rectangleHeight.Text = currentRectangle.Height.ToString();
            rectangleThickness.Text = currentRectangle.StrokeThickness.ToString();

            Brush br = currentRectangle.Fill;
            rectangleFillColor.SelectedColor = ConvertBrushToColor(br);
            br = currentRectangle.Stroke;
            rectangleBroderColor.SelectedColor = ConvertBrushToColor(br);
        }

       
        private void EditRectangle(object sender, RoutedEventArgs e)
        {
            currentRectangle.Width = Double.Parse(rectangleWidth.Text);
            currentRectangle.Height = Double.Parse(rectangleHeight.Text);
            var stringFillColor = rectangleFillColor.SelectedColorText;
            currentRectangle.Fill = (SolidColorBrush)new BrushConverter().ConvertFromString(stringFillColor);
            var BorderColor = rectangleBroderColor.SelectedColorText;
            currentRectangle.Stroke = (SolidColorBrush)new BrushConverter().ConvertFromString(BorderColor);
            currentRectangle.StrokeThickness = Double.Parse(rectangleThickness.Text);

            this.Close();
        }

        private void CloseEdit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private Color ConvertBrushToColor(Brush br)
        {
            byte a = ((Color)br.GetValue(SolidColorBrush.ColorProperty)).A;
            byte g = ((Color)br.GetValue(SolidColorBrush.ColorProperty)).G;
            byte r = ((Color)br.GetValue(SolidColorBrush.ColorProperty)).R;
            byte b = ((Color)br.GetValue(SolidColorBrush.ColorProperty)).B;
            Color color = new Color();
            color.A = a;
            color.R = r;
            color.G = g;
            color.B = b;

            return color;
        }
    }
}
