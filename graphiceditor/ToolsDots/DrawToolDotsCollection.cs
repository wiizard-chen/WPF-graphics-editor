using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace graphiceditor.ToolsDots
{
   public  class DrawToolDotsCollection :ObservableCollection<DrawToolDot>
    {
        /// <summary>
        /// 获取相应位置的点
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public DrawToolDot  this[RectPoints pt]
        {
            get
            {
                return this[(int)pt];
            }
        }

        public new DrawToolDot this[int ID]
        {
            get
            {
                return this.Where(p => p.ID == ID).DefaultIfEmpty(DrawToolDot.Empty).First();
            }
        }

        /// <summary>
        /// 在集合中添加点。每一个新的点DotID增加
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="points"></param>
        public void  AddPoints(DrawToolDots parent , IEnumerable<Point> points)
        {
            int max = this.DefaultIfEmpty(DrawToolDot.Empty).Max(p => p.ID) + 1;

            foreach (Point  p in points)
            {
                base.Add(new DrawToolDot(parent, max, p.X, p.Y));
                max++;
            }
        }
    }
}
