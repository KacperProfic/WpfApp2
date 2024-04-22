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


namespace WpfAppTest2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ListaDoMaloania.Items.Add("Circle");
            ListaDoMaloania.Items.Add("Line"); 
            ListaDoMaloania.Items.Add("Pentagon"); 
            ListaDoMaloania.Items.Add("Polyline");
            ListaDoMaloania.Items.Add("Rectangle"); 
            ListaDoMaloania.Items.Add("Triangle"); 
            currentPolyline = null;
        }

       
      
        private Point? startPoint = null;
        bool isDown = false;
        private Polyline currentPolyline;
        void PaintCircle(Brush circleColor, Point position)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Fill = circleColor;
            ellipse.Width = 15;
            ellipse.Height = 15;
            Canvas.SetLeft(ellipse, position.X);
            Canvas.SetTop(ellipse, position.Y);
            CanvasTest.Children.Add(ellipse);
        }

        private void DrawLine(Brush color, Point start, Point end)
        {
            Line line = new Line
            {
                Stroke = color,
                X1 = start.X,
                Y1 = start.Y,
                X2 = end.X,
                Y2 = end.Y,
                StrokeThickness = 2
            };
            CanvasTest.Children.Add(line);
        }

        void DrawRectangle(Brush circleColor, Point position)
        {
            Rectangle rectangle = new Rectangle();
            rectangle.Fill = circleColor;
            rectangle.Width = 15;
            rectangle.Height = 15;
            Canvas.SetLeft(rectangle, position.X);
            Canvas.SetTop(rectangle, position.Y);
            CanvasTest.Children.Add(rectangle);
        }

        void DrawTriangle(Brush fillColor, Point position)
        {
            Polygon triangle = new Polygon();
            triangle.Fill = fillColor;
            triangle.Stroke = Brushes.Black;
            triangle.StrokeThickness = 2;


            int sideLength = 30;
            double height = sideLength * (Math.Sqrt(3) / 2);

            Point point1 = new Point(position.X, position.Y - (height / 3));
            Point point2 = new Point(position.X - (sideLength / 2), position.Y + (height * 2 / 3));
            Point point3 = new Point(position.X + (sideLength / 2), position.Y + (height * 2 / 3));


            triangle.Points.Add(point1);
            triangle.Points.Add(point2);
            triangle.Points.Add(point3);


            CanvasTest.Children.Add(triangle);
        }

        private void DrawPolyline(Point newPoint)
        {
            if (currentPolyline == null)
            {
                
                currentPolyline = new Polyline
                {
                    Stroke = Brushes.Black,
                    StrokeThickness = 2
                };
                CanvasTest.Children.Add(currentPolyline);
            }

           
            currentPolyline.Points.Add(newPoint);
        }
        void DrawPentagon(Brush fillColor, Point center)
        {
            Polygon pentagon = new Polygon();
            pentagon.Fill = fillColor;
            pentagon.Stroke = Brushes.Black;
            pentagon.StrokeThickness = 2;

            double radius = 50;  
            int sides = 5; 
            double angle = 360.0 / sides;

            
            for (int i = 0; i < sides; i++)
            {
                double theta = (angle * i - 90) * Math.PI / 180.0;  
                Point vertex = new Point(
                    center.X + radius * Math.Cos(theta),
                    center.Y + radius * Math.Sin(theta)
                );
                pentagon.Points.Add(vertex);
            }

            CanvasTest.Children.Add(pentagon);
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isDown || startPoint == null) return;

            Point mousePosition = e.GetPosition(CanvasTest);
            if (ListaDoMaloania.SelectedItem?.ToString() == "Line")
            {

                DrawLine(Brushes.Black, startPoint.Value, mousePosition);
                startPoint = mousePosition;
            }
            else if (ListaDoMaloania.SelectedItem?.ToString() == "Rectangle")
            {

                DrawRectangle(Brushes.LightBlue, mousePosition);
            }
            else if (ListaDoMaloania.SelectedItem?.ToString() == "Triangle")
            {

                DrawTriangle(Brushes.LightBlue, mousePosition);
            }
            else  if (ListaDoMaloania.SelectedItem?.ToString() == "Circle")
            {
                PaintCircle(Brushes.Black, mousePosition);
            }



        }

        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDown = false;
            startPoint = null;

        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDown = true;
            startPoint = e.GetPosition(CanvasTest);
            if (ListaDoMaloania.SelectedItem?.ToString() == "Polyline")
            {
                Point mousePosition = e.GetPosition(CanvasTest);
                DrawPolyline(mousePosition);
            } else if (ListaDoMaloania.SelectedItem?.ToString() == "Pentagon")
            {
                Point mousePosition = e.GetPosition(CanvasTest);
                DrawPentagon(Brushes.LightBlue, mousePosition);
            }

        }
        private void Canvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
           
            if (currentPolyline != null && ListaDoMaloania.SelectedItem?.ToString() == "Polyline")
            {
                currentPolyline = null; 
            }
        }
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearCanvas();
        }

        private void ClearCanvas()
        {
            CanvasTest.Children.Clear();  
        }
    }
}