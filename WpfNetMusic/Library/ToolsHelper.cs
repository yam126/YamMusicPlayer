using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WpfNetMusic.Library
{
    public class ToolsHelper
    {
        /// <summary>
        /// 获取图片资源
        /// </summary>
        /// <param name="SourceFullPath">应用程序文件夹的路径</param>
        /// <param name="message">错误消息</param>
        /// <returns>图片资源</returns>
        public static BitmapImage GetImageSource(string SourceFullPath,out string message) 
        {
            BitmapImage result = null;
            message = string.Empty;
            try
            {             
                string path = System.IO.Directory.GetCurrentDirectory();
                DirectoryInfo directory2 = new DirectoryInfo(path);
                string FullPath = directory2.FullName + SourceFullPath;
                result = new BitmapImage(new Uri(FullPath, UriKind.Absolute));
            }
            catch(Exception exp) 
            {
                message = exp.Message;
                result = null;
            }
            return result;
        } 

        /// <summary>
        /// 获取操作系统所有逻辑磁盘盘符
        /// </summary>
        /// <returns>所有逻辑磁盘盘符</returns>
        public static List<string> GetAllOperationSystemLogicaldisk() 
        {
            SelectQuery selectQuery = new SelectQuery("select * from win32_logicaldisk");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(selectQuery);
            var drivers = searcher.Get();
            List<string> disks = new List<string>();//盘符列表
            foreach (var driver in drivers)
            {
                string name = driver["Name"].ToString();
                disks.Add(name);
            }
            return disks;
        }

        /// <summary>
        /// 将[时:分:秒]转换为秒钟
        /// </summary>
        /// <param name="durationStr"></param>
        /// <returns></returns>
        public static double ConvertDuration(string durationStr)
        {
            double result = 0;
            string[] durationAry = durationStr.Split(':');
            if (durationAry == null || durationAry.Length <= 0 || durationAry.Length < 3)
                return 0;
            double hourSecord = ToolsHelper.StrToDouble(durationAry[0]) * 3600;
            double minute = ToolsHelper.StrToDouble(durationAry[1]) * 60;
            double Second = ToolsHelper.StrToDouble(durationAry[2]);
            result = hourSecord + minute + Second;
            return result;
        }

        /// <summary>
        /// 将歌词时间转为[分:秒.毫秒]转换为秒钟
        /// </summary>
        /// <param name="durationStr"></param>
        /// <returns></returns>
        public static double ConvertDurationByLyric(string durationStr)
        {
            double result = 0;
            string[] durationAry = durationStr.Split(':');
            if (durationAry == null || durationAry.Length <= 0 || durationAry.Length < 2)
                return 0;
            string[] MinuteAndMillisecond = durationAry[1].Split(".");
            double minute = ToolsHelper.StrToDouble(durationAry[0]) * 60;
            double Second = ToolsHelper.StrToDouble(MinuteAndMillisecond[0]);
            double Millisecond = Math.Round(ToolsHelper.StrToDouble(MinuteAndMillisecond[1])/1000);
            result = minute + Second+Millisecond;
            return result;
        }

        /// <summary>
        /// 将秒钟转换为(时:分:秒)
        /// </summary>
        /// <param name="duration">秒钟</param>
        /// <returns>转换后的字符串</returns>
        public static string formatDuration(double duration)
        {
            int hour = Convert.ToInt32(decimal.Truncate(Convert.ToDecimal(duration / 3600)));
            int minute = Convert.ToInt32(decimal.Truncate(Convert.ToDecimal(duration % 3600 / 60)));
            int second = Convert.ToInt32(decimal.Truncate(Convert.ToDecimal(duration % 60)));
            return $"{hour.ToString().PadLeft(2, '0')}:{minute.ToString().PadLeft(2, '0')}:{second.ToString().PadLeft(2, '0')}";
        }

        /// <summary>
        /// 将string转换为decimal
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <returns>转换后的int类型结果</returns>
        public static double StrToDouble(string str)
        {
            double dt = 0;
            if (string.IsNullOrEmpty(str))
                return dt;
            if (!double.TryParse(str, out dt))
                return 0;
            return dt;
        }

        /// <summary>
        /// 文件大小格式化
        /// </summary>
        /// <param name="filesize">文件大小</param>
        /// <returns>格式化后的结果</returns>
        public static string GetFileSize(long filesize) 
        {
            if (filesize < 0)
                return "0";
            else if (filesize >= 1024 * 1024 * 1024)
                return string.Format("{0:0.00}GB", (double)filesize / (1024 * 1024 * 1024));
            else if (filesize >= 1024 * 1024)
                return string.Format("{0:0.00}MB", (double)filesize / (1024 * 1024));
            else if (filesize >= 1024)
                return string.Format("{0:0.00}KB", (double)filesize / 1024);
            else
                return string.Format("{0:0.00}bytes", filesize);
        }
    }
}
