using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;

namespace WpfNetMusic.Model
{
    /// <summary>
    /// MP3文件信息类
    /// </summary>
    public class Mp3Info
    {

        /// <summary>
        /// TAG，三个字节
        /// </summary>
        public string identify { get; set; }
        
        /// <summary>
        /// 文件编号
        /// </summary>
        public long FileId { get; set; }

        /// <summary>
        /// MP3文件名
        /// </summary>
        public string? FileName { get; set; }

        /// <summary>
        /// MP3文件大小
        /// </summary>
        public double FileSize { get; set; }

        /// <summary>
        /// 封面 
        /// </summary>
        public string? Cover { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// //歌曲名,30个字节
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// 播放时长
        /// </summary>
        public double duration { get; set; }

        /// <summary>
        /// 歌手名,30个字节
        /// </summary>
        public string? Artist { get; set; }   //歌手名,30个字节

        /// <summary>
        /// 所属唱片,30个字节
        /// </summary>
        public string? Album { get; set; }

        /// <summary>
        /// 年,4个字符
        /// </summary>
        public string? Year { get; set; }

        /// <summary>
        /// 注释,28个字节
        /// </summary>
        public string? Comment { get; set; }

        /// <summary>
        /// 保留位，一个字节
        /// </summary>
        public char reserved1 { get; set; }

        /// <summary>
        /// 保留位，一个字节
        /// </summary>
        public char reserved2 { get; set; }

        /// <summary>
        /// 保留位，一个字节
        /// </summary>
        public char reserved3 { get; set; }
    }
}
