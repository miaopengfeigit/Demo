using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Common
{
    public class MyRectangle: Shape
    {
        public MyRectangle()
        {
            MouseLeftButtonDown += MouseButtonEvent;
        }
        protected override Geometry DefiningGeometry
        {
            get { return new RectangleGeometry(new Rect(0,0,10,10)); }
        }

        private void MouseButtonEvent(object sender, MouseButtonEventArgs e)
        {
            
            Height += 10;
            Width += 10;
            var rect = RenderedGeometry.Bounds;
            rect.Width = Width;
            rect.Height = Height;
        }

    }
}
