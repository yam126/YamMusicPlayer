

namespace DataAccess.Model
{
    /// <summary>
    /// MP3文件信息类
    /// </summary>
    public class MP3FileInfo
    {
        /// <summary>
        /// 文件编号
        /// </summary>
        public long FileId { get; set; }

        /// <summary>
        /// 歌单编号
        /// </summary>
        public long SongListId { get; set; }

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
        public string? reserved1 { get; set; }

        /// <summary>
        /// 保留位，一个字节
        /// </summary>
        public string? reserved2 { get; set; }

        /// <summary>
        /// 保留位，一个字节
        /// </summary>
        public string? reserved3 { get; set; }

        /// <summary>
        /// 文件全路径
        /// </summary>
        public string? FileFullPath { get; set; }

        /// <summary>
        /// 歌词文件
        /// </summary>
        public string? LyricFilePath { get; set; }
    }

    /// <summary>
    /// 歌单类
    /// </summary>
    [Serializable]
    public partial class SongList
    {
        /// <summary>
        /// 歌单编号
        /// </summary>
        public System.Int64? SongListId { get; set; }

        /// <summary>
        /// 歌单名称
        /// </summary>
        public System.String SongListName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime? CreateTime { get; set; }

        /// <summary>
        /// 是否默认歌单
        /// </summary>
        public System.Int32? IsDefault { get; set; }
    }
}