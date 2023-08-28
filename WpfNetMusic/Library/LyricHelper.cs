using HandyControl.Tools.Extension;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Shapes;
using WpfNetMusic.Model.Lyrics;

namespace WpfNetMusic.Library
{
    /// <summary>
    /// 歌词帮助类
    /// </summary>
    public class LyricHelper
    {
        /// <summary>
        /// 歌词行
        /// </summary>
        private List<LyricsLine> _LyricsLines=new List<LyricsLine>();

        /// <summary>
        /// 歌词数据
        /// </summary>
        public List<LyricsLine> LyricsLines 
        {
            get 
            {
                return _LyricsLines;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="FileFullPath">文件全路径</param>
        /// <param name="eCode">文件编码</param>
        /// <param name="Color">默认颜色</param>
        public LyricHelper(string FileFullPath,string Color,Encoding eCode,out string message)
        {
            StreamReader sr = null;
            message = string.Empty;
            if (!File.Exists(FileFullPath))
            {
                message = "歌词文件不存在";
                return;
            }
            try
            {
                //读取歌词文件
                sr = new StreamReader(FileFullPath, eCode);
                string TempLine = "";
                #region 循环读取行
                while (TempLine!=null)
                {
                    TempLine = sr.ReadLine();
                    if(!string.IsNullOrEmpty(TempLine)) 
                    {
                        #region 声明和初始化

                        //歌词行
                        LyricsLine lyricsLine = new LyricsLine();

                        //每行字符
                        char[] lyricsChars = null;

                        //获取歌词时间
                        string time = GetTime(TempLine);

                        //分离出歌词
                        TempLine = TempLine.Replace($"[{time}]", "");

                        //将时间转换为秒钟
                        lyricsLine.TimeLine= ToolsHelper.ConvertDurationByLyric(time);

                        //歌词转为字符数组
                        lyricsChars = TempLine.ToCharArray();
                        #endregion

                        #region 循环拆分歌词的每个字
                        lyricsLine.lyricsChars = new List<LyricsChar>();
                        foreach(char lyricsChar in lyricsChars) 
                        {
                            lyricsLine.lyricsChars.Add(new LyricsChar()
                            {
                                Color = Color,//歌词默认颜色
                                Char = lyricsChar.ToString()//歌词单个字符
                            });
                        }
                        _LyricsLines.Add(lyricsLine);
                        #endregion
                    }
                }
                #endregion
            }
            catch (Exception ex) 
            {
                message = ex.Message;
            }
        }

        /// <summary>
        /// 正则表达式获取歌词时间
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string GetTime(string str)
        {
            Regex reg = new Regex(@"\[(?<time>.*)\]", RegexOptions.IgnoreCase);
            return reg.Match(str).Groups["time"].Value;
        }
    }
}
