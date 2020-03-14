﻿using BasicGeometricShapes.AddShapeWindows;
using BasicGeometricShapes.EditShapeWindows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BasicGeometricShapes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string selectedMenuItem = "";
        public static List<Shape> canvasShapes;
        public MainWindow()
        {
            InitializeComponent();
            canvasShapes = new List<Shape>();
        }

        private void ActiveCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Elipse.IsChecked)
            {
                selectedMenuItem = "Elipse";
                Point p = Mouse.GetPosition(ActiveCanvas);
                ElipseWindow elipseWindow = new ElipseWindow(p, ActiveCanvas); // 0 for add
                elipseWindow.ShowDialog();
            }
            else if (Rectangle.IsChecked)
            {
                selectedMenuItem = "Rectangle";
                Point p = Mouse.GetPosition(ActiveCanvas);
                RectangleWindow rectangleWindow = new RectangleWindow(p, ActiveCanvas);
                rectangleWindow.ShowDialog();
            }
            else if (Polygon.IsChecked)
            {
                selectedMenuItem = "Polygon";
            }
            else if (Image.IsChecked)
            {
                selectedMenuItem = "Image";
            }          
        }

        private void ActiveCanvas_MouseLeftButtonDown2(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is Ellipse)//fercera ko zmaj
            {
                Ellipse clickedEllipsy = (Ellipse)e.OriginalSource;
                Point p = Mouse.GetPosition(ActiveCanvas);
                ElipseWindowEdit elipseWindow = new ElipseWindowEdit(ActiveCanvas, clickedEllipsy); // 1 for edit
                elipseWindow.ShowDialog();
            }
            else if (e.OriginalSource is Rectangle)
            {
                Rectangle clickedRectangle = (Rectangle)e.OriginalSource;
                RectangleWindowEdit rectangleWindowEdit = new RectangleWindowEdit(ActiveCanvas, clickedRectangle);
                rectangleWindowEdit.ShowDialog();
            }
        }

        private void Elipse_Click(object sender, RoutedEventArgs e)
        {
            Rectangle.IsChecked = false;
            Polygon.IsChecked = false;
            Image.IsChecked = false;
            Clear.IsChecked = false;
        }

        private void Rectangle_Click(object sender, RoutedEventArgs e)
        {
            Elipse.IsChecked = false;
            Polygon.IsChecked = false;
            Image.IsChecked = false;
            Clear.IsChecked = false;
        }

        private void Polygon_Click(object sender, RoutedEventArgs e)
        {
            Rectangle.IsChecked = false;
            Elipse.IsChecked = false;
            Image.IsChecked = false;
            Clear.IsChecked = false;
        }

        private void Image_Click(object sender, RoutedEventArgs e)
        {
            Rectangle.IsChecked = false;
            Polygon.IsChecked = false;
            Elipse.IsChecked = false;
            Clear.IsChecked = false;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            Image.IsChecked = false;
            Rectangle.IsChecked = false;
            Polygon.IsChecked = false;
            Elipse.IsChecked = false;
            ActiveCanvas.Children.RemoveRange(0, ActiveCanvas.Children.Count);
        }
    }
}