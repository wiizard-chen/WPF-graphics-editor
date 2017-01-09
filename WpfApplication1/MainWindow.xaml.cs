using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToolTray;
namespace WpfApplication1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        private const int StandWidth = 720;
        private const int StandHeight = 480;

        private string _filePath;

        private double _ratio;

        private IDWMouseOperation test;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {//select
            Remove();
            test = new DTSelectors(this.canvas);
            Mouse.AddMouseDownHandler(this.canvas, test.DWMouseDown);
            Mouse.AddMouseMoveHandler(this.canvas, test.DWMouseMove);
            Mouse.AddMouseUpHandler(this.canvas, test.DWMouseUp);
            Keyboard.AddPreviewKeyDownHandler(this, (test as IDWKeyboardOperation).DWKeyDown);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Remove();
            test = new DTRectangles(this.canvas);
            Mouse.AddMouseDownHandler(this.canvas, test.DWMouseDown);
            Mouse.AddMouseMoveHandler(this.canvas, test.DWMouseMove);
            Mouse.AddMouseUpHandler(this.canvas, test.DWMouseUp);
        }

        private void Remove()
        {
            if (test != null)
            {
                Mouse.RemoveMouseDownHandler(this.canvas, test.DWMouseDown);
                Mouse.RemoveMouseMoveHandler(this.canvas, test.DWMouseMove);
                Mouse.RemoveMouseUpHandler(this.canvas, test.DWMouseUp);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Remove();
            test = new DTTexts(this.canvas);
            Mouse.AddMouseDownHandler(this.canvas, test.DWMouseDown);
            Mouse.AddMouseMoveHandler(this.canvas, test.DWMouseMove);
            Mouse.AddMouseUpHandler(this.canvas, test.DWMouseUp);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Remove();
            test = new DTLines(this.canvas);
            Mouse.AddMouseDownHandler(this.canvas, test.DWMouseDown);
            Mouse.AddMouseMoveHandler(this.canvas, test.DWMouseMove);
            Mouse.AddMouseUpHandler(this.canvas, test.DWMouseUp);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                _filePath = openFileDialog.FileName;
                ImageBrush brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri(_filePath));
                //图片尺寸按比例缩小
                var width = brush.ImageSource.Width;
                var height = brush.ImageSource.Height;
                if (width >= StandWidth && height >= StandHeight)
                {
                    if (width > height)
                    {
                        _ratio = Math.Round(StandWidth / width, 4);
                    }
                    else
                    {
                        _ratio = Math.Round(StandHeight / height, 4);
                    }
                }
                else
                    _ratio = 1;
                this.canvas.Width = width * _ratio;
                this.canvas.Height = height * _ratio;
                canvas.Background = brush;
            }
            Remove();
        }







        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            this.canvas.Children.Clear();
            this.canvas.Background = Brushes.White;
            Remove();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            //Rect bounds = VisualTreeHelper.GetDescendantBounds(this.canvas);
            //double dpi = 96d;
            //RenderTargetBitmap rtb = new RenderTargetBitmap((int)bounds.Width, (int)bounds.Height, dpi, dpi, PixelFormats.Default);

            //DrawingVisual dv = new DrawingVisual();
            //using (DrawingContext dc = dv.RenderOpen())
            //{
            //    VisualBrush vb = new VisualBrush(this.canvas);
            //    dc.DrawRectangle(vb, null, new Rect(new Point(), bounds.Size));
            //}
            //rtb.Render(dv);

            //BitmapEncoder jpedEncoder = new JpegBitmapEncoder();
            //jpedEncoder.Frames.Add(BitmapFrame.Create(rtb));
            //using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            //{
            //    jpedEncoder.Save(ms);
            //    var str = Environment.CurrentDirectory + "\\fuck.jpeg";
            //    System.IO.File.WriteAllBytes(str, ms.ToArray());
            //}
            SaveTheSize();
            Remove();
        }

        private void SaveTheSize()
        {
            ImageBrush brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri(_filePath));
            Canvas savecanvas1 = new Canvas();
            savecanvas1.Width = brush.ImageSource.Width;
            savecanvas1.Height = brush.ImageSource.Height;
            savecanvas1.Background = brush;

            var elems = this.canvas.Children.OfType<FrameworkElement>().Where(s => s.Visibility == Visibility.Visible);
            foreach (var item in elems)
            {
                if (item.Tag is ISaveTheSize)
                {
                    (item.Tag as ISaveTheSize).SaveTheSize(savecanvas1, _ratio);
                }
            }
            //this.canvas = savecanvas;
            //SaveImage(savecanvas1); 


            double dpi = 96d;
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)savecanvas1.Width, (int)savecanvas1.Height, dpi, dpi, PixelFormats.Default);

            DrawingVisual dv = new DrawingVisual();
            using (DrawingContext dc = dv.RenderOpen())
            {
                VisualBrush vb = new VisualBrush(this.canvas);
                dc.DrawRectangle(vb, null, new Rect(new Point(), new Size(savecanvas1.Width, savecanvas1.Height)));
            }
            rtb.Render(dv);

            BitmapEncoder jpedEncoder = new JpegBitmapEncoder();
            jpedEncoder.Frames.Add(BitmapFrame.Create(rtb));
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                jpedEncoder.Save(ms);
                var str = Environment.CurrentDirectory + "\\fuck.jpeg";
                System.IO.File.WriteAllBytes(str, ms.ToArray());
            }
        }


        private void SaveImage(Canvas savecanvas1)
        {
            Rect bounds = VisualTreeHelper.GetDescendantBounds(savecanvas1);
            double dpi = 96d;
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)bounds.Width, (int)bounds.Height, dpi, dpi, PixelFormats.Default);

            DrawingVisual dv = new DrawingVisual();
            using (DrawingContext dc = dv.RenderOpen())
            {
                VisualBrush vb = new VisualBrush(this.canvas);
                dc.DrawRectangle(vb, null, new Rect(new Point(), bounds.Size));
            }
            rtb.Render(dv);

            BitmapEncoder jpedEncoder = new JpegBitmapEncoder();
            jpedEncoder.Frames.Add(BitmapFrame.Create(rtb));
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                jpedEncoder.Save(ms);
                var str = Environment.CurrentDirectory + "\\fuck.jpeg";
                System.IO.File.WriteAllBytes(str, ms.ToArray());
            }
        }




        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            Remove();
            test = new DTArrows(this.canvas);
            Mouse.AddMouseDownHandler(this.canvas, test.DWMouseDown);
            Mouse.AddMouseMoveHandler(this.canvas, test.DWMouseMove);
            Mouse.AddMouseUpHandler(this.canvas, test.DWMouseUp);
        }
    }
}
