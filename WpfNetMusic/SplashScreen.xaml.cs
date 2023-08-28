using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Timers;
using DataAccess.DAL;
using System.IO;
using WpfNetMusic.Library;
using HandyControl.Tools.Extension;
using WpfNetMusic.Model;
using DataAccess.Model;
using Snowflake.Net;

namespace WpfNetMusic
{
    /// <summary>
    /// SplashScreen.xaml 的交互逻辑
    /// </summary>
    public partial class SplashScreen : Window
    {
        #region 声明变量

        /// <summary>
        /// 动画组件
        /// </summary>
        private Storyboard storyboard = null;

        /// <summary>
        /// 动画组件1
        /// </summary>
        private Storyboard storyboard1 = null;

        /// <summary>
        /// 进度控制组件
        /// </summary>
        private System.Timers.Timer timer = null;

        /// <summary>
        /// 提示信息
        /// </summary>
        private string _TipsStr = string.Empty;

        /// <summary>
        /// Sqlite数据库文件路径
        /// </summary>
        private string SqliteDBFilePath = string.Empty;

        /// <summary>
        /// 文件夹选择对话框
        /// </summary>
        private System.Windows.Forms.FolderBrowserDialog folderBrowser = null;

        /// <summary>
        /// 搜索的起始文件位置
        /// </summary>
        private List<string> SearchStartPath = new List<string>();

        /// <summary>
        /// 要搜索的文件夹
        /// </summary>
        private List<string> SearchPath = new List<string>();

        /// <summary>
        /// 所有MP3文件
        /// </summary>
        private List<string> AllMP3File = new List<string>();

        /// <summary>
        /// MP3文件信息
        /// </summary>
        private List<Mp3Info> mp3Infos = new List<Mp3Info>();

        /// <summary>
        /// 数据库MP3文件
        /// </summary>
        private List<MP3FileInfo> SqliteMP3Files = new List<MP3FileInfo>();

        /// <summary>
        /// 定时器间隔时间(毫秒)
        /// </summary>
        private double TimeInterval = 1000;

        /// <summary>
        /// 当前文件索引
        /// </summary>
        private int CurrentIndex = 0;

        /// <summary>
        /// 雪花ID
        /// </summary>
        private IdWorker SonwId = null;

        /// <summary>
        /// 错误消息
        /// </summary>
        private List<string> ErrorMsg= new List<string>();
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public SplashScreen()
        {
            try
            {
                InitializeComponent();
                this.DataContext = this;
                this.MainProgress.Width = this.Width;
                this.MainProgress.Visibility = Visibility.Hidden;
                this.SplashScreenBG.Width = this.Width;
                this.SplashScreenBG.Height = this.Height - this.MainProgress.Height;
                SonwId = new IdWorker(1, 1);
                string AppPath = Directory.GetCurrentDirectory();//应用程序所在目录
                storyboard1 = Resources["LocalAnimation"] as Storyboard;
                storyboard = Resources["LocalAnimation"] as Storyboard;
                storyboard.Begin();
                storyboard1.Begin();
                #region 调整窗口弹出位置在屏幕中央
                double screeHeight = SystemParameters.FullPrimaryScreenHeight;
                double screeWidth = SystemParameters.FullPrimaryScreenWidth;
                this.Top = (screeHeight - this.Height) / 2;
                this.Left = (screeWidth - this.Width) / 2;
                #endregion
                #region 设置主定时器
                timer = new System.Timers.Timer();
                timer.Interval = TimeInterval;//间隔时间单位毫秒
                timer.Elapsed += Timer_Elapsed; //处理函数
                #endregion
            }
            catch(Exception exp) 
            {
                MessageBox.Show($"程序启动错误{exp.Message}","错误",MessageBoxButton.OK,MessageBoxImage.Error);
                Application.Current.Shutdown();
                return;
            }
        }

        /// <summary>
        /// 提示信息
        /// </summary>
        public string TipsStr
        {
            get
            {
                return _TipsStr;
            }
            set
            {
                _TipsStr = value;
            }
        }

        /// <summary>
        /// 程序启动函数
        /// </summary>
        private void ApplicationStart()
        {
            #region 声明变量

            //是否读取完成配置文件
            bool isComplatedReadSetting = false;

            //错误消息
            string message = string.Empty;

            //mp3文件信息
            var MP3FileInfos = new List<DataAccess.Model.MP3FileInfo>();
            #endregion

            _TipsStr = $"读取本地歌曲数据{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}";
            SetTips(_TipsStr);
            #region 读取数据
            if (!File.Exists(Global.SqliteDbFullPath))
            {
                MessageBox.Show("数据文件不存在", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
                return;
            }
            MP3FileInfos = new MP3FileInfo_DAL(Global.SqliteDbFullPath).Query("", out message);
            if (MP3FileInfos == null || MP3FileInfos.Count <= 0)
            {
                if (!string.IsNullOrEmpty(message))
                {
                    MessageBox.Show($"读取本地数据出错\n原因:{message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    Application.Current.Shutdown();
                    return;
                }
            }
            #endregion

            _TipsStr = $"读取配置文件数据{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}";
            SetTips(_TipsStr);
            #region 读取配置文件
            isComplatedReadSetting = SettingHelper.LoadingSetting(out message);
            if (!isComplatedReadSetting && !string.IsNullOrEmpty(message))
            {
                MessageBox.Show($"读取配置文件出错\n原因:{message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
                return;
            }
            if (!isComplatedReadSetting && string.IsNullOrEmpty(message))
            {
                Global.setting = new Model.GlobalSetting()
                {
                    MusicFilePath = string.Empty,
                    closeSetting = new Model.CloseSetting()
                    {
                        QuitSystem = false,
                        IsMinTaskBar = false
                    }
                };
                isComplatedReadSetting = SettingHelper.SaveSetting(Global.setting, out message);
                if (!isComplatedReadSetting)
                {
                    MessageBox.Show($"保存配置文件出错\n原因:{message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    Application.Current.Shutdown();
                    return;
                }
            }
            #endregion

            if ((MP3FileInfos == null || MP3FileInfos.Count <= 0) && string.IsNullOrEmpty(Global.setting.MusicFilePath))
            {
                MessageBoxResult res = MessageBox.Show($"是否进行全盘搜索MP3音乐文件？", "是否", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (res == MessageBoxResult.Yes)
                {
                    List<string> AllLogicaldisk = ToolsHelper.GetAllOperationSystemLogicaldisk();
                    AllLogicaldisk.ForEach(item =>
                    {
                        SearchStartPath.Add($@"{item}\");
                    });
                }
                else
                {
                    folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
                    var DialogResult = folderBrowser.ShowDialog();
                    if (string.IsNullOrEmpty(folderBrowser.SelectedPath))
                    {
                        MessageBox.Show("必须选择一个存放MP3的文件夹", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                        Application.Current.Shutdown();
                        return;
                    }
                    SearchStartPath.Add(folderBrowser.SelectedPath);
                }
                InitSearchFile();
            }
            else
            {
                if (Global.Main == null)
                    Global.Main = new MainWindow();
                Global.Main.Show();
                this.Close();
            }
        }

        /// <summary>
        /// 递归查找文件
        /// </summary>
        /// <param name="RootFilePath">当前文件夹</param>
        /// <param name="searchPattern">搜索条件</param>
        /// <param name="ResultFiles">返回结果</param>
        private void RecursionFile(string RootFilePath, string searchPattern, ref List<string> ResultFiles)
        {
            if (string.IsNullOrEmpty(RootFilePath))
                return;
            if (!Directory.Exists(RootFilePath))
                return;
            string[] ChildDirectory = null;
            try
            {
                ChildDirectory = Directory.GetDirectories(RootFilePath);
                string[] Files = Directory.GetFiles(RootFilePath, searchPattern);
                if (Files != null && Files.Length > 0)
                    ResultFiles.AddRange(Files);
            }
            catch (Exception ex)
            {
            }
            if (ChildDirectory != null && ChildDirectory.Length > 0)
            {
                foreach (string ChildDirectoryItem in ChildDirectory)
                    RecursionFile(ChildDirectoryItem, searchPattern, ref ResultFiles);
            }
        }

        /// <summary>
        /// 初始化搜索文件
        /// </summary>
        private void InitSearchFile()
        {
            _TipsStr = $"开始搜索所有音乐文件{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}";
            SetTips(_TipsStr);
            foreach (var itemPath in SearchStartPath)
            {
                RecursionFile(itemPath, "*.mp3", ref AllMP3File);
            }
            if (AllMP3File.Count <= 0)
            {
                var DialogResult = MessageBox.Show("没有找到任何mp3文件", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                if (DialogResult == MessageBoxResult.OK)
                    Application.Current.Shutdown();
                return;
            }
            this.MainProgress.Visibility = Visibility.Visible;
            this.MainProgress.Maximum = AllMP3File.Count;
            timer.Start();
        }

        /// <summary>
        /// 进度条循环处理事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            #region 声明变量

            //MP3文件路径
            string MP3FilePath = string.Empty;

            //mp3文件信息
            Mp3Info mp3Info = null;

            //mp3文件信息
            FileInfo mp3File = null;

            //数据库MP3文件
            MP3FileInfo SqliteFile = null;

            //mp3文件帮助类
            Mp3FileNewHelper fileHelper = null;
            #endregion

            timer.Stop();

            #region 已分析完成跳出
            if (CurrentIndex >= AllMP3File.Count)
            {
                _TipsStr = "文件分析保存完毕";
                SetTips(_TipsStr);
                GotoMainWindow();
                return;
            }
            #endregion

            MP3FilePath = AllMP3File[CurrentIndex];
            _TipsStr = $"正在分析MP3文件:{MP3FilePath}";
            SetTips(_TipsStr);

            #region 验证MP3文是否存在
            if (!File.Exists(MP3FilePath))
            {
                _TipsStr = $"文件[{MP3FilePath}]不存在无法分析";
                SetTips(_TipsStr);
                CurrentIndex += 1;
                this.MainProgress.Value = CurrentIndex;
                timer.Start();
                return;
            }
            #endregion

            #region 分析MP3文件
            try
            {
                mp3File = new FileInfo(MP3FilePath);
                fileHelper = new Mp3FileNewHelper(MP3FilePath);
                mp3Info = fileHelper.Mp3Info;
                SqliteFile = fileHelper.AnalyseMp3File();
                #region 测试代码
                //if (CurrentIndex % 2 == 0)
                //    throw new Exception("测试错误消息");
                #endregion
                SqliteMP3Files.Add(SqliteFile);
                _TipsStr = $"文件[{MP3FilePath}]分析成功,已分析[{CurrentIndex}],未分析[{AllMP3File.Count - CurrentIndex}]";
                SetTips(_TipsStr);
            }
            catch (Exception exp)
            {
                _TipsStr = $"文件[{MP3FilePath}]分析出错,原因[{exp.Message}]";
                ErrorMsg.Add(_TipsStr);
                SetTips(_TipsStr);
            }
            #endregion

            CurrentIndex += 1;
            Dispatcher.Invoke(
                new Action(
                    delegate
                    {
                        this.MainProgress.Value = CurrentIndex;
                    }
                )
            );
            timer.Start();
        }

        /// <summary>
        /// 跳转到主界面
        /// </summary>
        private void GotoMainWindow()
        {
            timer.Stop();
            _TipsStr = $"正在保存数据{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}";
            SetTips(_TipsStr);
            if (SaveDatToSQLite())
            {
                Dispatcher.Invoke(
                    new Action(
                        delegate
                        {
                            if (Global.Main == null)
                                Global.Main = new MainWindow();
                            /**
                             *如果有错误消息
                             *先显示错误消息
                             */
                            if (ErrorMsg.Count > 0)
                            {
                                if (Global.errorMsgDialog == null)
                                    Global.errorMsgDialog = new ErrorMsgDialog();
                                string ErrMsg= string.Empty;
                                ErrorMsg.ForEach(messageLine => 
                                {
                                    ErrMsg+=messageLine+"\r\n";
                                });
                                bool? dialogResult=Global.errorMsgDialog.ShowDialog(ErrMsg);
                                if(dialogResult == true) 
                                {
                                    Global.Main.Show();
                                    this.Close();
                                }
                            }
                            else
                            {
                                Global.Main.Show();
                                this.Close();
                            }
                        }
                    )
                );
            }
            
        }

        /// <summary>
        /// 保存数据到SQLite数据库
        /// </summary>
        private bool SaveDatToSQLite()
        {
            #region 声明变量

            bool result = true;

            //保存状态
            int SaveState = 0;

            //错误信息
            string message = string.Empty;

            //歌单数据库保存类
            SongList_DAL songDal = null;

            //MP3数据库保存类
            MP3FileInfo_DAL mp3DAL = null;

            //要保存的歌单ID
            List<DataAccess.Model.SongList> songLists = new List<DataAccess.Model.SongList>();

            //歌单列表
            DataAccess.Model.SongList songList = new DataAccess.Model.SongList();
            #endregion

            #region 初始化歌单
            songList.SongListId = SonwId.NextId();
            songList.SongListName = "默认歌单";
            songList.CreateTime = DateTime.Now;
            songList.IsDefault = 1;
            songLists.Add(songList);
            #endregion

            #region 保存歌单
            songDal = new SongList_DAL(Global.SqliteDbFullPath);
            if (!string.IsNullOrEmpty(message))
            {
                MessageBox.Show($"初始化歌单数据库保存类出错\n原因[{message}]", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            SaveState = songDal.Insert(songLists, out message);
            if (SaveState != 0)
            {
                if (!string.IsNullOrEmpty(message))
                {
                    MessageBox.Show($"保存歌单数据出错\n原因[{message}]", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            #endregion

            #region 保存歌曲
            mp3DAL = new MP3FileInfo_DAL(Global.SqliteDbFullPath);
            if (!string.IsNullOrEmpty(message))
            {
                MessageBox.Show($"初始化MP3歌曲数据库保存类出错\n原因[{message}]", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            for (int i = 0; i < SqliteMP3Files.Count; i++)
                SqliteMP3Files[i].SongListId= songList.SongListId.GetValueOrDefault();
            SaveState = mp3DAL.Insert(SqliteMP3Files, out message);
            if (SaveState != 0)
            {
                if (!string.IsNullOrEmpty(message))
                {
                    MessageBox.Show($"保存MP3歌曲数据出错\n原因[{message}]", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            #endregion

            return result;
        }

        private void SetTips(string TipsContent) 
        {
            Dispatcher.Invoke(
                new Action(
                    delegate
                    {
                        this.tbTips.Text = TipsContent;
                    }
                )
            );
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.MainProgress.Width = this.Width;
            this.SplashScreenBG.Width = this.Width;
            this.SplashScreenBG.Height = this.Height - this.MainProgress.Height;
            this.ApplicationStart();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.MainProgress.Width = this.Width;
            this.SplashScreenBG.Width = this.Width;
            this.SplashScreenBG.Height = this.Height - this.MainProgress.Height;
        }
    }
}
