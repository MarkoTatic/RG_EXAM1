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
    /// Interaction logic for ElipseWindowEdit.xaml
    /// </summary>
    public partial class ElipseWindowEdit : Window
    {
        private Ellipse currentEllipse;
        public Ellipse CurrentEllipse { get => currentEllipse; set => currentEllipse = value; }

        public ElipseWindowEdit(Ellipse ellipse)
        {
            InitializeComponent();
            currentEllipse = ellipse;
            elipseThickness.Text = currentEllipse.StrokeThickness.ToString();

            Brush br = currentEllipse.Fill;
            elipseFillColor.SelectedColor = ConvertBrushToColor(br);
            br = currentEllipse.Stroke;
            ellipseBroderColor.SelectedColor = ConvertBrushToColor(br);
        }

        private void EditEllipse(object sender, RoutedEventArgs e)
        {
            if (IsValidate())
            {
                var stringFillColor = elipseFillColor.SelectedColorText;
                currentEllipse.Fill = (SolidColorBrush)new BrushConverter().ConvertFromString(stringFillColor);
                var BorderColor = ellipseBroderColor.SelectedColorText;
                currentEllipse.Stroke = (SolidColorBrush)new BrushConverter().ConvertFromString(BorderColor);
                currentEllipse.StrokeThickness = Double.Parse(elipseThickness.Text);

                this.Close();
            }
        }

        private bool IsValidate()
        {
            bool isValid = true;
            bool validWorh = true;

            if (elipseThickness.Text.Trim().Equals(String.Empty) || elipseThickness.Text.Trim().Contains(" "))
            {
                labelThc.Content = "Must have a border thickness.";
                elipseThickness.BorderBrush = Brushes.Red;

                validWorh = false;
                isValid = false;
            }
            else
            {
                labelThc.Content = String.Empty;
                elipseThickness.BorderBrush = Brushes.Gray;
            }
            if (elipseFillColor.SelectedColor == null)
            {
                labelFC.Content = "Choose fill color.";
                elipseFillColor.BorderBrush = Brushes.Red;

                isValid = false;
            }
            else
            {
                labelFC.Content = String.Empty;
                elipseFillColor.BorderBrush = Brushes.Gray;
            }
            if (ellipseBroderColor.SelectedColor == null)
            {
                labelBC.Content = "Choose border color.";
                ellipseBroderColor.BorderBrush = Brushes.Red;
            }
            else
            {
                labelBC.Content = String.Empty;
                ellipseBroderColor.BorderBrush = Brushes.Gray;
            }

            if (validWorh)
            {
                if (Int32.Parse(elipseThickness.Text.Trim()) > (Int32.Parse(currentEllipse.Width.ToString()) / 2) || Int32.Parse(elipseThickness.Text.Trim()) > (Int32.Parse(currentEllipse.Height.ToString()) / 2))
                {
                    labelThc.Content = "Invalid border thicknes.";
                    elipseThickness.BorderBrush = Brushes.Red;

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
