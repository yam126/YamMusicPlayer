using DataAccess.DAL;
using DataAccess.Model;
using Microsoft.Win32;
using Snowflake.Net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfNetMusic.Enum;
using WpfNetMusic.Library;
using WpfNetMusic.Library.clsMCIPlay;
using WpfNetMusic.Model;

namespace WpfNetMusic
{
    /// <summary>
    /// 程序主界面
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region 属性

        /// <summary>
        /// 最小化到任务栏的通知图标
        /// </summary>
        private System.Windows.Forms.NotifyIcon notifyIcon;

        /// <summary>
        /// 歌单列表
        /// </summary>
        private ObservableCollection<dynamic> songLists;

        /// <summary>
        /// 歌曲列表
        /// </summary>
        private ObservableCollection<dynamic> musicLists;

        /// <summary>
        /// 音乐数据
        /// </summary>
        private List<DataAccess.Model.MP3FileInfo> musicDatas;

        /// <summary>
        /// 当前选中的歌曲
        /// </summary>
        private MP3FileInfo selectedMusic = null;

        /// <summary>
        /// 当前播放状态
        /// </summary>
        private string CurrentPlayStatus = "Stop";

        /// <summary>
        /// App路径
        /// </summary>
        private string AppPath = Directory.GetCurrentDirectory();

        /// <summary>
        /// 播放进度显示
        /// </summary>
        private System.Timers.Timer playTimer = null;

        /// <summary>
        /// 定时器间隔时间(毫秒)
        /// </summary>
        private double TimeInterval = 1000;

        /// <summary>
        /// 进度条滑块是否处于拖拽状态
        /// </summary>
        private bool IsSliderDragStarted = false;

        /// <summary>
        /// 选中的歌单
        /// </summary>
        private Model.SongList SelectedSongList = null;

        /// <summary>
        /// 唱针默认角度
        /// </summary>
        private double _StylusAngle = -35;

        /// <summary>
        /// 唱片旋转动画
        /// </summary>
        private Storyboard discStoryboard = null;

        /// <summary>
        /// 当前播放顺序
        /// </summary>
        private PlaySequence CurrentPlaySequence = PlaySequence.SequenceCyclic;

        /// <summary>
        /// 拖拽中的音乐文件
        /// </summary>
        private MP3FileInfo? dragMp3FileInfo = null;

        /// <summary>
        /// 是否移动中
        /// </summary>
        private bool IsMusicMovePopup = false;

        /// <summary>
        /// 默认歌单编号
        /// </summary>
        private long defaultSontListId = 0;

        /// <summary>
        /// 属性更改通知界面
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            NotifyButton();
            this.InitSongList();
            btnMainMenu.ContextMenu = MarkMainMenu(true);
            this.DataContext = this;
            #region 初始化播放定时器
            this.playTimer = new System.Timers.Timer();
            this.playTimer.Interval = TimeInterval;
            this.playTimer.Elapsed += PlayTimer_Elapsed;
            #endregion
            this.InitVolume();
            this.discStoryboard = Resources["DiscAnimation"] as Storyboard;
        }

        /// <summary>
        /// 播放中实时进度条显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayTimer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            playTimer.Stop();
            Dispatcher.Invoke(
            new Action(
                delegate
                {
                    //获取当前播放进度
                    double CurrentPosition = Math.Round(this.mediaPlayer.Position.TotalSeconds);
                    //Debug.WriteLine($"CurrentPosition={CurrentPosition}");
                    //Debug.WriteLine($"duration={this.selectedMusic.duration}");

                    //实时显示播放进度
                    this.CurrentPlayTime.Text = ToolsHelper.formatDuration(CurrentPosition);

                    //将播放进度赋值给进度条
                    this.CurrentMusicSlider.Value = CurrentPosition;

                    if (Global.lyric != null && Global.lyric.Visibility == Visibility.Visible)
                    {
                        Global.lyric.CurrentPlayDuration = CurrentPosition;
                        Global.lyric.MP3File = this.selectedMusic;
                        Global.lyric.ShowLyrics();
                    }

                    //如果当前播放进度小于当前歌曲的总时长,则继续，否则停止播放
                    if (CurrentPosition < this.selectedMusic.duration)
                        playTimer.Start();
                    else
                    {
                        this.CurrentMusicSlider.Value = CurrentPosition;
                        this.CurrentPlayStatus = "Stop";
                        this.ChangePlayStatus();
                        this.ExecutePlaySequence();
                    }
                }
              )
            );
        }

        #region 属性过程
        /// <summary>
        /// 歌曲列表
        /// </summary>
        public ObservableCollection<dynamic> MusicList
        {
            get
            {
                return musicLists;
            }
            set
            {
                musicLists = value;
                NotifyPropertyChanged("MusicList");
            }
        }

        /// <summary>
        /// 歌单列表
        /// </summary>
        public ObservableCollection<dynamic> SongLists
        {
            get
            {
                return songLists;
            }
        }

        /// <summary>
        /// 唱针角度
        /// </summary>
        public double StylusAngle
        {
            get
            {
                return _StylusAngle;
            }
            set
            {
                _StylusAngle = value;
                NotifyPropertyChanged("StylusAngle");
            }
        }

        /// <summary>
        /// 当前选中的歌曲
        /// </summary>
        public MP3FileInfo CurrentMusic
        {
            get
            {
                return selectedMusic;
            }
        }
        #endregion

        #region 公开方法
        /// <summary>
        /// 初始化歌单列表
        /// </summary>
        public void InitSongList()
        {
            #region 声明变量

            //错误消息
            string message = string.Empty;

            //歌单列表
            List<DataAccess.Model.SongList> songlist = new List<DataAccess.Model.SongList>();

            //数据库读取
            SongList_DAL dbDal = null;

            //默认歌单
            DataAccess.Model.SongList defaultSontList = new DataAccess.Model.SongList();
            #endregion

            #region 数据库读取数据
            dbDal = new SongList_DAL(Global.SqliteDbFullPath);
            songlist = dbDal.Query("", out message);
            if (songlist == null || songlist.Count <= 0)
                return;
            #endregion

            #region 取默认歌单
            if (this.SelectedSongList == null)
            {
                if (songlist.Any(q => q.IsDefault == 1))
                    defaultSontList = songlist.First(q => q.IsDefault == 1);
                else
                    defaultSontList = songlist.First();
                this.SelectedSongList = new Model.SongList()
                {
                    SongListId = defaultSontList.SongListId.GetValueOrDefault(),
                    SongListName = defaultSontList.SongListName,
                    IsDefault = defaultSontList.IsDefault.GetValueOrDefault() == 1 ? true : false,
                };
            }
            else if (this.SelectedSongList.SongListId != 0 && songlist.Any(q => q.SongListId == this.SelectedSongList.SongListId))
                defaultSontList = songlist.First(q => q.SongListId == this.SelectedSongList.SongListId);
            else
                defaultSontList = songlist.First();
            defaultSontListId = defaultSontList.SongListId.GetValueOrDefault();
            #endregion

            #region 将歌单赋值给界面
            if (songLists == null)
                songLists = new ObservableCollection<dynamic>();
            songLists.Clear();
            songlist.ForEach(item =>
                         {
                             songLists.Add(item);
                         });
            this.CurrentSongList.Text = $"当前歌单名称:{defaultSontList.SongListName}";
            this.lbSongList.ItemsSource = songLists;
            if (this.SelectedSongList == null)
                this.lbSongList.SelectedIndex = songlist.FindIndex(q => q.IsDefault == 1);
            else
                this.lbSongList.SelectedIndex = songlist.FindIndex(q => q.SongListId == defaultSontListId);
            #endregion

            this.InitMusicList(defaultSontListId);
        }

        /// <summary>
        /// 初始化音乐数据
        /// </summary>
        /// <param name="SongListId">歌单编号</param>
        public void InitMusicList(long SongListId)
        {
            #region 声明变量

            //错误消息
            string message = string.Empty;

            //mp3音乐列表
            List<MP3FileInfo> mp3List = new List<MP3FileInfo>();

            //数据库读取
            MP3FileInfo_DAL dbDal = null;
            #endregion

            dbDal = new MP3FileInfo_DAL(Global.SqliteDbFullPath);
            string SqlWhere = GetSearchMp3FileInfoStr(txtSearch.Text);
            if (SqlWhere != string.Empty)
                SqlWhere = " and " + SqlWhere;
            mp3List = dbDal.Query($@" SongListId='{SongListId}' {SqlWhere} ", out message);
            if (mp3List == null || mp3List.Count <= 0)
            {
                MessageBox.Show("没有任何音乐文件", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                dg_MusicList.Visibility = Visibility.Hidden;
                NoFile.Visibility = Visibility.Visible;
                return;
            }
            dg_MusicList.Visibility = Visibility.Visible;
            NoFile.Visibility = Visibility.Hidden;
            if (musicLists == null)
                musicLists = new ObservableCollection<dynamic>();
            musicLists.Clear();
            musicDatas = mp3List;
            mp3List.ForEach(item =>
                 {
                     musicLists.Add(new
                     {
                         SongListId = item.SongListId,
                         FileId = item.FileId,
                         Index = mp3List.IndexOf(item) + 1,
                         Title = item.Title,
                         Artist = item.Artist,
                         Album = item.Album,
                         CreateTime = item.CreateTime.ToString("yyyy年MM月dd日HH时mm分ss秒")
                     });
                 });
            this.MusicList = musicLists;
            this.dg_MusicList.ItemsSource = musicLists;
        }

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
        #endregion

        #region 搜索框提示事件

        private void txtSearch_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                txtSearchHit.Visibility = Visibility.Hidden;
            }
        }

        private void txtSearch_MouseLeave(object sender, MouseEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                txtSearchHit.Visibility = Visibility.Visible;
            }
        }

        private void txtSearch_MouseEnter(object sender, MouseEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                txtSearchHit.Visibility = Visibility.Hidden;
            }
        }

        #endregion

        #region 事件代码
        /// <summary>
        /// 窗口最小化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MinIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// 最大化按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MaxIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MaxWindowClick();
        }

        /// <summary>
        /// 关闭按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_MouseDown(object sender, MouseButtonEventArgs e)
        {
            bool DialogResult = false;
            Global.closeAlert = new CloseAlert();
            if (!SettingHelper.IsHaveSettingFile())
                DialogResult = Global.closeAlert.ShowDialog().GetValueOrDefault();
            else
                DialogResult = true;
            if (DialogResult == true)
            {
                if (Global.setting.closeSetting != null)
                {
                    bool IsMinTaskBar = Global.setting.closeSetting.IsMinTaskBar;
                    bool QuitSystem = Global.setting.closeSetting.QuitSystem;
                    if (IsMinTaskBar)
                        this.Visibility = Visibility.Hidden;
                    else if (QuitSystem)
                        Application.Current.Shutdown();
                    else if (!IsMinTaskBar && !QuitSystem)
                        Global.closeAlert.ShowDialog().GetValueOrDefault();
                }
            }
        }

        /// <summary>
        /// 任务栏菜单显示主界面点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowWindow(object sender, EventArgs e)
        {
            Visibility = Visibility.Visible;
        }

        /// <summary>
        /// 任务栏菜单退出程序点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitApp(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// 窗口加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string message = string.Empty;
            if (SettingHelper.LoadingSetting(out message))
            {
                if (!string.IsNullOrEmpty(message))
                    MessageBox.Show($"读取配置文件出错,原因[{message}]", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// 窗口鼠标拖拽事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && IsMusicMovePopup == false)
            {
                DragMove();
            }
        }

        /// <summary>
        /// 窗口状态改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_StateChanged(object sender, EventArgs e)
        {
            BitmapImage image = null;
            string message = string.Empty;
            if (this.WindowState == WindowState.Maximized)
                image = Library.ToolsHelper.GetImageSource(@"\Image\NormalIcon-01.png", out message);
            else
                image = Library.ToolsHelper.GetImageSource(@"\Image\MaxIcon-01.png", out message);
            if (image != null)
                this.NormalMaxBtn.Source = image;
        }

        /// <summary>
        /// 歌单列表鼠标滚轮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBox_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (!e.Handled)
            {
                // ListView拦截鼠标滚轮事件
                e.Handled = true;

                // 激发一个鼠标滚轮事件，冒泡给外层ListView接收到
                var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
                eventArg.RoutedEvent = UIElement.MouseWheelEvent;
                eventArg.Source = sender;
                var parent = SongListScrollView;
                parent.RaiseEvent(eventArg);
            }
        }

        /// <summary>
        /// 音乐列表双击播放事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dg_MusicList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.MainPanel.Visibility = Visibility.Hidden;
            this.PlayerPanel.Visibility = Visibility.Visible;
            this.SetMusicInfoToUI();
        }

        /// <summary>
        /// 音乐列表单条音乐选中事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dg_MusicList_Selected(object sender, RoutedEventArgs e)
        {
            int SelectedIndex = this.dg_MusicList.SelectedIndex;
            this.selectedMusic = this.musicDatas[SelectedIndex];
            this.tbSongTitle.Text = this.selectedMusic.Title;
        }

        /// <summary>
        /// 返回按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.MainPanel.Visibility = Visibility.Visible;
            this.PlayerPanel.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// 窗口最大化按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount > 1)
            {
                MaxWindowClick();
            }
        }

        /// <summary>
        /// 播放按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.ChangePlayStatus();
        }

        /// <summary>
        /// 播放按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayButtonImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount > 1)
            {
                if (this.CurrentPlayStatus == "Stop")
                    this.CurrentPlayStatus = "Play";
                else
                    this.CurrentPlayStatus = "Stop";
                this.ChangePlayStatus();
            }
        }

        /// <summary>
        /// 进度滑块值改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CurrentMusicSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //如果当前没有选中歌曲则返回
            if (this.selectedMusic == null)
                return;
            double Position = e.NewValue;
            this.CurrentPlayTime.Text = ToolsHelper.formatDuration(Position);

        }

        /// <summary>
        /// 滑块拖动开始事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CurrentMusicSlider_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            this.IsSliderDragStarted = true;//打标记表示滑块已经开始拖动

            #region 拖动时停止播放
            this.CurrentPlayStatus = "Stop";
            this.ChangePlayStatus();
            #endregion

            //Debug.WriteLine("DragStarted");
        }

        /// <summary>
        /// 滑块拖动结束事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CurrentMusicSlider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            //打标记滑块拖动结束
            this.IsSliderDragStarted = false;

            //获得当前进度条的进度值
            double Position = this.CurrentMusicSlider.Value;

            //设置当前播放进度
            this.mediaPlayer.Position = TimeSpan.FromSeconds(Position);

            #region 设置当前播放状态为播放
            this.CurrentPlayStatus = "Play";
            this.ChangePlayStatus();
            #endregion
            //Debug.WriteLine("DragCompleted");
        }

        /// <summary>
        /// 上一首
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrevoiusBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.PreviousMusic();
        }

        /// <summary>
        /// 下一首歌曲
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.NextMusic();
        }

        /// <summary>
        /// 播放顺序改变按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPlaySequence_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.ChangePlaySequence();
        }

        /// <summary>
        /// 音量调节滑块滑动结束事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SliderVolume_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            double VolumeValue = SliderVolume.Value;
            if (VolumeValue <= 0)
            {
                VolumeIcon.Source = new BitmapImage(new Uri($@"{AppPath}\Image\MuteIcon.png", UriKind.Absolute));
                SystemVolume.SetMasterVolumeMute(true);

            }
            else
            {
                VolumeIcon.Source = new BitmapImage(new Uri($@"{AppPath}\Image\VolumeIcon.png", UriKind.Absolute));
                SystemVolume.SetMasterVolumeMute(false);
                SystemVolume.SetMasterVolume(Convert.ToSingle(VolumeValue));
            }
        }

        /// <summary>
        /// 声音图标按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VolumeIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            float defalutVolume = 10;
            bool IsMute = SystemVolume.IsMuted();
            if (IsMute)
            {
                VolumeIcon.Source = new BitmapImage(new Uri($@"{AppPath}\Image\VolumeIcon.png", UriKind.Absolute));
                SystemVolume.SetMasterVolumeMute(false);
                SystemVolume.SetMasterVolume(defalutVolume);
                SliderVolume.Value = defalutVolume;
            }
            else
            {
                VolumeIcon.Source = new BitmapImage(new Uri($@"{AppPath}\Image\MuteIcon.png", UriKind.Absolute));
                SystemVolume.SetMasterVolumeMute(true);
                SliderVolume.Value = 0;
            }
        }

        /// <summary>
        /// 音乐列表拖拽事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dg_MusicList_DragEnter(object sender, DragEventArgs e)
        {
            Debug.WriteLine("dg_MusicList_DragEnter");
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Link;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }

        /// <summary>
        /// 外部拖拽添加mp3文件代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dg_MusicList_Drop(object sender, DragEventArgs e)
        {
            var fileName = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            this.AddMp3File(fileName);
        }

        /// <summary>
        /// 列表排序代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dg_MusicList_Sorting(object sender, DataGridSortingEventArgs e)
        {
            int ColumnIndex = e.Column.DisplayIndex;
            string ColumnName = e.Column.SortMemberPath;
            switch (e.Column.SortDirection)
            {
                case ListSortDirection.Ascending:
                    this.musicDatas.OrderBy(order =>
                    {
                        return order.GetType().GetMembers().Any(p => p.Name == ColumnName);
                    });
                    break;
                case ListSortDirection.Descending:
                    this.musicDatas.OrderByDescending(order =>
                    {
                        return order.GetType().GetMembers().Any(p => p.Name == ColumnName);
                    });
                    break;
            }
        }

        /// <summary>
        /// 添加歌单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddSongList_Click(object sender, RoutedEventArgs e)
        {
            AddSongList();
        }

        /// <summary>
        /// 歌单选中方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbSongList_Selected(object sender, RoutedEventArgs e)
        {
            int selectedIndex = lbSongList.SelectedIndex;
            var selectedItem = this.songLists[selectedIndex];
            this.SelectedSongList = selectedItem;
            this.InitSongList();
        }


        /// <summary>
        /// 修改歌单方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RenameSongList_Click(object sender, RoutedEventArgs e)
        {
            if (this.SelectedSongList == null)
            {
                MessageBox.Show("没有选择歌单不能修改", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            DataAccess.Model.SongList songList = new DataAccess.Model.SongList();
            songList.SongListId = this.SelectedSongList.SongListId;
            songList.SongListName = this.SelectedSongList.SongListName;
            EditSongList editSongList = new EditSongList(songList, "Edit");
            bool dialogResult = editSongList.ShowDialog().GetValueOrDefault();
            if (dialogResult == true)
                this.InitSongList();
        }

        /// <summary>
        /// 歌单列表鼠标点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbSongList_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                int selectedIndex = lbSongList.SelectedIndex;
                var selectedItem = this.songLists[selectedIndex];
                this.SelectedSongList = new Model.SongList()
                {
                    SongListId = selectedItem.SongListId,
                    SongListName = selectedItem.SongListName
                };
            }
        }

        /// <summary>
        /// 选中歌单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBlock_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if (textBlock != null)
            {
                System.Int64? SongListId = textBlock.Tag as System.Int64?;
                Debug.WriteLine($" SongListId='{SongListId}' ");
                var selectedItem = this.songLists.First(q => q.SongListId == SongListId);
                this.SelectedSongList = new Model.SongList()
                {
                    SongListId = selectedItem.SongListId,
                    SongListName = selectedItem.SongListName
                };
            }
        }

        /// <summary>
        /// 删除歌单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteSongList_Click(object sender, RoutedEventArgs e)
        {
            int state = -1;
            string message = string.Empty;
            long SongListId = 0;
            long defaultSongListId = 0;
            MP3FileInfo deleteInfo = null;
            SongList_DAL songDal = new SongList_DAL(Global.SqliteDbFullPath);
            MP3FileInfo_DAL mp3Dal = new MP3FileInfo_DAL(Global.SqliteDbFullPath);
            if (this.SelectedSongList == null)
            {
                MessageBox.Show("没有选中歌单不能删除", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            SongListId = this.SelectedSongList.SongListId;
            if (this.SelectedSongList.IsDefault)
            {
                MessageBox.Show("默认歌单不能删除", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBoxResult dialogResult = MessageBox.Show(
                "删除歌单的同时是否要删除歌单中的歌曲",
                "是否?",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);
            if (dialogResult == MessageBoxResult.Yes)
            {
                state = mp3Dal.Delete($" SongListId='{SongListId}' ", out message);
                if (state != 0)
                {
                    MessageBox.Show($"删除歌曲出错,原因[{message}]", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            else
            {
                state = mp3Dal.Update("SongListId", Convert.ToString(SongListId), "SongListId=(select SongListId from SongList where IsDefault=1)", out message);
                if (state != 0)
                {
                    MessageBox.Show($"更新歌曲到默认歌单出错,原因[{message}]", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            state = songDal.Delete($" SongListId='{SongListId}' ", out message);
            if (state != 0)
            {
                MessageBox.Show($"删除歌单出错,原因[{message}]", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            this.InitSongList();
        }

        /// <summary>
        /// 歌曲列表左键单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridCell_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            int SelectedIndex = this.dg_MusicList.SelectedIndex;
            this.selectedMusic = this.musicDatas[SelectedIndex];
            this.tbSongTitle.Text = this.selectedMusic.Title;
        }

        /// <summary>
        /// 歌曲移动到歌单事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoveSongList_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = this.dg_MusicList.SelectedIndex;
            if (selectedIndex != -1)
            {
                long FileId = this.musicLists[selectedIndex].FileId;
                long SongListId = this.SelectedSongList.SongListId;
                MoveMusic move = new MoveMusic(FileId, SongListId);
                bool dialogResult = move.ShowDialog().GetValueOrDefault();
                if (dialogResult)
                    InitSongList();
            }
        }

        /// <summary>
        /// 鼠标左键点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if (textBlock != null)
            {
                System.Int64? SongListId = textBlock.Tag as System.Int64?;
                Debug.WriteLine($" SongListId='{SongListId}' ");
                var selectedItem = this.songLists.First(q => q.SongListId == SongListId);
                this.SelectedSongList = new Model.SongList()
                {
                    SongListId = selectedItem.SongListId,
                    SongListName = selectedItem.SongListName
                };
                this.InitSongList();
            }
        }

        /// <summary>
        /// 删除歌曲事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteMusic_Click(object sender, RoutedEventArgs e)
        {
            MP3FileInfo_DAL dal = null;
            string message = string.Empty;
            int state = -1;
            if (this.selectedMusic == null)
            {
                System.Windows.MessageBox.Show("没有选中歌曲不能删除", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBoxResult DialogResult = System.Windows.MessageBox.Show("是否连物理文件一起删除?", "错误", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (DialogResult == MessageBoxResult.Yes)
            {
                try
                {
                    if (File.Exists(this.selectedMusic.FileFullPath))
                        File.Delete(this.selectedMusic.FileFullPath);
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show($"删除物理文件失败,原因[{ex.Message}]", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            dal = new MP3FileInfo_DAL(Global.SqliteDbFullPath);
            state = dal.Delete($" FileId='{this.selectedMusic.FileId}' ", out message);
            if (state != 0)
            {
                System.Windows.MessageBox.Show($"删除数据库数据失败,原因[{message}]", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.InitSongList();
        }

        /// <summary>
        /// 歌曲列表鼠标点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridCell_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int SelectedIndex = this.dg_MusicList.SelectedIndex;
            if (SelectedIndex == -1)
                return;
            this.selectedMusic = this.musicDatas[SelectedIndex];
            this.tbSongTitle.Text = this.selectedMusic.Title;
        }

        /// <summary>
        /// 歌曲列表拖拽鼠标事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dg_MusicList_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int SelectedIndex = this.dg_MusicList.SelectedIndex;
            if (SelectedIndex == -1)
                return;
            this.selectedMusic = this.musicDatas[SelectedIndex];
            this.tbSongTitle.Text = this.selectedMusic.Title;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.dragMp3FileInfo = this.musicDatas[SelectedIndex];
                #region 设置默认封面
                string DefaultCover = $@"{AppPath}\Image\NoCover.png";
                string Conver = string.IsNullOrEmpty(this.dragMp3FileInfo.Cover) ? DefaultCover : $@"{AppPath}\{this.dragMp3FileInfo.Cover}";
                #endregion
                PopupImage.Source = new BitmapImage(new Uri(Conver, UriKind.Absolute));
                IsMusicMovePopup = true;
                popup.IsOpen = true;
            }
        }

        /// <summary>
        /// 窗体鼠标放开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindowHandle_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (IsMusicMovePopup)
            {
                popup.IsOpen = false;
                IsMusicMovePopup = false;
            }
        }

        /// <summary>
        /// 窗体鼠标移动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindowHandle_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMusicMovePopup)
            {
                Point mousePoint = e.GetPosition(MainWindowHandle);
                popup.HorizontalOffset = mousePoint.X;
                popup.VerticalOffset = mousePoint.Y;
                IsMusicMovePopup = true;
            }
        }

        /// <summary>
        /// 歌曲拖拽到歌单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void songListItem_MouseMove(object sender, MouseEventArgs e)
        {
            #region 声明变量

            //状态
            int State = -1;

            //错误消息
            string message = string.Empty;

            //SQLite数据库
            MP3FileInfo_DAL dal = null;

            //歌单控件
            TextBlock textBlock = null;
            #endregion

            //判断是否拖拽结束
            if (!popup.IsOpen && e.LeftButton == MouseButtonState.Released && this.dragMp3FileInfo != null)
            {
                #region 获取要拖拽到的目标歌单
                textBlock = sender as TextBlock;//目标控件
                var songlistItem = this.songLists.First(q => q.SongListId == Convert.ToInt64(textBlock.Tag));//获得tag
                #endregion

                if (Convert.ToInt64(songlistItem.SongListId) == this.dragMp3FileInfo.SongListId)
                {
                    MessageBox.Show("歌曲已经在要移动的歌单里不需要移动", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.dragMp3FileInfo = null;
                    return;
                }

                #region 移动歌曲到歌单
                dal = new MP3FileInfo_DAL(Global.SqliteDbFullPath);
                State = dal.Update(
                    "SongListId",
                    Convert.ToString(songlistItem.SongListId),
                    $" FileId='{this.dragMp3FileInfo.FileId}' ",
                    out message);
                if (State != 0)
                {
                    MessageBox.Show(
                        $"移动歌曲文件[{this.dragMp3FileInfo.FileName}]\n" +
                        $"到歌单[{songlistItem.SongListName}]出错\n" +
                        $"原因[{message}]",
                        "错误",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                #endregion

                //释放移动的歌曲准备下次拖拽
                this.dragMp3FileInfo = null;

                //刷新当前页面
                this.InitSongList();
            }
        }

        /// <summary>
        /// 歌词按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            #region 声明和初始化

            //打开歌词文件
            OpenFileDialog fileDialog = new OpenFileDialog();

            //对话框返回结果
            bool? DialogResult = false;

            //歌词帮助类
            LyricHelper lyric = null;

            //错误消息
            string message = string.Empty;

            //默认颜色
            string defaultColor = "White";
            #endregion

            if (Global.lyric != null)
                Global.lyric = null;

            if (this.selectedMusic == null)
            {
                MessageBox.Show("请先选择歌曲", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!string.IsNullOrEmpty(this.selectedMusic.LyricFilePath))
            {
                if (File.Exists(this.selectedMusic.LyricFilePath))
                {
                    #region 分析并读取歌词文件
                    AnalyseLyricAndShow(this.selectedMusic.LyricFilePath, defaultColor, ref lyric, out message);
                    if (!string.IsNullOrEmpty(message))
                    {
                        MessageBox.Show(
                            $"读取歌词[{this.selectedMusic.LyricFilePath}]出错\n原因[{message}]",
                            "错误",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
                        return;
                    }
                    #endregion

                    #region 初始化并打开歌词窗口

                    //初始化歌词文件
                    Global.lyric = new LyricMusic(lyric.LyricsLines);

                    //设置当前播放的歌曲
                    Global.lyric.MP3File = this.selectedMusic;

                    //设置当前播放进度
                    Global.lyric.CurrentPlayDuration = this.CurrentMusicSlider.Value;

                    //打开歌词窗口
                    Global.lyric.Show();

                    //显示歌词
                    Global.lyric.ShowLyrics();
                    #endregion
                    return;
                }
            }

            #region 初始化歌词
            fileDialog.Filter = "ylrc|*.ylrc";
            fileDialog.Title = "打开歌词文件";
            DialogResult = fileDialog.ShowDialog();
            #endregion


            if (DialogResult.GetValueOrDefault())
            {
                #region 分析并读取歌词文件
                AnalyseLyricAndShow(fileDialog.FileName, defaultColor, ref lyric, out message);
                if (!string.IsNullOrEmpty(message))
                {
                    MessageBox.Show(
                        $"读取歌词[{fileDialog.FileName}]出错\n原因[{message}]",
                        "错误",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    return;
                }
                #endregion


                #region 保存歌词文件
                MP3FileInfo_DAL dal = new MP3FileInfo_DAL(Global.SqliteDbFullPath);
                this.selectedMusic.LyricFilePath = fileDialog.FileName;
                int state = dal.Update(
                    new List<MP3FileInfo>() { this.selectedMusic },
                    $" FileId='{this.selectedMusic.FileId}' ",
                    out message
                    );
                if (state != 0)
                {
                    MessageBox.Show(
                        $"保存歌词[{fileDialog.FileName}]出错\n原因[{message}]",
                        "错误",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    return;
                }
                #endregion
                Global.lyric = new LyricMusic(lyric.LyricsLines);
                Global.lyric.MP3File = this.selectedMusic;
                Global.lyric.CurrentPlayDuration = this.CurrentMusicSlider.Value;
                Global.lyric.Show();
                Global.lyric.ShowLyrics();
            }
        }

        /// <summary>
        /// 搜索框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.InitMusicList(defaultSontListId);
            }
        }
        #endregion

        #region 私有方法

        /// <summary>
        /// 添加MP3文件
        /// </summary>
        /// <param name="fileName">文件全路径</param>
        private void AddMp3File(string fileName) 
        {
            try
            {
                #region 声明变量

                //获取文件
                FileInfo fileInfo = new FileInfo(fileName);

                //mp3文件分析帮助类
                Mp3FileNewHelper fileHelper = null;

                //要添加的mp3文件
                MP3FileInfo mP3FileInfo = null;

                //错误消息
                string message = string.Empty;

                //数据库添加类
                MP3FileInfo_DAL dal = new MP3FileInfo_DAL(Global.SqliteDbFullPath);
                #endregion

                #region 验证文件必须是mp3文件
                if (fileInfo.Extension.ToLower().IndexOf("lnk") != -1)
                {
                    string FilePath = ShotCutTool.GetTargetPath(fileName);
                    fileInfo = new FileInfo(FilePath);
                }
                if (fileInfo.Extension.ToLower().IndexOf("mp3") == -1)
                {
                    MessageBox.Show("不能添加非mp3文件", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                #endregion

                #region 分析并添加MP3文件
                fileHelper = new Mp3FileNewHelper(fileName);
                mP3FileInfo = fileHelper.AnalyseMp3File();
                if (this.SelectedSongList != null)
                    mP3FileInfo.SongListId = this.SelectedSongList.SongListId;
                else if (this.songLists != null && this.songLists.Any(q => q.IsDefault == 1))
                    mP3FileInfo.SongListId = this.songLists.Where(q => q.IsDefault == 1).First().SongListId;
                int insertState = dal.Insert(new List<MP3FileInfo>() { mP3FileInfo }, out message);
                if (insertState != 0)
                    throw new Exception($"添加歌曲出错,原因\n[{message}]");
                else
                {
                    this.musicLists.Add(mP3FileInfo);
                    this.musicDatas.Add(mP3FileInfo);
                }
                #endregion

                //刷新界面
                this.InitMusicList(mP3FileInfo.SongListId);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// 添加歌单
        /// </summary>
        private void AddSongList() 
        {
            EditSongList editSongList = new EditSongList(null, "Add");
            bool dialogResult = editSongList.ShowDialog().GetValueOrDefault();
            if (dialogResult == true)
                this.InitSongList();
        }

        /// <summary>
        /// 上一首歌
        /// </summary>
        private void PreviousMusic() 
        {
            if (this.musicLists == null || this.musicLists.Count <= 0)
                return;
            int SelectedIndex = this.dg_MusicList.SelectedIndex;
            SelectedIndex -= 1;
            if (SelectedIndex < 0)
            {
                SelectedIndex = 0;
                MessageBox.Show("已经是第一首歌曲了", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            this.dg_MusicList.SelectedIndex = SelectedIndex;
            this.SetMusicInfoToUI();
        }

        /// <summary>
        /// 下一首
        /// </summary>
        private void NextMusic() 
        {
            if (this.musicLists == null || this.musicLists.Count <= 0)
                return;
            int SelectedIndex = this.dg_MusicList.SelectedIndex;
            SelectedIndex += 1;
            if (SelectedIndex > this.musicLists.Count - 1)
            {
                SelectedIndex = this.musicLists.Count - 1;
                MessageBox.Show("已经是最后一首歌曲了", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            this.dg_MusicList.SelectedIndex = SelectedIndex;
            this.SetMusicInfoToUI();
        }

        /// <summary>
        /// 获取歌曲搜索条件
        /// </summary>
        /// <param name="searchStr"></param>
        /// <returns></returns>
        private string GetSearchMp3FileInfoStr(string searchStr)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(searchStr))
            {
                result = $" FileName like '%{searchStr}%' or " +
                         $" Title like '%{searchStr}%' or " +
                         $" Album like '%{searchStr}%' ";
            }
            return result;
        }

        /// <summary>
        /// 窗口最大化按钮点击事件
        /// </summary>
        private void MaxWindowClick()
        {
            BitmapImage image = null;
            string message = string.Empty;
            if (this.WindowState != WindowState.Maximized)
            {
                image = Library.ToolsHelper.GetImageSource(@"\Image\NormalIcon-01.png", out message);
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                image = Library.ToolsHelper.GetImageSource(@"\Image\MaxIcon-01.png", out message);
                this.WindowState = WindowState.Normal;
            }
            if (image != null)
            {
                this.NormalMaxBtn.Source = image;
            }
        }

        /// <summary>
        /// 初始化音量
        /// </summary>
        private void InitVolume()
        {
            SliderVolume.Maximum = 100;
            SliderVolume.Minimum = 0;
            SliderVolume.Value = SystemVolume.GetMasterVolume();
            if (SystemVolume.IsMuted() || SliderVolume.Value == 0)
            {
                SliderVolume.Value = 0;
                VolumeIcon.Source = new BitmapImage(new Uri($@"{AppPath}\Image\MuteIcon.png", UriKind.Absolute));
            }
            else
            {
                VolumeIcon.Source = new BitmapImage(new Uri($@"{AppPath}\Image\VolumeIcon.png", UriKind.Absolute));
            }
        }

        /// <summary>
        /// 执行播放顺序
        /// </summary>
        private void ExecutePlaySequence()
        {
            #region 判断当前歌曲列表是否为空
            if (this.musicLists == null || this.musicLists.Count <= 0)
                return;
            if (this.musicLists.Count == 1)
                return;
            #endregion

            //获得当前正在播放的歌曲索引
            int SelectedIndex = this.dg_MusicList.SelectedIndex;

            #region 执行播放顺序
            switch (this.CurrentPlaySequence)
            {
                #region 顺序循环播放
                case PlaySequence.SequenceCyclic:
                    SelectedIndex += 1;
                    if (SelectedIndex > this.musicLists.Count - 1)
                        SelectedIndex = 0;
                    this.dg_MusicList.SelectedIndex = SelectedIndex;
                    this.SetMusicInfoToUI();
                    break;
                #endregion

                #region 顺序只播放一次
                case PlaySequence.SequenceOne:
                    SelectedIndex += 1;
                    if (SelectedIndex > this.musicLists.Count - 1)
                    {
                        SelectedIndex = this.musicLists.Count - 1;
                        this.dg_MusicList.SelectedIndex = SelectedIndex;
                        this.SetMusicInfoToUI();
                        this.CurrentPlayStatus = "Stop";
                        this.ChangePlayStatus();
                    }
                    else
                    {
                        this.dg_MusicList.SelectedIndex = SelectedIndex;
                        this.SetMusicInfoToUI();
                    }
                    break;
                #endregion

                #region 随机播放
                case PlaySequence.RandomPlay:
                    Random random = new Random();
                    SelectedIndex = random.Next(0, this.musicLists.Count - 1);
                    this.dg_MusicList.SelectedIndex = SelectedIndex;
                    this.SetMusicInfoToUI();
                    break;
                #endregion

                #region 单曲循环
                case PlaySequence.SingleLoop:
                    this.dg_MusicList.SelectedIndex = SelectedIndex;
                    this.SetMusicInfoToUI();
                    break;
                    #endregion

            }
            #endregion
        }

        /// <summary>
        /// 生成主菜单
        /// </summary>
        /// <param name="isWpf">是否WPF菜单</param>
        /// <returns>主菜单</returns>
        private dynamic MarkMainMenu(bool isWpf)
        {
            dynamic result = isWpf ? new System.Windows.Controls.ContextMenu() : new System.Windows.Forms.ContextMenuStrip();
            #region 主菜单菜单项目

            if (!isWpf)
                result.Items.Add(new System.Windows.Forms.ToolStripSeparator());

            #region 播放
            if (isWpf)
            {
                var playMusic = new System.Windows.Controls.MenuItem()
                {
                    Header = "播放"
                };
                playMusic.Click += PlayMusic_Click;
                result.Items.Add(playMusic);
            }
            else
            {
                var playMusic = new System.Windows.Forms.ToolStripMenuItem()
                {
                    Text = "播放"
                };
                playMusic.Click += PlayMusic_Click;
                result.Items.Add(playMusic);
            }
            #endregion

            #region 暂停
            if (isWpf)
            {
                var pauseMusic = new System.Windows.Controls.MenuItem()
                {
                    Header = "暂停"
                };
                pauseMusic.Click += PauseMusic_Click;
                result.Items.Add(pauseMusic);
            }
            else
            {
                var pauseMusic = new System.Windows.Forms.ToolStripMenuItem()
                {
                    Text = "暂停"
                };
                pauseMusic.Click += PauseMusic_Click;
                result.Items.Add(pauseMusic);
            }
            #endregion

            #region 下一首
            if (isWpf)
            {
                var nextMusic = new System.Windows.Controls.MenuItem()
                {
                    Header = "下一首"
                };
                nextMusic.Click += NextMusic_Click;
                result.Items.Add(nextMusic);
            }
            else
            {
                var nextMusic = new System.Windows.Forms.ToolStripMenuItem()
                {
                    Text = "下一首"
                };
                nextMusic.Click += NextMusic_Click;
                result.Items.Add(nextMusic);
            }
            #endregion

            #region 上一首
            if (isWpf)
            {
                var previousMusic = new System.Windows.Controls.MenuItem()
                {
                    Header = "上一首"
                };
                previousMusic.Click += PreviousMusic_Click;
                result.Items.Add(previousMusic);
            }
            else
            {
                var previousMusic = new System.Windows.Forms.ToolStripMenuItem()
                {
                    Text = "上一首"
                };
                previousMusic.Click += PreviousMusic_Click;
                result.Items.Add(previousMusic);
            }
            #endregion

            if (!isWpf)
                result.Items.Add(new System.Windows.Forms.ToolStripSeparator());

            #region 批量导入MP3到歌单
            if (isWpf)
            {
                var importMp3 = new System.Windows.Controls.MenuItem()
                {
                    Header = "批量导入MP3到歌单"
                };
                importMp3.Click += ImportMp3_Click;
                result.Items.Add(importMp3);
            }
            else
            {
                var importMp3 = new System.Windows.Forms.ToolStripMenuItem()
                {
                    Text = "批量导入MP3到歌单"
                };
                importMp3.Click += ImportMp3_Click;
                result.Items.Add(importMp3);
            }
            #endregion

            #region 添加MP3
            if (isWpf) 
            {
                var addMp3 = new System.Windows.Controls.MenuItem()
                {
                    Header = "添加MP3"
                };
                addMp3.Click += AddMp3_Click;
                result.Items.Add(addMp3);
            }
            else
            {
                var addMp3 = new System.Windows.Forms.ToolStripMenuItem()
                {
                    Text = "添加MP3"
                };
                addMp3.Click += AddMp3_Click;
                result.Items.Add(addMp3);
            }
            #endregion

            #region 添加歌单
            if (isWpf)
            {
                var addSongList = new System.Windows.Controls.MenuItem()
                {
                    Header = "添加歌单"
                };
                addSongList.Click += AddSongList_Click1;
                result.Items.Add(addSongList);
            }
            else
            {
                var addSongList = new System.Windows.Forms.ToolStripMenuItem()
                {
                    Text = "添加歌单"
                };
                addSongList.Click += AddSongList_Click1;
                result.Items.Add(addSongList);
            }
            #endregion

            if(!isWpf)
               result.Items.Add(new System.Windows.Forms.ToolStripSeparator());

            #region 显示主页面
            if (!isWpf) 
            {
                var showApp = new System.Windows.Forms.ToolStripMenuItem()
                {
                    Text = "显示主页面",
                };
                showApp.Click += ShowWindow;
                result.Items.Add(showApp);
            }
            #endregion

            #region 关于本程序
            if (isWpf)
            {
                var aboutApp = new System.Windows.Controls.MenuItem()
                {
                    Header = "关于本程序"
                };
                aboutApp.Click += AboutApp_Click;
                result.Items.Add(aboutApp);
            }
            else
            {
                var aboutApp = new System.Windows.Forms.ToolStripMenuItem()
                {
                    Text = "关于本程序"
                };
                aboutApp.Click += AboutApp_Click;
                result.Items.Add(aboutApp);
            }
            #endregion

            #region 使用帮助
            if(isWpf)
            {
                var helpApp = new System.Windows.Controls.MenuItem()
                {
                    Header = "使用帮助"
                };
                helpApp.Click += HelpApp_Click;
                result.Items.Add(helpApp);
            }
            else
            {
                var helpApp = new System.Windows.Forms.ToolStripMenuItem() 
                {
                    Text="使用帮助"
                };
                helpApp.Click += HelpApp_Click;
                result.Items.Add(helpApp);
            }
            #endregion

            #region 退出程序
            if (isWpf)
            {
                var exitApp = new System.Windows.Controls.MenuItem()
                {
                    Header = "退出程序"
                };
                exitApp.Click += ExitApp;
                result.Items.Add(exitApp);
            }
            else
            {
                var exitApp = new System.Windows.Forms.ToolStripMenuItem()
                {
                    Text = "退出程序"
                };
                exitApp.Click += ExitApp;
                result.Items.Add(exitApp);
            }
            #endregion

            #endregion

            return result;

        }



        #region 主菜单点击事件

        /// <summary>
        /// 关于点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void AboutApp_Click(object? sender, EventArgs e)
        {
            if (Global.about == null)
                Global.about = new About();
            Global.about.Show();
        }

        /// <summary>
        /// 使用帮助
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void HelpApp_Click(object? sender, EventArgs e)
        {
            Global.help = new HelpApp();
            Global.help.Show();
        }

        /// <summary>
        /// 添加MP3文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void AddMp3_Click(object? sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "mp3|*.mp3";
            fileDialog.Title = "打开mp3文件";
            bool? DialogResult = fileDialog.ShowDialog();
            if(!string.IsNullOrEmpty(fileDialog.FileName)&&File.Exists(fileDialog.FileName))
                AddMp3File(fileDialog.FileName);
        }

        /// <summary>
        /// 下一首歌
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextMusic_Click(object? sender, EventArgs e)
        {
            this.NextMusic();
        }

        /// <summary>
        /// 上一首歌
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviousMusic_Click(object? sender, EventArgs e)
        {
            this.PreviousMusic();
        }

        /// <summary>
        /// 批量导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void ImportMp3_Click(object? sender, EventArgs e)
        {
            this.BrowseImportMp3FIle();
        }

        /// <summary>
        /// 添加歌单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddSongList_Click1(object? sender, EventArgs e)
        {
            this.AddSongList();
        }

        /// <summary>
        /// 播放按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayMusic_Click(object? sender, EventArgs e)
        {
            this.CurrentPlayStatus = "Play";
            this.ChangePlayStatus();
        }

        /// <summary>
        /// 暂停按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PauseMusic_Click(object? sender, EventArgs e)
        {
            this.CurrentPlayStatus = "Stop";
            this.ChangePlayStatus();
        }
        #endregion

        /// <summary>
        /// 任务栏图标按钮的实现
        /// </summary>
        private void NotifyButton()
        {
            System.Windows.Forms.ToolStripMenuItem showApp = new System.Windows.Forms.ToolStripMenuItem() { Text = "显示主页面" };//定义菜单按钮
            System.Windows.Forms.ToolStripMenuItem exitApp = new System.Windows.Forms.ToolStripMenuItem() { Text = "退出程序" };//定义菜单按钮


            System.Windows.Forms.ContextMenuStrip iconMenu = new System.Windows.Forms.ContextMenuStrip();//定义菜单
            iconMenu.Items.AddRange(new System.Windows.Forms.ToolStripMenuItem[] { showApp, exitApp });//添加菜单按钮

            notifyIcon = new System.Windows.Forms.NotifyIcon();//实例化
            notifyIcon.Text = "NetTool";//鼠标在托盘图标上面时显示的文本
            string path = System.IO.Directory.GetCurrentDirectory();
            string FullPath = $@"{path}\Logo16.ico";
            System.IO.Stream ManifestStream = new FileStream(FullPath, FileMode.Open, FileAccess.Read);//读取图标文件流
            notifyIcon.Icon = new System.Drawing.Icon(ManifestStream);//定义程序图标
            notifyIcon.Visible = true;//是否显示
            notifyIcon.MouseDoubleClick += ShowWindow;//鼠标双击事件
            notifyIcon.ContextMenuStrip = MarkMainMenu(false);//绑定菜单
        }

        /// <summary>
        /// 设置选中的音乐到界面
        /// </summary>
        private void SetMusicInfoToUI()
        {
            #region 获取界面选中的音乐
            int SelectedIndex = this.dg_MusicList.SelectedIndex;
            if (SelectedIndex == -1)
                return;
            this.selectedMusic = this.musicDatas[SelectedIndex];
            #endregion

            #region 设置默认封面
            string DefaultCover = $@"{AppPath}\Image\NoCover.png";
            string Conver = string.IsNullOrEmpty(this.selectedMusic.Cover) ? DefaultCover : $@"{AppPath}\{this.selectedMusic.Cover}";
            #endregion

            #region 设置歌曲信息

            //歌曲名
            this.tbSongTitle.Text = this.selectedMusic.Title;
            this.tbMiniSongTitle.Text = this.selectedMusic.Title;

            //歌手
            this.tbSongArtist.Text = "歌手:" + this.selectedMusic.Artist;
            this.tbMiniSongArtist.Text = this.selectedMusic.Artist;

            //文件编号
            this.tbSongFileId.Text = "文件编号:" + this.selectedMusic.FileId;

            //文件名
            this.tbSongFileName.Text = "文件名:" + this.selectedMusic.FileName;

            //所属唱片
            this.tbSongAlbum.Text = "所属唱片:" + this.selectedMusic.Album;

            //文件大小
            this.tbSongFileSize.Text = "文件大小:" + ToolsHelper.GetFileSize(Convert.ToInt64(this.selectedMusic.FileSize));

            //总时长
            this.tbSongDuration.Text = "时长:" + ToolsHelper.formatDuration(this.selectedMusic.duration);

            //设置进度条边上的总时长
            this.CurrentMusicDuration.Text = ToolsHelper.formatDuration(this.selectedMusic.duration);
            #endregion

            //设置封面
            this.MusicCover.Source = new BitmapImage(new Uri(Conver, UriKind.Absolute));
            this.MiniCover.Source = new BitmapImage(new Uri(Conver, UriKind.Absolute));

            #region 设置歌曲播放源
            if (File.Exists(this.selectedMusic.FileFullPath))
                this.mediaPlayer.Source = new Uri(this.selectedMusic.FileFullPath, UriKind.Absolute);
            else
            {
                MessageBox.Show(
                $"歌曲文件已不存在,无法播放\n文件路径[{this.selectedMusic.FileFullPath}]",
                "错误",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
                return;
            }
            #endregion

            #region 设置进度条

            //最大值等于总时长(单位:秒钟)
            this.CurrentMusicSlider.Maximum = this.selectedMusic.duration;

            //当前值为0
            this.CurrentMusicSlider.Value = 0;
            #endregion

            #region 初始化唱片动画
            if (this.discStoryboard == null)
                this.discStoryboard = Resources["DiscAnimation"] as Storyboard;
            this.discStoryboard.Stop();//先停止播放唱片旋转动画
            #endregion

            //重置唱片旋转角度
            this.AngleRotate.Angle = 0;

            //唱针默认角度位置
            this.StylusAngle = -35;

            #region 读取并配置歌词
            string defaultColor = "White";
            LyricHelper lyric = null;
            string message = string.Empty;
            if (!string.IsNullOrEmpty(this.selectedMusic.LyricFilePath))
            {
                #region 分析并读取歌词文件
                AnalyseLyricAndShow(this.selectedMusic.LyricFilePath, defaultColor, ref lyric, out message);
                if (!string.IsNullOrEmpty(message))
                {
                    System.Windows.MessageBox.Show(
                        $"读取歌词[{this.selectedMusic.LyricFilePath}]出错\n原因[{message}]",
                        "错误",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                else
                {
                    Global.lyric = new LyricMusic(lyric.LyricsLines);
                    Global.lyric.MP3File = this.selectedMusic;
                    Global.lyric.CurrentPlayDuration = this.CurrentMusicSlider.Value;
                }
                #endregion
            }
            else
            {
                if (Global.lyric != null)
                    Global.lyric.Visibility = Visibility.Hidden;
                Global.lyric = null;
            }
            #endregion

            #region 设置当前播放状态为播放
            this.CurrentPlayStatus = "Play";
            this.ChangePlayStatus();
            #endregion
        }

        /// <summary>
        /// 播放状态改变代码
        /// </summary>
        private void ChangePlayStatus()
        {
            switch (this.CurrentPlayStatus)
            {
                case "Play":

                    #region 播放代码

                    //没有选择歌曲不能播
                    if (this.CurrentMusic == null)
                    {
                        MessageBox.Show("没有选择歌曲不能播", "错误",MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    //设置播放进度配合进度条使用
                    this.mediaPlayer.Position = TimeSpan.FromSeconds(this.CurrentMusicSlider.Value);

                    //开始播放音乐
                    mediaPlayer.Play();

                    //启动进度显示进程
                    playTimer.Start();

                    //启动唱片封面旋转动画
                    discStoryboard.Begin();

                    //唱针移动到播放为止
                    this.StylusAngle = -25;//唱针播放角度位置

                    #region 设置播放按钮图标为暂停图标
                    this.PlayButtonImage.Source = new BitmapImage(new Uri($@"{AppPath}\Image\PauseIcon.png", UriKind.Absolute));
                    this.PlayButtonImage.Margin = new Thickness(0, 0, 0, 0);
                    #endregion

                    #endregion

                    break;

                case "Stop":

                    #region 停止播放代码

                    //停止音乐播放
                    mediaPlayer.Stop();

                    //暂停封面旋转动画
                    discStoryboard.Pause();

                    //重置唱针到默认角度
                    this.StylusAngle = -35;//唱针默认角度位置

                    //停止显示进度条
                    playTimer.Stop();

                    #region 设置播放按钮图标为播放
                    this.PlayButtonImage.Source = new BitmapImage(new Uri($@"{AppPath}\Image\PlayIcon.png", UriKind.Absolute));
                    this.PlayButtonImage.Margin = new Thickness(5, 0, 0, 0);
                    #endregion

                    #endregion

                    break;
            }
        }

        /// <summary>
        /// 改变播顺序
        /// </summary>
        private void ChangePlaySequence()
        {
            if (this.CurrentPlaySequence < PlaySequence.SingleLoop)
                this.CurrentPlaySequence += 1;
            else
                this.CurrentPlaySequence = 0;
            SetPlaySequenceBtnIcon();
        }

        /// <summary>
        /// 改变播放顺序按钮图标
        /// </summary>
        private void SetPlaySequenceBtnIcon()
        {
            switch (this.CurrentPlaySequence)
            {
                case PlaySequence.SequenceCyclic:
                    BtnPlaySequence.Source = new BitmapImage(new Uri($@"{AppPath}\Image\LoopPlayIcon.png", UriKind.Absolute));
                    BtnPlaySequence.ToolTip = "顺序循环播放";
                    break;
                case PlaySequence.SequenceOne:
                    BtnPlaySequence.Source = new BitmapImage(new Uri($@"{AppPath}\Image\SequencePlayOne.png", UriKind.Absolute));
                    BtnPlaySequence.ToolTip = "顺序只播放1次";
                    break;
                case PlaySequence.SingleLoop:
                    BtnPlaySequence.Source = new BitmapImage(new Uri($@"{AppPath}\Image\SingleLoopPlay.png", UriKind.Absolute));
                    BtnPlaySequence.ToolTip = "单曲循环";
                    break;
                case PlaySequence.RandomPlay:
                    BtnPlaySequence.Source = new BitmapImage(new Uri($@"{AppPath}\Image\RandomPlayIcon.png", UriKind.Absolute));
                    BtnPlaySequence.ToolTip = "随机播放";
                    break;
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

        /// <summary>
        /// 浏览并导入MP3文件
        /// </summary>
        private void BrowseImportMp3FIle() 
        {
            System.Windows.Forms.FolderBrowserDialog folderBrowser = null;
            folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult dialogResult = folderBrowser.ShowDialog();
            string message = string.Empty;
            List<MP3FileInfo> insertFiles = new List<MP3FileInfo>();
            MP3FileInfo SqliteFile = null;
            MP3FileInfo_DAL dal = new MP3FileInfo_DAL(Global.SqliteDbFullPath);
            txtFolderPath.Text = folderBrowser.SelectedPath;
            if (dialogResult == System.Windows.Forms.DialogResult.OK)
            {
                #region 验证文件夹选择
                if (string.IsNullOrEmpty(folderBrowser.SelectedPath))
                {
                    MessageBox.Show("没有选择任何文件夹不能导入", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                string[] files = Directory.GetFiles(folderBrowser.SelectedPath, "*.mp3");
                if (files == null || files.Length <= 0)
                {
                    MessageBox.Show("文件夹中没有任何mp3文件", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                #endregion

                #region 循环导入mp3文件
                foreach (string file in files)
                {
                    try
                    {
                        Mp3FileNewHelper mp3Helper = new Mp3FileNewHelper(file);
                        SqliteFile = mp3Helper.AnalyseMp3File();
                        SqliteFile.SongListId = defaultSontListId;
                        insertFiles.Add(SqliteFile);
                    }
                    catch (Exception exp)
                    {
                        FileInfo fileInfo = new FileInfo(file);
                        message += $"文件[{fileInfo.Name}],导入失败\n原因[{exp.Message}]";
                    }
                }
                #endregion


                if (!string.IsNullOrEmpty(message))
                    MessageBox.Show($"部分mp3文件导入出错,原因[{message}]", "错误", MessageBoxButton.OK, MessageBoxImage.Error);

                #region 导入文件
                int state = dal.Insert(insertFiles, out message);
                if (state != 0)
                {
                    MessageBox.Show($"mp3文件导入出错,原因[{message}]", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                #endregion

                //刷新界面列表
                InitMusicList(defaultSontListId);
            }
        }

        /// <summary>
        /// 浏览按钮导入mp3文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrowseFolder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BrowseImportMp3FIle();
        }
        #endregion

        private void btnMainMenu_MouseDown(object sender, MouseButtonEventArgs e)
        {            
            if (btnMainMenu.ContextMenu.IsOpen == false)
                btnMainMenu.ContextMenu.IsOpen = true;
            else
                btnMainMenu.ContextMenu.IsOpen = false;
        }
    }
}
