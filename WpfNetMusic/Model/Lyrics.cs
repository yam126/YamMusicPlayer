using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfNetMusic.Model.Lyrics
{
    /// <summary>
    /// 一行歌词
    /// </summary>
    public class LyricsLine
    {
        /// <summary>
        /// 时间线
        /// </summary>
        public double TimeLine { get; set; }

        /// <summary>
        /// 歌词字符合集
        /// </summary>
        public List<LyricsChar> lyricsChars { get; set; }
    }

    /// <summary>
    /// 歌词单个字付
    /// </summary>
    public class LyricsChar
    {
        /// <summary>
        /// 单个字符
        /// </summary>
        public string Char { get; set; }

        /// <summary>
        /// 字体颜色
        /// </summary>
        public string Color { get; set; }
    }
}
