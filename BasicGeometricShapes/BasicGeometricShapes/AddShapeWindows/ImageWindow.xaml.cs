using BasicGeometricShapes.UndoRedoCommand;
using Microsoft.Win32;
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
    /// Interaction logic for ImageWindow.xaml
    /// </summary>
    public partial class ImageWindow : Window
    {
        public static string imgSource;
        private Canvas activeDrawTable;
        private Point points;
        public Canvas ActiveDrawTable { get => activeDrawTable; set => activeDrawTable = value; }
        public Point Points { get => points; set => points = value; }

        public ImageWindow(Point p, Canvas canvas)
        {
            InitializeComponent();
            points = p;
            activeDrawTable = canvas;
            imgSource = "";
        }

        private void ChooseDialog(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Open Image";
            dialog.Filter = "All Files|*.*";
            dialog.ShowDialog();
            ImageWindow.imgSource = dialog.FileName.ToString();
        }

        private void DrawImage(object sender, RoutedEventArgs e)
        {
            if (IsValidate())
            {
                Image image = new Image();
                image.Stretch = Stretch.Fill;//da bi lepo pokrio sve velicine slike
                image.Width = Double.Parse(imageWidth.Text);
                image.Height = Double.Parse(imageHeight.Text);
                image.Source = new ImageSourceConverter().ConvertFromString(ImageWindow.imgSource) as ImageSource;
                Canvas.SetTop(image, points.Y);
                Canvas.SetLeft(image, points.X);
                activeDrawTable.Children.Add(image);

                AddToUndoStack(image);

                this.Close();
            }
        }

        private void AddToUndoStack(Image image)
        {
            var images = new List<UIElement>();
            images.Add(image);
            if (CanvasCommand.UndoStack.Count > 0)
            {
                foreach (var item in CanvasCommand.UndoStack.Peek())//sve elemente koji su trenutno na steku dodaj u novu listu i onda pushaj tu listu na vrh steka
                {
                    images.Add(item);
                }
            }

            CanvasCommand.RedoStack.Clear();//mora da se ocitisti, jer ako ostane onda ce pamtiti i nakon undo-a ako se napravi nova grana pamtice sa stare grane sta je bilo za redo
            CanvasCommand.UndoStack.Push(images);
        }

        private bool IsValidate()
        {
            bool isValid = true;

            if (imageWidth.Text.Trim().Equals(String.Empty) || imageWidth.Text.Trim().Contains(" "))
            {
                labelW.Content = "Image must have a width.";
                imageWidth.BorderBrush = Brushes.Red;

                isValid = false;
            }
            else
            {
                labelW.Content = String.Empty;
                imageWidth.BorderBrush = Brushes.Gray;
            }
            if (imageHeight.Text.Trim().Equals(String.Empty) || imageHeight.Text.Trim().Contains(" "))
            {
                labelH.Content = "Image must have a height.";
                imageHeight.BorderBrush = Brushes.Red;

                isValid = false;
            }
            else
            {
                labelH.Content = String.Empty;
                imageHeight.BorderBrush = Brushes.Gray;
            }
            if (imgSource.Trim().Equals(String.Empty))
            {
                labelChoose.Content = "You must choose image.";
                chooseButton.BorderBrush = Brushes.Red;

                isValid = false;
            }
            else
            {
                labelChoose.Content = "";
                chooseButton.BorderBrush = Brushes.Gray;
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
