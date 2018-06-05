using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Interactivity;

namespace MvvmLight1.ViewModel
{
    public class TransformInCanvasBehavior : Behavior<FrameworkElement>
    {
        private TranslateTransform elementTranslate;
        
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Loaded += AssociatedObjectLoaded;

            //先将对象置于左上角
            AssociatedObject.HorizontalAlignment = HorizontalAlignment.Left;
            AssociatedObject.VerticalAlignment = VerticalAlignment.Top;

            // Hook up event handlers. 
            this.AssociatedObject.MouseLeftButtonDown += AssociatedObject_MouseLeftButtonDown;
            this.AssociatedObject.MouseMove += AssociatedObject_MouseMove;
            this.AssociatedObject.MouseLeftButtonUp += AssociatedObject_MouseLeftButtonUp;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            // Detach event handlers. 
            this.AssociatedObject.MouseLeftButtonDown -= AssociatedObject_MouseLeftButtonDown;
            this.AssociatedObject.MouseMove -= AssociatedObject_MouseMove;
            this.AssociatedObject.MouseLeftButtonUp -= AssociatedObject_MouseLeftButtonUp;
        }

        void AssociatedObjectLoaded(object sender, RoutedEventArgs e)
        {
            //默认先给对象创建一个TranslateTransform
            //AssociatedObject.RenderTransform = elementTranslate;
            elementTranslate = AssociatedObject.RenderTransform as TranslateTransform;
        }

        // Keep track of when the element is being dragged. 
        private bool isDragging = false;

        // When the element is clicked, record the exact position 
        // where the click is made. 
        private Point sttPoint;

        private void AssociatedObject_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            // Dragging mode begins. 
            isDragging = true;
            
            sttPoint = e.GetPosition(AssociatedObject);
            var path = AssociatedObject as Path;
            if (path != null)
            {
                path.Fill = System.Windows.Media.Brushes.MediumSlateBlue;
            }
                

            // Capture the mouse. This way you'll keep receiveing 
            // the MouseMove event even if the user jerks the mouse 
            // off the element. 
            AssociatedObject.CaptureMouse();
        }

        private void AssociatedObject_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPoint = e.GetPosition(AssociatedObject);
                
                elementTranslate.X += currentPoint.X - sttPoint.X;
                elementTranslate.Y += currentPoint.Y - sttPoint.Y;
            }
        }

        private void AssociatedObject_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (isDragging)
            {
                AssociatedObject.ReleaseMouseCapture();
                sttPoint = e.GetPosition(AssociatedObject);
                var path = AssociatedObject as Path;
                if (path != null)
                {
                    path.Fill = System.Windows.Media.Brushes.AliceBlue;
                }
                isDragging = false;
            }
        }
    }
}