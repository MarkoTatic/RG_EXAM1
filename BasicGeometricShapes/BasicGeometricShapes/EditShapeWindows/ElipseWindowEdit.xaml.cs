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
        public static Canvas activeDrawTable;
        public static Ellipse currentEllipse;
        public static double getLeft;
        public static double getTop;


        public ElipseWindowEdit(Canvas activeCanvas, Ellipse ellipse)
        {
            InitializeComponent();
            activeDrawTable = activeCanvas;
            currentEllipse = ellipse;
            elipseWidth.Text = currentEllipse.Width.ToString();
            elipseHeight.Text = currentEllipse.Height.ToString();
            elipseThickness.Text = currentEllipse.StrokeThickness.ToString();
            getLeft = Canvas.GetLeft(currentEllipse);
            getTop = Canvas.GetTop(currentEllipse);
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            activeDrawTable.Children.Remove(currentEllipse);
            Ellipse ellipse = new Ellipse();
            ellipse.Width = Double.Parse(elipseWidth.Text);
            ellipse.Height = Double.Parse(elipseHeight.Text);
            var stringFillColor = elipseFillColor.SelectedColorText;
            ellipse.Fill = (SolidColorBrush)new BrushConverter().ConvertFromString(stringFillColor);
            var BorderColor = ellipseBroderColor.SelectedColorText;
            ellipse.Stroke = (SolidColorBrush)new BrushConverter().ConvertFromString(BorderColor);
            ellipse.StrokeThickness = Double.Parse(elipseThickness.Text);
            Canvas.SetLeft(ellipse, getLeft);
            Canvas.SetTop(ellipse, getTop);
            activeDrawTable.Children.Add(ellipse);


            MainWindow.canvasShapes.Add(ellipse);

            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
