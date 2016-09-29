using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace graphiceditor.ToolsDots
{
    public class DrawToolDot
    {
        /// <summary>
        /// 父类
        /// </summary>
        public DrawToolDots Parent { get; private set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 坐标X
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// 坐标Y
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// 坐标
        /// </summary>
        public Point Point
        {
            get
            {
                return new Point(this.X, this.Y);
            }
        }

        /// <summary>
        /// 坐标位置
        /// </summary>
        public RectPoints RectPoint
        {
            get
            {
                if (this.ID <= 8)
                    return (RectPoints)this.ID;
                else
                    return RectPoints.None;
            }
        }

        /// <summary>
        /// 空DrawToolDot
        /// </summary>
        public static DrawToolDot Empty
        {
            get
            {
                return new DrawToolDot(null, -1);
            }
        }

        public DrawToolDot(DrawToolDots parent , int id ,double x= 0 ,double y =0)
        {
            this.X = x;
            this.Y = y;
            this.Parent = parent;
            this.ID = id;
        }

        public static bool IsDotsIntersect(DrawToolDot d1,DrawToolDot d2)
        {
            double size = 1;
            return Math.Abs(d1.X - d2.X) <= size && Math.Abs(d1.Y - d2.Y) <= size;
        }

    }

    public enum RectPoints : int
    {
        None = -1,
        TopLeft = 1,
        Top = 2,
        TopRight = 3,
        Right = 4,
        BottomRight = 5,
        Bottom = 6,
        BottomLeft = 7,
        Left = 8,
        Center = 0
    }
}
