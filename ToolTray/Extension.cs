using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ToolTray
{
    public static class ShapesHelper
    {
        public static PolyLineSegment GetSegment(this Path p)
        {
            return ((p.Data as PathGeometry).Figures[0] as PathFigure).Segments[0] as PolyLineSegment;
        }

        public static void SetTopLeft(this Path p, Point pt)
        {
            ((p.Data as PathGeometry).Figures[0] as PathFigure).StartPoint = pt;
            GetSegment(p).Points[0] = pt;
        }
    }
}
