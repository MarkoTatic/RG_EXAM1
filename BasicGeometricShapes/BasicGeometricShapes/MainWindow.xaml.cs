﻿using BasicGeometricShapes.AddShapeWindows;
using BasicGeometricShapes.EditShapeWindows;
using BasicGeometricShapes.UndoRedoCommand;
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
        public static List<Point> polygonPoints;
        public static List<Ellipse> tempPolyDots;

        public CanvasCommand _commander;

        public MainWindow()
        {
            InitializeComponent();
            polygonPoints = new List<Point>();
            tempPolyDots = new List<Ellipse>();

            _commander = new CanvasCommand(ActiveCanvas);
        }

        private void ActiveCanvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Elipse.IsChecked)
            {
                Point p = Mouse.GetPosition(ActiveCanvas);
                ElipseWindow elipseWindow = new ElipseWindow(p, ActiveCanvas);
                elipseWindow.ShowDialog();
            }
            else if (Rectangle.IsChecked)
            {
                Point p = Mouse.GetPosition(ActiveCanvas);
                RectangleWindow rectangleWindow = new RectangleWindow(p, ActiveCanvas);
                rectangleWindow.ShowDialog();
            }
            else if (Polygon.IsChecked)
            {
                Point p = Mouse.GetPosition(ActiveCanvas);
                polygonPoints.Add(p);
                CreateEllipseDotForPolygon(p);   
            }
            else if (Image.IsChecked)
            {
                Point p = Mouse.GetPosition(ActiveCanvas);
                ImageWindow imageWindow = new ImageWindow(p, ActiveCanvas);
                imageWindow.ShowDialog();
            }          
        }

        private void CreateEllipseDotForPolygon(Point p)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Width = 5;
            ellipse.Height = 5;
            ellipse.Fill = Brushes.Crimson;
            ellipse.Stroke = Brushes.Crimson;
            ellipse.StrokeThickness = 1;
            Canvas.SetTop(ellipse, p.Y);
            Canvas.SetLeft(ellipse, p.X);
            ActiveCanvas.Children.Add(ellipse);
            tempPolyDots.Add(ellipse);
        }

        private void ActiveCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (polygonPoints.Count > 0)
            {
                if (MainWindow.polygonPoints.Count < 3)
                {
                    MessageBoxResult result = MessageBox.Show("The polygon must have 3 vertices or more. Would you like to continue drawing the polygon?", "Polygon draw error!", MessageBoxButton.YesNo, MessageBoxImage.Error);
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            break;
                        case MessageBoxResult.No:
                            ClearPolygon();
                            break;
                    }
                }
                else
                {
                    PolygonWindow polygonWindow = new PolygonWindow(ActiveCanvas);
                    polygonWindow.ShowDialog();
                }
            }
            else if (e.OriginalSource is Polygon)
            {
                Polygon clickedPolygon = (Polygon)e.OriginalSource;
                PolygonWindowEdit polygonWindowEdit = new PolygonWindowEdit(clickedPolygon);
                polygonWindowEdit.ShowDialog();

            }
            else if (e.OriginalSource is Ellipse)
            {
                Ellipse clickedEllipsy = (Ellipse)e.OriginalSource;
                ElipseWindowEdit elipseWindowEdit = new ElipseWindowEdit(clickedEllipsy);
                elipseWindowEdit.ShowDialog();
            }
            else if (e.OriginalSource is Rectangle)
            {
                Rectangle clickedRectangle = (Rectangle)e.OriginalSource;
                RectangleWindowEdit rectangleWindowEdit = new RectangleWindowEdit(clickedRectangle);
                rectangleWindowEdit.ShowDialog();
            }
            else if (e.OriginalSource is Image)
            {
                Image clickedImage = (Image)e.OriginalSource;
                ImageWindowEdit imageWindowEdit = new ImageWindowEdit(clickedImage);
                imageWindowEdit.ShowDialog();
            }
        }

        private void Elipse_Click(object sender, RoutedEventArgs e)
        {
            UncheckMenuItems(1);
            ClearPolygon();
        }

        private void Rectangle_Click(object sender, RoutedEventArgs e)
        {
            UncheckMenuItems(2);
            ClearPolygon();
        }

        private void Polygon_Click(object sender, RoutedEventArgs e)
        {
            UncheckMenuItems(3);
        }

        private void Image_Click(object sender, RoutedEventArgs e)
        {
            UncheckMenuItems(4);
            ClearPolygon();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            UncheckMenuItems(5);
            ClearPolygon();//ocisti tacke ako je zapocet poligon

            if (ActiveCanvas.Children.Count > 0)//nema potrebe da radi clear ako je cista tabla...
            {
                ClearCanvasBoard();
            }
        }

        private void ClearCanvasBoard()
        {
            var emptyList = new List<UIElement>();
            ActiveCanvas.Children.Clear();

            CanvasCommand.UndoStack.Push(emptyList);//prazna je lista za undo jer je bio clear
            CanvasCommand.RedoStack.Clear();// jer posle cleara krece na novu granu jer je to nova funkcija kaoo
        }

        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            UncheckMenuItems(0);

            if (MainWindow.polygonPoints.Count > 0)//provera samo da li je krenuo da crta tacke poligona a hoce undo
            {
                MessageBoxResult result = MessageBox.Show("Would you like to continue drawing the polygon? If you don't want it, Clear operation will be performed.", "Polygon draw error!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        Polygon.IsChecked = true;
                        break;
                    case MessageBoxResult.No:
                        ClearPolygon();
                        _commander.Execute();
                        break;
                }
            }
            else
            {
                _commander.Execute();
            }
        }

        private void Redo_Click(object sender, RoutedEventArgs e)
        {
            UncheckMenuItems(0);

            if (MainWindow.polygonPoints.Count > 0)//provera samo da li je krenuo da crta tacke poligona a hoce redo
            {
                MessageBoxResult result = MessageBox.Show("Would you like to continue drawing the polygon? If you don't want it, Clear operation will be performed.", "Polygon draw error!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        Polygon.IsChecked = true;
                        break;
                    case MessageBoxResult.No:
                        ClearPolygon();
                        _commander.UnExecute();
                        break;
                }
            }
            else
            {
                _commander.UnExecute();
            }
        }

        private void UncheckMenuItems(int positionInMenu)
        {
            Image.IsChecked = false;
            Rectangle.IsChecked = false;
            Polygon.IsChecked = false;
            Elipse.IsChecked = false;
            Undo.IsChecked = false;
            Clear.IsChecked = false;
            Redo.IsChecked = false;
            if (positionInMenu == 1)
            {
                Elipse.IsChecked = true;
            }
            else if (positionInMenu == 2)
            {
                Rectangle.IsChecked = true;
            }
            else if (positionInMenu == 3)
            {
                Polygon.IsChecked = true;
            }
            else if (positionInMenu == 4)
            {
                Image.IsChecked = true;
            }
            else if (positionInMenu == 5)
            {
                Clear.IsChecked = true;
            }
            else if (positionInMenu == 6)
            {
                Undo.IsChecked = true;
            }
            else if (positionInMenu == 7)
            {
                Redo.IsChecked = true;
            }
        }

        private void ClearPolygon()
        {
            MainWindow.polygonPoints.Clear();

            foreach (Ellipse ellipseDot in MainWindow.tempPolyDots)
            {
                ActiveCanvas.Children.Remove(ellipseDot);
            }
        }
    }
}
