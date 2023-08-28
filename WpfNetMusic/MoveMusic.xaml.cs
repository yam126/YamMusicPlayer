using DataAccess.DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WpfNetMusic
{
    /// <summary>
    /// 将MP3移动到歌单的逻辑
    /// </summary>
    public partial class MoveMusic : Window
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Mp3FIleId">MP3文件编号</param>
        /// <param name="Mp3SongListId">MP3歌单编号</param>
        public MoveMusic(long Mp3FIleId,long Mp3SongListId)
        {
            this._Mp3FileId= Mp3FIleId;
            this._Mp3SongListId= Mp3SongListId;
            InitializeComponent();
            InitSongList();
            this.DataContext = this;
        }

        /// <summary>
        /// MP3歌单ID
        /// </summary>
        private long _Mp3SongListId;

        /// <summary>
        /// Mp3文件编号
        /// </summary>
        private long _Mp3FileId;

        /// <summary>
        /// 歌单
        /// </summary>
        private ObservableCollection<dynamic> songLists;

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
        /// 初始化歌单列表
        /// </summary>
        public void InitSongList()
        {
            #region 声明变量

            //错误消息
            string message = string.Empty;

            //MP3文件的歌单索引
            int Mp3FileIndex = -1;

            //歌单列表
            List<DataAccess.Model.SongList> songlist = new List<DataAccess.Model.SongList>();

            //数据库读取
            SongList_DAL dbDal = null;

            //默认歌单
            DataAccess.Model.SongList defaultSontList = null;
            #endregion

            if (songLists == null)
                songLists = new ObservableCollection<dynamic>();
            songLists.Clear();
            songLists.Add(new
            {
                SongListId = -1,
                SongListName = "请选择歌单",
                IsSelected = true
            });
            cbSongList.SelectedIndex = 0;

            #region 数据库读取数据
            dbDal = new SongList_DAL(Global.SqliteDbFullPath);
            songlist = dbDal.Query("", out message);
            if (songlist == null || songlist.Count <= 0)
            {
                MessageBox.Show($"读取歌单出错,原因[{message}]", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            #endregion

            #region 取默认歌单
            if (songlist.Any(q => q.IsDefault == 1))
                defaultSontList = songlist.First(q => q.IsDefault == 1);
            else
                defaultSontList = songlist.First();
            #endregion

            #region 将歌单赋值给界面
            for(var i=0; i<songlist.Count; i++) 
            {
                var item = songlist[i];
                bool isSelected = item.SongListId == _Mp3SongListId ? true : false;
                if (isSelected)
                    Mp3FileIndex = i+1;
                songLists.Add(new {
                    SongListId=item.SongListId,
                    SongListName=item.SongListName,
                    IsSelected= isSelected
                });
            }
            this.cbSongList.SelectedIndex = Mp3FileIndex;
            #endregion
        }

        /// <summary>
        /// 取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// 拖拽
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

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility= Visibility.Hidden;
        }

        /// <summary>
        /// 确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            long selectedSongListId = -1;
            int selctedIndex = -1;
            int state = -1;
            string message = string.Empty;
            MP3FileInfo_DAL dal = new MP3FileInfo_DAL(Global.SqliteDbFullPath);
            selctedIndex = cbSongList.SelectedIndex;
            selectedSongListId = this.songLists[selctedIndex].SongListId;
            if(selectedSongListId == -1)
            {
                MessageBox.Show("请选择要移动到的歌单", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            state=dal.Update("SongListId", Convert.ToString(selectedSongListId), $" FileId='{_Mp3FileId}' ", out message);
            if (state == 0)
            {
                DialogResult = true;
                this.Visibility=Visibility.Hidden;
            }
            else
            {
                MessageBox.Show($"移动歌曲出错,原因[{message}]", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                DialogResult = false;
                this.Visibility = Visibility.Hidden;
            }
        }
    }
}
