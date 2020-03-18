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
    /// Interaction logic for PolygonWindowEdit.xaml
    /// </summary>
    public partial class PolygonWindowEdit : Window
    {
        private Polygon currentPolygon;
        public Polygon CurrentPolygon { get => currentPolygon; set => currentPolygon = value; }
       
        public PolygonWindowEdit(Polygon polygon)
        {
            InitializeComponent();
            currentPolygon = polygon;
            polygonThickness.Text = currentPolygon.StrokeThickness.ToString();

            Brush br = currentPolygon.Fill;
            polygonFillColor.SelectedColor = ConvertBrushToColor(br);
            br = currentPolygon.Stroke;
            polygonBorderColor.SelectedColor = ConvertBrushToColor(br);
        }

        private void EditEllipse(object sender, RoutedEventArgs e)
        {
            if (IsValidate())
            {
                var stringFillColor = polygonFillColor.SelectedColorText;
                currentPolygon.Fill = (SolidColorBrush)new BrushConverter().ConvertFromString(stringFillColor);
                var BorderColor = polygonBorderColor.SelectedColorText;
                currentPolygon.Stroke = (SolidColorBrush)new BrushConverter().ConvertFromString(BorderColor);
                currentPolygon.StrokeThickness = Double.Parse(polygonThickness.Text);

                this.Close();
            }
        }

        private bool IsValidate()
        {
            bool isValid = true;

            if (polygonThickness.Text.Trim().Equals(String.Empty) || polygonThickness.Text.Trim().Contains(" "))
            {
                labelThc.Content = "Must have a border thickness.";
                polygonThickness.BorderBrush = Brushes.Red;

                isValid = false;
            }
            else
            {
                labelThc.Content = String.Empty;
                polygonThickness.BorderBrush = Brushes.Gray;
            }
            if (polygonFillColor.SelectedColor == null)
            {
                labelFC.Content = "Choose fill color.";
                polygonFillColor.BorderBrush = Brushes.Red;

                isValid = false;
            }
            else
            {
                labelFC.Content = String.Empty;
                polygonFillColor.BorderBrush = Brushes.Gray;
            }
            if (polygonBorderColor.SelectedColor == null)
            {
                labelBC.Content = "Choose border color.";
                polygonBorderColor.BorderBrush = Brushes.Red;
            }
            else
            {
                labelBC.Content = String.Empty;
                polygonBorderColor.BorderBrush = Brushes.Gray;
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
