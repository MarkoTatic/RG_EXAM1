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
    /// Interaction logic for ElipseWindowEdit.xaml
    /// </summary>
    public partial class ElipseWindowEdit : Window
    {
        private Ellipse currentEllipse;//polje ne objekat... ide po referenci dakle...
        public Ellipse CurrentEllipse { get => currentEllipse; set => currentEllipse = value; }

        public ElipseWindowEdit(Ellipse ellipse)
        {
            InitializeComponent();
            currentEllipse = ellipse;
            elipseWidth.Text = currentEllipse.Width.ToString();
            elipseHeight.Text = currentEllipse.Height.ToString();
            elipseThickness.Text = currentEllipse.StrokeThickness.ToString();

            Brush br = currentEllipse.Fill;
            elipseFillColor.SelectedColor = ConvertBrushToColor(br);
            br = currentEllipse.Stroke;
            ellipseBroderColor.SelectedColor = ConvertBrushToColor(br);
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

        private void EditEllipse(object sender, RoutedEventArgs e)
        {
            currentEllipse.Width = Double.Parse(elipseWidth.Text);
            currentEllipse.Height = Double.Parse(elipseHeight.Text);
            var stringFillColor = elipseFillColor.SelectedColorText;
            currentEllipse.Fill = (SolidColorBrush)new BrushConverter().ConvertFromString(stringFillColor);
            var BorderColor = ellipseBroderColor.SelectedColorText;
            currentEllipse.Stroke = (SolidColorBrush)new BrushConverter().ConvertFromString(BorderColor);
            currentEllipse.StrokeThickness = Double.Parse(elipseThickness.Text);

            this.Close();
        }

        private void CloseEdit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
