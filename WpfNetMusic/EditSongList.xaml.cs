using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DataAccess.Model;
using DataAccess.DAL;
using Snowflake.Net;

namespace WpfNetMusic
{
    /// <summary>
    /// EditSongList.xaml 的交互逻辑
    /// </summary>
    public partial class EditSongList : Window
    {
        /// <summary>
        /// 歌单编号
        /// </summary>
        private long _songId;

        /// <summary>
        /// 歌单名称
        /// </summary>
        private string _songName;

        /// <summary>
        /// 是否默认
        /// </summary>
        private bool _isDefault=false;

        /// <summary>
        /// 雪花ID
        /// </summary>
        private IdWorker SnowId;

        /// <summary>
        /// 操作标识
        /// </summary>
        private string _Action = "Add";

        /// <summary>
        /// 歌单编号
        /// </summary>
        public long SongId 
        {
            get { return _songId; }
            set { _songId = value; }
        }

        /// <summary>
        /// 歌单名称
        /// </summary>
        public string SongName 
        {
            get { return _songName; }
            set { _songName = value; }
        }

        /// <summary>
        /// 是否默认歌单
        /// </summary>
        public bool IsDefault 
        {
            get { return _isDefault; }
            set { _isDefault = value; }
        }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public EditSongList()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 重载构造函数
        /// </summary>
        /// <param name="songList">要编辑的歌单</param>
        public EditSongList(SongList songList,string Action)
        {
            if(songList != null) 
            {
                _songId = songList.SongListId.GetValueOrDefault();
                _songName = songList.SongListName;
            }
            _Action=Action;
            this.DataContext = this;
            InitializeComponent();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// 保存歌单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            string message = string.Empty;
            SongList_DAL dal = new SongList_DAL(Global.SqliteDbFullPath);
            List<DataAccess.Model.SongList> saveData = new List<SongList>();
            List<DataAccess.Model.SongList> oldData = new List<SongList>();
            int State = -1;
            if (string.IsNullOrEmpty(_songName)) 
            {
                MessageBox.Show("歌单名称不能为空", "错误", MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }
            if (_Action == "Edit" && _songId <= 0) 
            {
                MessageBox.Show("歌单编号不能为空", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (_Action == "Edit") 
            {
                oldData = dal.Query($" SongListId='{_songId}' ", out message);
                if (oldData == null || oldData.Count <= 0) 
                {
                    if (!string.IsNullOrEmpty(message))
                        message = $"读取歌单数据出错,原因[{message}]";
                    else
                        message = "没有读取到歌单数据";
                    MessageBox.Show(message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            SnowId = new IdWorker(1, 1);
            if (_songId <= 0&&_Action!="Edit")
                _songId = SnowId.NextId();
            saveData.Add(new SongList()
            {
                SongListId = _songId,
                SongListName = _songName,
                IsDefault = _isDefault ? 1 : 0,
                CreateTime = _Action == "Edit" ? oldData[0].CreateTime : DateTime.Now
            });
            if (_Action == "Edit")
                State = dal.Update(saveData, $" SongListId='{_songId}' ", out message);
            else
                State = dal.Insert(saveData, out message);
            if (State != 0) 
            {
                MessageBox.Show($"保存歌单数据出错，原因[{message}]", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (_isDefault)
                State = dal.SetDefault(_songId, out message);
            if (State != 0)
            {
                MessageBox.Show($"设置默认歌单出错，原因[{message}]", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            DialogResult = State==0?true:false;
        }
    }
}
