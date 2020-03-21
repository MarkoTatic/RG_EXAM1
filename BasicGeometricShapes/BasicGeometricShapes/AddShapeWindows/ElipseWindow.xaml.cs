using BasicGeometricShapes.UndoRedoCommand;
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
    /// Interaction logic for ElipseWindow.xaml
    /// </summary>
    public partial class ElipseWindow : Window
    {
        private Canvas activeDrawTable;
        private Point points;

        public Canvas ActiveDrawTable { get => activeDrawTable; set => activeDrawTable = value; }
        public Point Points { get => points; set => points = value; }

        public ElipseWindow(Point point, Canvas activeCanvas)
        {
           InitializeComponent();
           points = point;
           activeDrawTable = activeCanvas;
        }


        private void DrawEllipse(object sender, RoutedEventArgs e)
        {
            if (IsValidate())
            {
                Ellipse ellipse = new Ellipse();
                ellipse.Width = Double.Parse(elipseWidth.Text);
                ellipse.Height = Double.Parse(elipseHeight.Text);
                var stringFillColor = elipseFillColor.SelectedColorText;
                ellipse.Fill = (SolidColorBrush)new BrushConverter().ConvertFromString(stringFillColor);
                var BorderColor = ellipseBroderColor.SelectedColorText;
                ellipse.Stroke = (SolidColorBrush)new BrushConverter().ConvertFromString(BorderColor);
                ellipse.StrokeThickness = Double.Parse(elipseThickness.Text);
                Canvas.SetTop(ellipse, points.Y);
                Canvas.SetLeft(ellipse, points.X);

                activeDrawTable.Children.Add(ellipse);
                AddToUndoStack(ellipse);

                this.Close();
            }
        }

        private void AddToUndoStack(Ellipse ellipse)
        {
            var ellipses = new List<UIElement>();
            ellipses.Add(ellipse);
            if (CanvasCommand.UndoStack.Count > 0)
            {
                foreach (var item in CanvasCommand.UndoStack.Peek())//sve elemente koji su trenutno na steku dodaj u novu listu i onda pushaj tu listu na vrh steka
                {
                    ellipses.Add(item);
                }
            }
            CanvasCommand.RedoStack.Clear();//mora da se ocitisti, jer ako ostane onda ce pamtiti i nakon undo-a ako se napravi nova grana pamtice sa stare grane sta je bilo za redo
            CanvasCommand.UndoStack.Push(ellipses);
        }

        private void CloseDraw(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool IsValidate()
        {
            bool isValid = true;
            bool valid_WorH = true;

            if (elipseWidth.Text.Trim().Equals(String.Empty) || elipseWidth.Text.Trim().Contains(" "))
            {
                labelWElipse.Content = "Ellipse must have a width.";
                elipseWidth.BorderBrush = Brushes.Red;

                valid_WorH = false;
                isValid = false;
            }
            else
            {
                labelWElipse.Content = String.Empty;
                elipseWidth.BorderBrush = Brushes.Gray;
            }
            if (elipseHeight.Text.Trim().Equals(String.Empty) || elipseHeight.Text.Trim().Contains(" "))
            {
                labelHElipse.Content = "Ellipse must have a height.";
                elipseHeight.BorderBrush = Brushes.Red;

                valid_WorH = false;
                isValid = false;
            }
            else
            {
                labelHElipse.Content = String.Empty;
                elipseHeight.BorderBrush = Brushes.Gray;
            }
            if (elipseThickness.Text.Trim().Equals(String.Empty) || elipseThickness.Text.Trim().Contains(" "))
            {
                labelThicknessElipse.Content = "Must have a border thickness.";
                elipseThickness.BorderBrush = Brushes.Red;

                valid_WorH = false;
                isValid = false;
            }
            else
            {
                labelThicknessElipse.Content = String.Empty;
                elipseThickness.BorderBrush = Brushes.Gray;
            }
            if (elipseFillColor.SelectedColor == null)
            {
                labelFCElipse.Content = "Choose fill color.";
                elipseFillColor.BorderBrush = Brushes.Red;

                isValid = false;
            }
            else
            {
                labelFCElipse.Content = String.Empty;
                elipseFillColor.BorderBrush = Brushes.Gray;
            }
            if (ellipseBroderColor.SelectedColor == null)
            {
                labelBCElipse.Content = "Choose border color.";
                ellipseBroderColor.BorderBrush = Brushes.Red;
            }
            else
            {
                labelBCElipse.Content = String.Empty;
                ellipseBroderColor.BorderBrush = Brushes.Gray;
            }

            if (valid_WorH)
            {
                if (Int32.Parse(elipseThickness.Text.Trim()) > (Int32.Parse(elipseWidth.Text.Trim()) / 2) || Int32.Parse(elipseThickness.Text.Trim()) > (Int32.Parse(elipseHeight.Text.Trim()) / 2))
                {
                    labelThicknessElipse.Content = "Border thic. must be lower.";
                    elipseThickness.BorderBrush = Brushes.Red;

                    isValid = false;
                }
            }

            return isValid;
        }
        
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
