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

            ListaDoMaloania.Items.Add("Line");
            ListaDoMaloania.Items.Add("Path");
            ListaDoMaloania.Items.Add("Polygon");
            ListaDoMaloania.Items.Add("Polyline");
            ListaDoMaloania.Items.Add("Rectangle");
            ListaDoMaloania.Items.Add("Shape");
        }


        bool isDown = false;

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

        void PaintRectangle(Brush circleColor, Point position)
        {
            Rectangle rectangle = new Rectangle();
            rectangle.Fill = circleColor;
            rectangle.Width = 5;
            rectangle.Height = 5;
            Canvas.SetLeft(rectangle, position.X);
            Canvas.SetTop(rectangle, position.Y);
            CanvasTest.Children.Add(rectangle);
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDown)
            {
                Point mousePosition = e.GetPosition(CanvasTest);
                //wyrysowanie Circle
                PaintCircle(Brushes.Black, mousePosition);
            }

            if (ListaDoMaloania.SelectedItem == "Rectangle")
            {
                if (isDown)
                {
                    Point mousePosition = e.GetPosition(CanvasTest);
                    PaintRectangle(Brushes.Black, mousePosition);
                }
            }
        }

        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDown = false;
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDown = true;
            //System.Windows.Shapes.Ellipse

        }
    }
}