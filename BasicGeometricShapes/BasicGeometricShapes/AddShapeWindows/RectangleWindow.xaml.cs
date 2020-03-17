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
            if (IsValidate())
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
        }

        private bool IsValidate()
        {
            bool isValid = true;
            bool valid_WorH = true;

            if (rectangleWidth.Text.Trim().Equals(String.Empty) || rectangleWidth.Text.Trim().Contains(" "))
            {
                labelW.Content = "Rect. must have a width.";
                rectangleWidth.BorderBrush = Brushes.Red;

                valid_WorH = false;
                isValid = false;
            }
            else
            {
                labelW.Content = String.Empty;
                rectangleWidth.BorderBrush = Brushes.Gray;
            }
            if (rectangleHeight.Text.Trim().Equals(String.Empty) || rectangleHeight.Text.Trim().Contains(" "))
            {
                labelH.Content = "Rect. must have a height.";
                rectangleHeight.BorderBrush = Brushes.Red;

                valid_WorH = false;
                isValid = false;
            }
            else
            {
                labelH.Content = String.Empty;
                rectangleHeight.BorderBrush = Brushes.Gray;
            }
            if (rectangleThickness.Text.Trim().Equals(String.Empty) || rectangleThickness.Text.Trim().Contains(" "))
            {
                labelThk.Content = "Must have a border thickness.";
                rectangleThickness.BorderBrush = Brushes.Red;

                valid_WorH = false;
                isValid = false;
            }
            else
            {
                labelThk.Content = String.Empty;
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

            if (valid_WorH)
            {
                if (Int32.Parse(rectangleThickness.Text.Trim()) > Int32.Parse(rectangleWidth.Text.Trim()) || Int32.Parse(rectangleThickness.Text.Trim()) > Int32.Parse(rectangleHeight.Text.Trim()))
                {
                    labelThk.Content = "Border thickness must be lower.";
                    rectangleThickness.BorderBrush = Brushes.Red;

                    isValid = false;
                }
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
