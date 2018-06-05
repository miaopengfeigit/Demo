using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MvvmLight1.ViewModel
{
    public class Drag : Behavior<FrameworkElement>
    {
        public static readonly DependencyProperty IsMovableProperty =
            DependencyProperty.Register("IsMovable", typeof(bool),
                                        typeof(Drag), new PropertyMetadata(null));

        [Category("Target Properties")]
        public bool IsMovable { get; set; }

        private bool _isDragging = false;
        private Point _offset;
        private readonly TranslateTransform _elementTranslate = new TranslateTransform();
        private TranslateTransform _imgTranslate = new TranslateTransform();
        private Image _img = new Image();

        /// <summary>
        /// Drag行为附加到对象上时触发
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.Loaded += AssociatedObjectLoaded;

            //先将对象置于左上角
            AssociatedObject.HorizontalAlignment = HorizontalAlignment.Left;
            AssociatedObject.VerticalAlignment = VerticalAlignment.Top;


            AssociatedObject.MouseLeftButtonDown += AssociatedObjectMouseLeftButtonDown;
        }

        void AssociatedObjectLoaded(object sender, RoutedEventArgs e)
        {
            //默认先给对象创建一个TranslateTransform
            AssociatedObject.RenderTransform = _elementTranslate;
        }

        /// <summary>
        /// Drag行为从对象剥离时触发
        /// </summary>
        protected override void OnDetaching()
        {
            base.OnDetaching();
            //移除鼠标左键事件处理
            AssociatedObject.MouseLeftButtonDown -= AssociatedObjectMouseLeftButtonDown;
        }

        /// <summary>
        /// 动象拖动时的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AssociatedObjectMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!_isDragging) return;
            FrameworkElement parent = _img.Parent as FrameworkElement;
            Point newPosition = e.GetPosition(parent);

            //移动的其实只是对象的"影子副本"
            _imgTranslate.X = (newPosition.X - _offset.X);
            _imgTranslate.Y = (newPosition.Y - _offset.Y);
        }

        /// <summary>
        /// 托运结束时的处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AssociatedObjectMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!_isDragging) return;
            Panel panel = AssociatedObject.Parent as Panel;

            //停止拖动
            _isDragging = false;

            //释放鼠标
            _img.ReleaseMouseCapture();

            //解除事件绑定
            _img.MouseMove -= AssociatedObjectMouseMove;
            _img.MouseLeftButtonUp -= AssociatedObjectMouseLeftButtonUp;

            //如果允许移动，则将"影子Transform"的偏移量赋值给"对象的Transform"
            if (IsMovable)
            {
                _elementTranslate.X = _imgTranslate.X;
                _elementTranslate.Y = _imgTranslate.Y;
            }

            //重新初始化偏移量，同时将对象本身恢复原透明度
            _imgTranslate = new TranslateTransform();
            _offset = new Point(0, 0);
            AssociatedObject.Opacity = 1;

            //清除Image
            if (panel != null) panel.Children.Remove(_img);

            //为下次移动准备一个新的Image
            _img = new Image();
        }


        /// <summary>
        /// 开始拖动时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AssociatedObjectMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //_isDragging = true;//处理标志位

            //AssociatedObject.Opacity = .35;//将对象透明度降低

            ////生成对象的"位图影子副本"
            //WriteableBitmap bitmap = new WriteableBitmap(AssociatedObject, new TranslateTransform());
            //if (_img == null) return;

            //_img.Source = bitmap;
            //_img.HorizontalAlignment = HorizontalAlignment.Left;
            //_img.VerticalAlignment = VerticalAlignment.Top;
            //_img.Stretch = Stretch.None;
            //_img.Width = bitmap.PixelWidth;
            //_img.Height = bitmap.PixelHeight;

            //_imgTranslate.X = _elementTranslate.X;
            //_imgTranslate.Y = _elementTranslate.Y;

            //_img.RenderTransform = _imgTranslate;

            ////注册鼠标事件，以响应拖动
            //_img.MouseMove += AssociatedObjectMouseMove;
            //_img.MouseLeftButtonUp += AssociatedObjectMouseLeftButtonUp;

            //Panel panel = AssociatedObject.Parent as Panel;

            //if (panel != null) panel.Children.Add(_img);

            //_offset = e.GetPosition(_img);

            ////捕获鼠标，以防止鼠标移动过快时，甩掉"影子对象"
            //_img.CaptureMouse();
        }
    }
}
