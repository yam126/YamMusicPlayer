using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfNetMusic.Enum
{
    /// <summary>
    /// 播放顺序
    /// </summary>
    public enum PlaySequence
    {
        /// <summary>
        /// 顺序循环播放
        /// </summary>
        SequenceCyclic=0,

        /// <summary>
        /// 顺序只播放1次
        /// </summary>
        SequenceOne=1,

        /// <summary>
        /// 随机播放
        /// </summary>
        RandomPlay=2,

        /// <summary>
        /// 单曲循环
        /// </summary>
        SingleLoop=3
    }
}
