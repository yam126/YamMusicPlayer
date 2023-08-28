using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfNetMusic.Model
{
    /// <summary>
    /// 全局设置
    /// </summary>
    public class GlobalSetting
    {
        /// <summary>
        /// 音乐文件路径
        /// </summary>
        public string MusicFilePath { get; set; }

        /// <summary>
        /// 关闭设置
        /// </summary>
        public CloseSetting closeSetting { get; set; }
    }

    /// <summary>
    /// 关闭设置
    /// </summary>
    public class CloseSetting 
    {
        /// <summary>
        /// 最小化到任务栏
        /// </summary>
        public bool IsMinTaskBar { get; set; }

        /// <summary>
        /// 退出系统
        /// </summary>
        public bool QuitSystem { get; set; }
    }
}
