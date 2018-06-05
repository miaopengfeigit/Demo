using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MaterialDesignThemes.Wpf;
using MaterialDesignColors;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Common;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Media;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Controls;

namespace MvvmLight1.ViewModel
{
    public class VisionViewModel : ViewModelBase
    {
        //#region Singleton
        //private static readonly object syncObject = new object();
        //private static VisionViewModel instance;
        //private VisionViewModel()
        //{
           
        //}

        //public static VisionViewModel Instance
        //{
        //    get
        //    {
        //        lock (syncObject)
        //        {
        //            return instance ?? (instance = new VisionViewModel());
        //        }
        //    }
        //}

        //#endregion
        public VisionViewModel()
        {
            CanvasVecPicture.Add(new MyItemViewModel
            {
                Name = "mmm",
                Left = 100,
                Top = 10,
                Hight = 100,
                Wdith = 100,
                PathType = 0,
                MyGeometry = new EllipseGeometry(new Point(10, 10), 10, 10),
                Translate = new TranslateTransform(12,12)
            });
            CanvasVecPicture.Add(new MyItemViewModel
            {
                Name = "mmm2",
                Left = 100,
                Top = 10,
                Hight = 100,
                Wdith = 100,
                PathType = 0,
                MyGeometry = new EllipseGeometry(new Point(10, 10), 20, 20),
                Translate = new TranslateTransform()
            });

            SelectPath = CanvasVecPicture[0];

            Geometry geometry = new EllipseGeometry(new Point(10, 10), 20, 20);
            Geometry geometry1 = new EllipseGeometry(new Point(30, 30), 20, 30);

            CanvasPath.Add(AddPath(geometry));
            CanvasPath.Add(AddPath(geometry1));

            SelectWidth = 500;
        }
        private ObservableCollection<Path> canvasPath = new ObservableCollection<Path>();
        public ObservableCollection<Path> CanvasPath
        {
            get
            {
                return canvasPath;
            }

            set
            {
                canvasPath = value;
            }
        }

        private ObservableCollection<MyItemViewModel> canvasVecPicture = new ObservableCollection<MyItemViewModel>();
        public ObservableCollection<MyItemViewModel> CanvasVecPicture
        {
            get { return canvasVecPicture; }
            set { canvasVecPicture = value; }
        }

        public  MyItemViewModel SelectPath { get; set; }

        bool captured = false;
        double xShape, xCanvas, yShape, yCanvas;
        private Path pathTemp;
        private Canvas canvas;

        private CanvasOpMode _opMode = CanvasOpMode.View;
        public enum CanvasOpMode
        {
            View,
            Selection,
            Line,
            Rect,
            Circle,
            Ellipse,
            Text,
        }

        private void shape_MouseLeftButtonDown(Object sender, MouseButtonEventArgs e)
        {
            //pathTemp = (UIElement)sender;
            ////var mm = VisualTreeHelper.GetParent(source);
            //Mouse.Capture(pathTemp);
            //captured = true;
            //xShape = Canvas.GetLeft(pathTemp);
            //xCanvas = e.GetPosition(canvas).X;
            //yShape = Canvas.GetTop(pathTemp);
            //yCanvas = e.GetPosition(canvas).Y;
        }

        private void shape_MouseMove(Object sender, MouseEventArgs e)
        {
            if (captured)
            {
                var cp = e.GetPosition(canvas);

                xShape += cp.X - xCanvas;
                Canvas.SetLeft(pathTemp, xShape);
                xCanvas = cp.X;

                yShape += cp.Y - yCanvas;
                Canvas.SetTop(pathTemp, yShape);
                yCanvas = cp.Y;
            }
        }

        private void shape_MouseLeftButtonUp(Object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(null);
            captured = false;
        }

        private void shape_Add(Object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(null);
            captured = false;
        }

        #region 属性



        private BitmapSource imgSourc;
        public BitmapSource ImgSourc
        {
            get
            {
                return imgSourc;
            }

            set
            {
                SetProperty(ref imgSourc, value);
            }
        }

        private string str;
        public string Str
        {
            get
            {
                return str;
            }
            set
            {
                SetProperty(ref str, value);
            }
        }

        private int selectWidth;
        public int SelectWidth
        {
            get
            {
                return selectWidth;
            }
            set
            {
                //Str = selectWidth.ToString();
                SetProperty(ref selectWidth, value);
            }
        }
        #endregion

        #region 命令
        public ICommand SourcePathCmd
        {
            get
            {
                return new DelegateCommand(o => ExcuteSourcePathCmd());
            }
        }


        //private RelayCommand sourcePathCmd;
        ///// <summary>
        ///// SourcePathCmd
        ///// </summary>
        //public RelayCommand SourcePathCmd
        //{
        //    get
        //    {
        //        if (sourcePathCmd == null)
        //            sourcePathCmd = new RelayCommand(() => ExcuteSourcePathCmd());
        //        return sourcePathCmd;

        //    }
        //    set { sourcePathCmd = value; }
        //}

        private void ExcuteSourcePathCmd()
        {
            string path = OpenFileDialog();
            if (!string.IsNullOrEmpty(path))
            {
                //BitmapImage image = new BitmapImage();
                //image.BeginInit();
                ////image.StreamSource = new MemoryStream(bytes);
                //image.UriSource = new Uri(path);//(@"C:\Users\miaopengfei\Desktop\1487.jpg");
                //image.EndInit();
                ImgSourc = Cv.ReadBitmapImage1(path);
            }
        }
        public ICommand LoadedCommand
        {
            get { return new DelegateCommand(e => Loaded()); }
        }

        private void Loaded()
        {
            MessageBox.Show("ok");
        }

        //public ICommand MouseLeftButtonDownCommand
        //{
        //    get { return new DelegateCommand(e => MouseLeftButtonDown((MouseEventArgs)e)); }
        //}

        //private RelayCommand<MouseEventArgs> mouseLeftButtonDownCommand;
        //public RelayCommand<MouseEventArgs> MouseLeftButtonDownCommand
        //{
        //    get
        //    {
        //        if (mouseLeftButtonDownCommand == null)
        //            mouseLeftButtonDownCommand = new RelayCommand<MouseEventArgs>((e) => MouseLeftButtonDown(e));
        //        return mouseLeftButtonDownCommand;
        //    }
        //}

        public void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var element = sender as UIElement;
            var p = e.GetPosition(element);
            Str = string.Empty;
            Str = string.Format("X{0} Y{1}", p.X, p.Y);
            switch (_opMode)
            {
                case CanvasOpMode.View:
                    //VisualTreeHelper.HitTest(element, null, f =>
                    // {
                    //     Str += f.VisualHit.ToString();
                    //     return HitTestResultBehavior.Continue;
                    // }, new PointHitTestParameters(p));
                    var path = element.InputHitTest(p);
                    Str += path.ToString()+element.ToString();
                    if (path.ToString()!= "System.Windows.Controls.Canvas")
                    {
                        pathTemp = (Path)element;
                        Str += "Hit It";
                    }
                    break;
                case CanvasOpMode.Ellipse:
                    pathTemp = AddPath(new EllipseGeometry(p, 1, 1));
                    //pathTemp.IsHitTestVisible = true;
                    xCanvas = p.X;
                    yCanvas = p.Y;
                    //CanvasVecPicture.Add(pathTemp);
                    break;
                default:
                    break;
            }
            captured = true;
        }

        //private RelayCommand<MouseEventArgs> mouseLeftButtonUpCommand;
        //public RelayCommand<MouseEventArgs> MouseLeftButtonUpCommand
        //{
        //    get
        //    {
        //        if (mouseLeftButtonUpCommand == null)
        //            mouseLeftButtonUpCommand = new RelayCommand<MouseEventArgs>((e) => MouseLeftButtonUp(e));
        //        return mouseLeftButtonUpCommand;
        //    }
        //}
        //public ICommand MouseLeftButtonUpCommand
        //{
        //    get { return new DelegateCommand(e => MouseLeftButtonUp((MouseEventArgs)e)); }
        //}


        public void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var element = sender as UIElement;
            var p = e.GetPosition(element);
            switch (_opMode)
            {
                case CanvasOpMode.View:
                    break;
                case CanvasOpMode.Ellipse:
                    _opMode = CanvasOpMode.View;
                    break;
                default:
                    break;
            }
            captured = false;
            pathTemp = null;
        }

        //private RelayCommand<MouseEventArgs> mouseMoveCommand;
        //public RelayCommand<MouseEventArgs> MouseMoveCommand
        //{
        //    get
        //    {
        //        if (mouseMoveCommand == null)
        //            mouseMoveCommand = new RelayCommand<MouseEventArgs>((e) => MouseMove(e));
        //        return mouseMoveCommand;
        //    }
        //}
        //public ICommand MouseMoveCommand
        //{
        //    get { return new DelegateCommand(e => MouseMove((MouseEventArgs)e)); }
        //}

        public void MouseMove(object sender, MouseEventArgs e)
        {
            if (!captured || pathTemp == null) return;
            var element = sender as UIElement;
            var p = e.GetPosition(element);
            switch (_opMode)
            {
                case CanvasOpMode.View:
                    xShape += p.X - xCanvas;
                    Canvas.SetLeft(pathTemp, xShape);
                    xCanvas = p.X;

                    yShape += p.Y - yCanvas;
                    Canvas.SetTop(pathTemp, yShape);
                    yCanvas = p.Y;
                    break;
                case CanvasOpMode.Ellipse:
                    var path = (EllipseGeometry)pathTemp.Data;
                    path.RadiusX = p.X - xCanvas;
                    path.RadiusY = p.Y - yCanvas;
                    break;
                default:
                    break;
            }
        }

        //private RelayCommand addLine;
        //public RelayCommand AddLine
        //{
        //    get
        //    {
        //        if (addLine == null)
        //            addLine = new RelayCommand(() => { _opMode = CanvasOpMode.Line; });
        //        return addLine;
        //    }
        //}

        public ICommand AddLine
        {
            get { return new DelegateCommand(e => { _opMode = CanvasOpMode.Line; }); }
        }

        //private RelayCommand addRect;
        //public RelayCommand AddRect
        //{
        //    get
        //    {
        //        if (addRect == null)
        //            addRect = new RelayCommand(() => { _opMode = CanvasOpMode.Rect; });
        //        return addRect;
        //    }
        //}
        public ICommand AddRect
        {
            get { return new DelegateCommand(e => { _opMode = CanvasOpMode.Rect; }); }
        }

        //private RelayCommand addEllipseCommand;
        //public RelayCommand AddEllipseCommand
        //{
        //    get
        //    {
        //        if (addEllipseCommand == null)
        //            addEllipseCommand = new RelayCommand(() => { _opMode = CanvasOpMode.Ellipse; });
        //        return addEllipseCommand;
        //    }
        //}
        public ICommand AddEllipseCommand
        {
            get { return new DelegateCommand(e => { _opMode = CanvasOpMode.Ellipse; }); }
        }

        

        private Path AddPath(Geometry geometry)
        {
            Path myPath = new Path();
            myPath.Stroke = System.Windows.Media.Brushes.Black;
            myPath.Fill = System.Windows.Media.Brushes.MediumSlateBlue;
            myPath.StrokeThickness = 1;
            myPath.HorizontalAlignment = HorizontalAlignment.Left;
            myPath.VerticalAlignment = VerticalAlignment.Top;
            myPath.Data = geometry;
            return myPath;
        }
        
        #endregion

        #region 方法

        private string OpenFileDialog()
        {
            var dialog = new CommonOpenFileDialog();
            dialog.Title = SR.GetString("Language");
            //dialog.IsFolderPicker = true;
            if (CommonFileDialogResult.Ok == dialog.ShowDialog())
                return dialog.FileName;
            else
                return string.Empty;
        }

        #endregion
    }

    public class MyItemViewModel
    {
        public string Name { get; set; }

        //public Path MyPath { get; set; }

        public Geometry MyGeometry { get; set; }

        public TranslateTransform Translate { get; set; }

        public int Left { get; set; }

        public int Top { get; set; }

        public int Hight { get; set; }

        public int Wdith { get; set; }

        public int PathType { get; set; }
    }
}
