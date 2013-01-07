using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace FishingPoint.Controls
{
    public partial class PushPinCursorControl : UserControl
    {
        public PushPinCursorControl()
        {
            InitializeComponent();
        }

        public void MoveTo(Point point)
        {
            this.Visibility = Visibility.Visible;
            

            double cursorX = point.X - 20; //-Stand.ActualWidth / 2;
            double cursorY = point.Y - 90;// -Trunk.ActualHeight;
            this.SetValue(Canvas.LeftProperty, cursorX);
            this.SetValue(Canvas.TopProperty, cursorY);
        }

        public void Hide()
        {
            this.Visibility = Visibility.Collapsed;
        }
    }


}
