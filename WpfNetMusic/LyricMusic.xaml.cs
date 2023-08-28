using DataAccess.DAL;
using DataAccess.Model;
using IWshRuntimeLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfNetMusic.Library;
using WpfNetMusic.Model.Lyrics;

namespace WpfNetMusic
{
    /// <summary>
    /// 歌词窗口的交互逻辑
    /// </summary>
    public partial class LyricMusic : Window, INotifyPropertyChanged
    {
        #region 属性

        /// <summary>
        /// 歌词数据
        /// </summary>
        private List<LyricsLine> _lyricsLines = new List<LyricsLine>();

        /// <summary>
        /// 当前播放时长
        /// </summary>
        private double _CurrentPlayDuration;

        /// <summary>
        /// 当前是第几行歌词
        /// </summary>
        private int _CurrentLineIndex = 0;

        /// <summary>
        /// 当前歌词
        /// </summary>
        private LyricsLine CurrentLyrics = null;

        /// <summary>
        /// 歌词绑定数据
        /// </summary>
        private ObservableCollection<dynamic> _lyrics = new ObservableCollection<dynamic>();

        /// <summary>
        /// 属性更改事件
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion

        /// <summary>
        /// 属性更改通知(此方法为了让绑定变量影响界面)
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #region 属性过程

        /// <summary>
        /// 当前歌词属于的MP3文件
        /// </summary>
        public MP3FileInfo MP3File { get; set; }

        /// <summary>
        /// 歌词绑定数据属性过程
        /// </summary>
        public ObservableCollection<dynamic> Lyrics
        {
            get { return _lyrics; }
            set { _lyrics = value; }
        }

        /// <summary>
        /// 所有歌词数据
        /// </summary>
        public List<LyricsLine> lyricsLines
        {
            get { return _lyricsLines; }
            set { _lyricsLines = value; }
        }

        /// <summary>
        /// 当前播放时长
        /// </summary>
        public double CurrentPlayDuration
        {
            get { return _CurrentPlayDuration; }
            set { _CurrentPlayDuration = value; }
        }
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public LyricMusic(List<LyricsLine> lyricsLines)
        {
            InitializeComponent();
            this.lyricsLines = lyricsLines;
            ShowLyrics();
            this.DataContext = this;
        }

        /// <summary>
        /// 显示歌词代码
        /// </summary>
        public void ShowLyrics()
        {
            if (_lyricsLines != null)
            {
                _lyrics = new ObservableCollection<dynamic>();
                GetCurrentLyrics();
                if (CurrentLyrics == null)
                    return;
                CurrentLyrics.lyricsChars.ForEach(lyricsChar =>
                {
                    _lyrics.Add(lyricsChar);
                });
                NotifyPropertyChanged("Lyrics");
            }
        }

        /// <summary>
        /// 获取当前播放的歌词行
        /// </summary>
        /// <returns>歌词行</returns>
        private void GetCurrentLyrics()
        {
            #region 根据当前播放进度找出属于那句歌词

            //如果当前播放进度为0，则默认当前歌词为第一句
            if (CurrentPlayDuration == 0)
                CurrentLyrics = _lyricsLines.First();
            //从所有歌词中找出属于当前进度的歌词(歌词出现时间大于等于当前进度开始时间并且小于等于当前进度结束时间)
            else if (_lyricsLines.Any(q => CurrentPlayDuration >= q.TimeLine&& CurrentPlayDuration <= q.TimeLine))
                CurrentLyrics = _lyricsLines.First(q => CurrentPlayDuration >= q.TimeLine && CurrentPlayDuration <= q.TimeLine);
            _CurrentLineIndex = _lyricsLines.IndexOf(CurrentLyrics);//返回当前歌词所在第几句
            #endregion

            //取得上一句歌词索引
            int PreviousLineIndex = _CurrentLineIndex - 1 <= 0 ? 0 : _CurrentLineIndex;

            //找出上一句歌词的行
            var PreviousLine = _lyricsLines[PreviousLineIndex];

            //找出下一句歌词的行号
            var NextLineIndex = (_CurrentLineIndex + 1) >= (_lyricsLines.Count - 1) ? _lyricsLines.Count - 1 : _CurrentLineIndex + 1;
            var NextLine = _lyricsLines[NextLineIndex];

            //计算出当前歌词的持续时长(下一句歌词的出现时间-当前歌词的出现时间等于当前歌词持续时间)
            var CurrentTimeLine = NextLine.TimeLine - CurrentLyrics.TimeLine;

            //计算出当前歌词每个字的播放时长(持续时间/当前行歌词字数)
            var CharTimeStep = Math.Round(CurrentTimeLine / CurrentLyrics.lyricsChars.Count);

            //当前歌词的开始时间位置
            var StartTimeLine = CurrentLyrics.TimeLine;

            #region 循环每个字找出当前唱的哪个字标红
            for (var i = 0; i < CurrentLyrics.lyricsChars.Count; i++)
            {
                //当前字的出现时间位置=开始时间+每个歌词的播放时长
                var CharTime = Math.Round(StartTimeLine + CharTimeStep);

                //当前进如果小于等于当前字出现时间的位置则标红,证明唱过了
                //否则还是白色
                if (CharTime <= _CurrentPlayDuration && _CurrentPlayDuration != 0)
                    CurrentLyrics.lyricsChars[i].Color = "Red";
                else
                    CurrentLyrics.lyricsChars[i].Color = "White";

                StartTimeLine = Math.Round(StartTimeLine + CharTimeStep);
            }
            #endregion
        }

        /// <summary>
        /// 窗口移动事件
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

        //窗口关闭事件
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// 打开歌词文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            //打开歌词文件
            System.Windows.Forms.OpenFileDialog fileDialog = new System.Windows.Forms.OpenFileDialog();
            
            //歌词帮助类
            LyricHelper lyricHelper = null;

            //错误消息
            string message = string.Empty;

            //歌词默认字体颜色
            string defaultColor = "White";

            #region 初始化歌词
            fileDialog.Filter = "ylrc|*.ylrc";
            fileDialog.Title = "打开歌词文件";
            System.Windows.Forms.DialogResult dialogResult = fileDialog.ShowDialog();
            #endregion

            if(!string.IsNullOrEmpty(fileDialog.FileName)&&
                System.IO.File.Exists(fileDialog.FileName)&&
                dialogResult== System.Windows.Forms.DialogResult.OK) 
            {
                #region 分析并读取歌词文件
                AnalyseLyricAndShow(fileDialog.FileName, defaultColor, ref lyricHelper, out message);
                if (!string.IsNullOrEmpty(message))
                {
                    System.Windows.MessageBox.Show(
                        $"读取歌词[{fileDialog.FileName}]出错\n原因[{message}]",
                        "错误",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    return;
                }
                #endregion

                #region 保存歌词文件
                MP3FileInfo_DAL dal = new MP3FileInfo_DAL(Global.SqliteDbFullPath);
                this.MP3File.LyricFilePath = fileDialog.FileName;
                int state = dal.Update(
                    new List<MP3FileInfo>() { this.MP3File },
                    $" FileId='{this.MP3File.FileId}' ",
                    out message
                    );
                if (state != 0)
                {
                    System.Windows.MessageBox.Show(
                        $"保存歌词[{fileDialog.FileName}]出错\n原因[{message}]",
                        "错误",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    return;
                }
                #endregion

                this.lyricsLines = lyricHelper.LyricsLines;
                ShowLyrics();
            }
        }

        /// <summary>
        /// 分析歌词文件
        /// </summary>
        /// <param name="LyricFileFullPath">歌词文件全路径</param>
        /// <param name="defaultColor">默认颜色</param>
        /// <param name="lyric">歌词帮助类</param>
        /// <param name="message">错误消息</param>
        private void AnalyseLyricAndShow(
            string LyricFileFullPath,
            string defaultColor,
            ref LyricHelper lyric,
            out string message
            )
        {
            lyric = new LyricHelper(LyricFileFullPath, defaultColor, Encoding.UTF8, out message);
            if (!string.IsNullOrEmpty(message))
            {
                System.Windows.MessageBox.Show(
                $"读取歌词[{LyricFileFullPath}]出错\n原因[{message}]",
                "错误",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}
