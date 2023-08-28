using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfNetMusic.Model
{
    /// <summary>
    /// 歌单列表
    /// </summary>
    public class SongList
    {
        /// <summary>
        /// 歌单编号
        /// </summary>
        public long SongListId { get; set; }

        /// <summary>
        /// 歌单名称
        /// </summary>
        public string? SongListName { get; set; }

        /// <summary>
        /// 是否默认歌单
        /// </summary>
        public bool IsDefault { get; set; }
    }
}
