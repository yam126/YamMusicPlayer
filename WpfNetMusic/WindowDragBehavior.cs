using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors;

namespace WpfNetMusic
{
    /// <summary>
    /// 窗口拖拽类，首先引用nuget包Microsoft.Xaml.Behaviors.Wpf并安装，然后添加此类
    /// </summary>
    public class WindowDragBehavior : Behavior<Window>
    {
        protected override void OnAttached()
        {
            try
            {
                if (Mouse.LeftButton == MouseButtonState.Pressed)
                    AssociatedObject.MouseLeftButtonDown += (s, _) => ((Window)s).DragMove();
            }
            catch(Exception exp) 
            {

            }
        }
    }
}
