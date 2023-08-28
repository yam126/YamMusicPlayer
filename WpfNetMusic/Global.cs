using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfNetMusic.Model;

namespace WpfNetMusic
{
    /// <summary>
    /// 全局变量
    /// </summary>
    public class Global
    {
        /// <summary>
        /// App路径
        /// </summary>
        public static string AppPath = Directory.GetCurrentDirectory();

        /// <summary>
        /// Sqlite数据库路径
        /// </summary>
        public static string SqliteDbFullPath = $@"{Directory.GetCurrentDirectory()}\WpfNetMusicDB.db";
        
        /// <summary>
        /// 主窗体
        /// </summary>
        public static MainWindow Main { get; set; }

        /// <summary>
        /// 关闭选项对话框
        /// </summary>
        public static CloseAlert closeAlert { get; set; }

        /// <summary>
        /// 全局设置
        /// </summary>
        public static GlobalSetting setting { get; set; }

        /// <summary>
        /// 歌词信息
        /// </summary>
        public static LyricMusic lyric { get; set; }

        /// <summary>
        /// 关于程序
        /// </summary>
        public static About about { get; set; }

        /// <summary>
        /// 使用帮助
        /// </summary>
        public static HelpApp help { get; set; }

        /// <summary>
        /// 错误窗口
        /// </summary>
        public static ErrorMsgDialog errorMsgDialog { get; set; }
    }
}
