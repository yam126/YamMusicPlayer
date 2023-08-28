using System;
using System.Collections.Generic;
using System.Linq;
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
using WpfNetMusic.Library;
using WpfNetMusic.Model;

namespace WpfNetMusic
{
    /// <summary>
    /// CloseAlert.xaml 的交互逻辑
    /// </summary>
    public partial class CloseAlert : Window
    {
        public CloseAlert()
        {
            InitializeComponent();

            //WPF双向绑定的关键将
            //将当前窗体绑定到数据上下文之后就能将变量绑定到界面
            //注意:此句代码一定是在InitializeComponent之后执行
            this.DataContext = this;
        }

        /// <summary>
        /// 最小化到任务栏
        /// </summary>
        public bool IsMinTaskBar { get; set; }

        /// <summary>
        /// 退出系统
        /// </summary>
        public bool QuitSystem { get; set; }

        /// <summary>
        /// 是否保存
        /// </summary>
        public bool IsSave { get; set; }

        /// <summary>
        /// 窗口关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_Window(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// 取消按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// 确定按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            string message = string.Empty;
            this.DialogResult = true;
            if (Global.setting == null)
                Global.setting = new GlobalSetting();
            if (Global.setting.closeSetting == null)
                Global.setting.closeSetting = new CloseSetting();
            Global.setting.closeSetting.IsMinTaskBar = IsMinTaskBar;
            Global.setting.closeSetting.QuitSystem= QuitSystem;
            if (IsSave)
            {
                if(!SettingHelper.SaveSetting(Global.setting, out message))
                {
                    if (!string.IsNullOrEmpty(message)) 
                    {
                        MessageBox.Show(
                            $"保存配置文件出错，原因{message}",
                            "错误",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
                    }
                }
            }
            this.Close();
        }

        /// <summary>
        /// 圆角窗口边框必加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        /// <summary>
        /// 加载完成代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(Global.setting.closeSetting!=null) 
            {
                var closeSetting=Global.setting.closeSetting;
                IsMinTaskBar=closeSetting.IsMinTaskBar;
                QuitSystem=closeSetting.QuitSystem;
            }
        }

        private void RadioButton_Exit_Click(object sender, RoutedEventArgs e)
        {
            QuitSystem = true;
            IsMinTaskBar = false;
        }

        private void RadioButton_TaskBar_Click(object sender, RoutedEventArgs e)
        {
            QuitSystem = false;
            IsMinTaskBar = true;
        }
    }
}
