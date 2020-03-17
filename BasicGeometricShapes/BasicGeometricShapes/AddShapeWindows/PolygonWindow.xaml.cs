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

namespace BasicGeometricShapes.AddShapeWindows
{
    /// <summary>
    /// Interaction logic for PolygonWindow.xaml
    /// </summary>
    public partial class PolygonWindow : Window
    {
        private Canvas activeDrawTable;
        public Canvas ActiveDrawTable { get => activeDrawTable; set => activeDrawTable = value; }

        public PolygonWindow(Canvas canvas)
        {
            InitializeComponent();
            activeDrawTable = canvas;
        }

        private void DrawPolygon(object sender, RoutedEventArgs e)
        {
            if (IsValidate())
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
                foreach (Ellipse ellipseDot in MainWindow.tempPolyDots)//fejk pravljenje da dotovi nestanu... jer je referenca moze
                {
                    activeDrawTable.Children.Remove(ellipseDot);
                    ellipseDot.Fill = Brushes.Red;
                    ellipseDot.Stroke = Brushes.Red;
                }

                this.Close();
            }
        }

        private bool IsValidate()
        {
            bool isValid = true;

            if (polygonThickness.Text.Trim().Equals(String.Empty) || polygonThickness.Text.Trim().Contains(" "))
            {
                labelTh.Content = "Polygon must have a width.";
                polygonThickness.BorderBrush = Brushes.Red;

                isValid = false;
            }
            else
            {
                labelTh.Content = String.Empty;
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
            if (polygonBroderColor.SelectedColor == null)
            {
                labelBC.Content = "Choose border color.";
                polygonBroderColor.BorderBrush = Brushes.Red;
            }
            else
            {
                labelBC.Content = String.Empty;
                polygonBroderColor.BorderBrush = Brushes.Gray;
            }

            return isValid;
        }

        private void CloseDraw(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
