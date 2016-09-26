using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;

namespace shapeeditor
{
    public static class ShapesHelper
    {
        public static void ClearShapes(this List<Shape> list)
        {
            list.ForEach(s => s.Tag = s.Tag == null ? ShapeTag.None : (((ShapeTag)s.Tag | ShapeTag.Select) ^ ShapeTag.Select));
            list.Clear();
        }
        public static void AddShape(this List<Shape> list,Shape shape)
        {
            if(shape!=null)
            {
                if (!list.Contains(shape))
                    list.Add(shape);
                shape.Tag = shape.Tag == null ? ShapeTag.Select : ((ShapeTag)shape.Tag | ShapeTag.Select);
                shape.Tag = ((ShapeTag)shape.Tag | ShapeTag.Deleting) ^ ShapeTag.Deleting;
            }
        }
    }

    [FlagsAttribute]
    public enum ShapeTag : byte
    {
        None = 0,
        Select = 1,
        Deleting = 4,
    }
}
