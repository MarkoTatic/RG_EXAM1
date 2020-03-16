using BasicGeometricShapes.AddShapeWindows;
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
        public static List<Shape> canvasShapes;
        public static List<Point> polygonPoints;
        public static List<Ellipse> tempPolyDots;
        public static Stack<UIElement> removedShapesForRedo;
        public static Stack<UIElement> undoClearFunction;
        public MainWindow()
        {
            InitializeComponent();
            canvasShapes = new List<Shape>();
            polygonPoints = new List<Point>();
            tempPolyDots = new List<Ellipse>();
            removedShapesForRedo = new Stack<UIElement>();
            undoClearFunction = new Stack<UIElement>();
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
            ellipse.Fill = Brushes.LightSeaGreen;
            ellipse.Stroke = Brushes.LightSeaGreen;
            ellipse.StrokeThickness = 1;
            Canvas.SetTop(ellipse, p.Y);
            Canvas.SetLeft(ellipse, p.X);
            ActiveCanvas.Children.Add(ellipse);
            tempPolyDots.Add(ellipse);
        }

        private void ActiveCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (polygonPoints.Count > 0)//u slucaju da je stavljena bar jedna tacka na Canvas i klikne se bilo gde teme polygona i ako je polygon aktivan u meniju napravi polygon !!!!!!!![mozda da bude aktivno u meniju ono sto ...]
            {
                PolygonWindow polygonWindow = new PolygonWindow(ActiveCanvas);
                polygonWindow.ShowDialog();
            }
            else if (e.OriginalSource is Ellipse)
            {
                Ellipse clickedEllipsy = (Ellipse)e.OriginalSource;
                ElipseWindowEdit elipseWindow = new ElipseWindowEdit(clickedEllipsy); // 1 for edit
                elipseWindow.ShowDialog();
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
            Rectangle.IsChecked = false;
            Polygon.IsChecked = false;
            Image.IsChecked = false;
            Clear.IsChecked = false;
            Undo.IsChecked = false;
            Redo.IsChecked = false;

            ClearPolygon();
        }

        private void Rectangle_Click(object sender, RoutedEventArgs e)
        {
            Elipse.IsChecked = false;
            Polygon.IsChecked = false;
            Image.IsChecked = false;
            Clear.IsChecked = false;
            Undo.IsChecked = false;
            Redo.IsChecked = false;

            ClearPolygon();
        }

        private void Polygon_Click(object sender, RoutedEventArgs e)
        {
            Rectangle.IsChecked = false;
            Elipse.IsChecked = false;
            Image.IsChecked = false;
            Clear.IsChecked = false;
            Undo.IsChecked = false;
            Redo.IsChecked = false;
        }

        private void Image_Click(object sender, RoutedEventArgs e)
        {
            Rectangle.IsChecked = false;
            Polygon.IsChecked = false;
            Elipse.IsChecked = false;
            Clear.IsChecked = false;
            Undo.IsChecked = false;
            Redo.IsChecked = false;

            ClearPolygon();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            Image.IsChecked = false;
            Rectangle.IsChecked = false;
            Polygon.IsChecked = false;
            Elipse.IsChecked = false;
            Undo.IsChecked = false;
            Redo.IsChecked = false;
           
            foreach (UIElement element in ActiveCanvas.Children)
            {
                undoClearFunction.Push(element);
            }
            ActiveCanvas.Children.Clear();
        }

        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            Image.IsChecked = false;
            Rectangle.IsChecked = false;
            Polygon.IsChecked = false;
            Elipse.IsChecked = false;
            Redo.IsChecked = false;
            Clear.IsChecked = false;
            Undo.IsChecked = false;

            if (ActiveCanvas.Children.Count > 0)
            {
                removedShapesForRedo.Push(ActiveCanvas.Children[ActiveCanvas.Children.Count - 1]);
                ActiveCanvas.Children.RemoveAt(ActiveCanvas.Children.Count - 1);
            }
            else
            {
                foreach (UIElement element in MainWindow.undoClearFunction)
                {
                    ActiveCanvas.Children.Add(element);
                }
                MainWindow.undoClearFunction.Clear();
            }
        }

        private void Redo_Click(object sender, RoutedEventArgs e)
        {
            Image.IsChecked = false;
            Rectangle.IsChecked = false;
            Polygon.IsChecked = false;
            Elipse.IsChecked = false;
            Undo.IsChecked = false;
            Clear.IsChecked = false;
            Redo.IsChecked = false;

            if (removedShapesForRedo.Count > 0)
                ActiveCanvas.Children.Add(removedShapesForRedo.Pop());
        }

        private void ClearPolygon()
        {
            MainWindow.polygonPoints.Clear();

            foreach (Ellipse ellipseDot in MainWindow.tempPolyDots)//fejk pravljenje da dotovi nestanu... jer je referenca moze
            {
                ActiveCanvas.Children.Remove(ellipseDot);
            }
        }
    }
}
