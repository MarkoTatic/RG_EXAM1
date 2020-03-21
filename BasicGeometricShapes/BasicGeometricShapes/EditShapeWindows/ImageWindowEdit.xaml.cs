using BasicGeometricShapes.AddShapeWindows;
using Microsoft.Win32;
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
    /// Interaction logic for ImageWindowEdit.xaml
    /// </summary>
    public partial class ImageWindowEdit : Window
    {
        private Image currentImage;
        public Image CurrentImage { get => currentImage; set => currentImage = value; }

        public ImageWindowEdit(Image image)
        {
            InitializeComponent();
            currentImage = image;
            currentImage.Stretch = image.Stretch;
            currentImage.Height = image.Height;
            currentImage.Width = image.Width;
            ImageWindow.imgSource.Source = image.Source;
        }

        private void EditImage(object sender, RoutedEventArgs e)
        {
            if (IsValidate())
            {
                currentImage.Source = ImageWindow.imgSource.Source;
                currentImage.Height = currentImage.Height;
                currentImage.Width = currentImage.Width;
                currentImage.Stretch = currentImage.Stretch;

                this.Close();
            }
        }

        private bool IsValidate()
        {
            bool isValid = true;

            if (ImageWindow.imgSource.Source == null)
            {
                labelchoose.Content = "You must choose image.";
                chooseButton.BorderBrush = Brushes.Red;

                isValid = false;
            }
            else
            {
                labelchoose.Content = "";
                chooseButton.BorderBrush = Brushes.Gray;
            }

            return isValid;
        }

        private void CloseImage(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ChooseDialog(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Choose Image";
            dialog.Filter = "All Files|*.*";
            dialog.ShowDialog();
            ImageWindow.imgSource.Source = new ImageSourceConverter().ConvertFromString(dialog.FileName.ToString()) as ImageSource;
        }
    }
}
