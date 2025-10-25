using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Live.Controls
{
    internal class Hexview : Control
    {
        private Pen _pen = new(
            new SolidColorBrush(
                Color.FromArgb(255, 255,255, 128)), 
            1.0);
        static Hexview()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Hexview), new FrameworkPropertyMetadata(typeof(Hexview)));
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            drawingContext.DrawLine(_pen, new Point(0.0, 0.0), new Point(100.0, 100.0));

        }
    }
}
