using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            rectangleThickness.Text = currentRectangle.StrokeThickness.ToString();

            Brush br = currentRectangle.Fill;
            rectangleFillColor.SelectedColor = ConvertBrushToColor(br);
            br = currentRectangle.Stroke;
            rectangleBroderColor.SelectedColor = ConvertBrushToColor(br);
        }

       
        private void EditRectangle(object sender, RoutedEventArgs e)
        {
            if (IsValidate())
            {
                var stringFillColor = rectangleFillColor.SelectedColorText;
                currentRectangle.Fill = (SolidColorBrush)new BrushConverter().ConvertFromString(stringFillColor);
                var BorderColor = rectangleBroderColor.SelectedColorText;
                currentRectangle.Stroke = (SolidColorBrush)new BrushConverter().ConvertFromString(BorderColor);
                currentRectangle.StrokeThickness = Double.Parse(rectangleThickness.Text);

                this.Close();
            }
        }

        private bool IsValidate()
        {
            bool isValid = true;
            bool validWorh = true;

            if (rectangleThickness.Text.Trim().Equals(String.Empty) || rectangleThickness.Text.Trim().Contains(" "))
            {
                labelThc.Content = "Must have a border thickness.";
                rectangleThickness.BorderBrush = Brushes.Red;

                isValid = false;
            }
            else
            {
                labelThc.Content = String.Empty;
                rectangleThickness.BorderBrush = Brushes.Gray;
            }
            if (rectangleFillColor.SelectedColor == null)
            {
                labelFC.Content = "Choose fill color.";
                rectangleFillColor.BorderBrush = Brushes.Red;

                isValid = false;
            }
            else
            {
                labelFC.Content = String.Empty;
                rectangleFillColor.BorderBrush = Brushes.Gray;
            }
            if (rectangleBroderColor.SelectedColor == null)
            {
                labelBC.Content = "Choose border color.";
                rectangleBroderColor.BorderBrush = Brushes.Red;
            }
            else
            {
                labelBC.Content = String.Empty;
                rectangleBroderColor.BorderBrush = Brushes.Gray;
            }

            if (validWorh)
            {
                if (Int32.Parse(rectangleThickness.Text.Trim()) > (Int32.Parse(currentRectangle.Width.ToString()) / 2) || Int32.Parse(rectangleThickness.Text.Trim()) > (Int32.Parse(currentRectangle.Height.ToString()) / 2))
                {
                    labelThc.Content = "Border thic. must be lower.";
                    rectangleThickness.BorderBrush = Brushes.Red;

                    isValid = false;
                }
            }

            return isValid;
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

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
