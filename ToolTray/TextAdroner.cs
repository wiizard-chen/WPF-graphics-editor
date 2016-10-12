using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;
using System.ComponentModel;
using System.Diagnostics;

namespace ToolTray
{
    public class TextAdroner : Adorner, INotifyPropertyChanged
    {
        #region 接口实现
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyname)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
        }
        #endregion

        #region 事件实现
        /// <summary>
        /// 位置改变
        /// </summary>
        public event EventHandler ElementPositionChanged;
        /// <summary>
        /// 大小改变
        /// </summary>
        public event EventHandler ElementSizeChanged;
        #endregion

        #region 业务属性
        private const double THUMB_SIZE = 12;
        private const double MINIMAL_SIZE = 20;
        private const double MOVE_OFFSET = 8;//20;
        private Thumb tl, tr, bl, br;
        private Thumb mov;
        private VisualCollection visCollec;
        /// <summary>
        /// 内部控件
        /// </summary>
        public FrameworkElement Element
        {
            get
            {
                return AdornedElement as FrameworkElement;
            }
        }
        #endregion

        public TextAdroner(UIElement adorned)
            : base(adorned)
        {
            //visCollec = new VisualCollection(this);
            //visCollec.Add(tl = GetResizeThumb(Cursors.SizeNWSE, HorizontalAlignment.Left, VerticalAlignment.Top));
            //visCollec.Add(tr = GetResizeThumb(Cursors.SizeNESW, HorizontalAlignment.Right, VerticalAlignment.Top));
            //visCollec.Add(bl = GetResizeThumb(Cursors.SizeNESW, HorizontalAlignment.Left, VerticalAlignment.Bottom));
            //visCollec.Add(br = GetResizeThumb(Cursors.SizeNWSE, HorizontalAlignment.Right, VerticalAlignment.Bottom));
            //visCollec.Add(mov = GetMoveThumb());
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            double offset = THUMB_SIZE / 2;
            Size sz = new Size(THUMB_SIZE, THUMB_SIZE);
            tl.Arrange(new Rect(new Point(-offset, -offset), sz));
            tr.Arrange(new Rect(new Point(AdornedElement.RenderSize.Width - offset, -offset), sz));
            bl.Arrange(new Rect(new Point(-offset, AdornedElement.RenderSize.Height - offset), sz));
            br.Arrange(new Rect(new Point(AdornedElement.RenderSize.Width - offset, AdornedElement.RenderSize.Height - offset), sz));
            //mov.Arrange(new Rect(new Point(AdornedElement.RenderSize.Width / 2 - THUMB_SIZE / 2, -MOVE_OFFSET), sz));
            mov.Arrange(new Rect(new Point(AdornedElement.RenderSize.Width / 2 - offset, AdornedElement.RenderSize.Height / 2 - offset), sz));
            //Debug.WriteLine(AdornedElement.RenderSize.Width);
            //Debug.WriteLine(AdornedElement.RenderSize.Height);
            return finalSize;
        }
    }
}