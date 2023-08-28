using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfNetMusic
{
    /// <summary>
    /// 错误消息页面
    /// </summary>
    public partial class ErrorMsgDialog : Window,INotifyPropertyChanged
    {
        /// <summary>
        /// 错误消息
        /// </summary>
        private string _ErrorMessage = string.Empty;

        /// <summary>
        /// 属性更改通知
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// 属性更改通知(此方法为了让绑定变量影响界面)
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMsg 
        { 
            get 
            { 
                return _ErrorMessage; 
            }
            set 
            { 
                _ErrorMessage = value;
                NotifyPropertyChanged("ErrorMsg");
            }
        }

        public ErrorMsgDialog()
        {
            InitializeComponent();
            this.DataContext= this;
        }

        
        /// <summary>
        /// 显示对话框
        /// </summary>
        /// <param name="message">错误消息</param>
        public bool? ShowDialog(string message) 
        {
            this.ErrorMsg= message;
            return this.ShowDialog();
        }

        private void WindowClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DialogResult = true;
            this.Visibility = Visibility.Hidden;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
