using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using WpfNetMusic.Model;
using Shell32;
using System.Text.RegularExpressions;
using Id3.v2;
using Snowflake.Net;
using Id3.InfoFx;
using DataAccess.Model;

namespace WpfNetMusic.Library
{
    /// <summary>
    /// MP3文件帮助类
    /// </summary>
    public class Mp3FileHelper
    {
        /// <summary>
        /// MP3文件信息
        /// </summary>
        private Mp3Info info;

        /// <summary>
        /// 返回获取的MP3信息
        /// </summary>
        public Mp3Info Mp3Info 
        { 
            get 
            { 
                return info; 
            } 
        }

        /// <summary>
        /// 构造函数,输入文件名即得到信息
        /// </summary>
        /// <param name="mp3FilePos"></param>
        public Mp3FileHelper(String mp3FilePos)
        {
            info = getMp3Info(getLast128(mp3FilePos));
        }
        /// <summary>
        /// 获取整理后的Mp3文件名,这里以标题和艺术家名定文件名
        /// </summary>
        /// <returns></returns>
        public String GetOriginalName()
        {
            return formatString(info.Title.Trim()) + "-" + formatString(info.Artist.Trim());
        }
        /// <summary>
        /// 去除\0字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static String formatString(String str)
        {
            return str.Replace("\0", "");
        }
        /// <summary>
        /// 获取MP3文件最后128个字节
        /// </summary>
        /// <param name="FileName">文件名</param>
        /// <returns>返回字节数组</returns>
        public static byte[] getLast128(string FileName)
        {
            FileStream fs = new System.IO.FileStream(FileName, FileMode.Open, FileAccess.Read);
            Stream stream = fs;
            stream.Seek(-128, SeekOrigin.End);
            const int seekPos = 128;
            int rl = 0;
            byte[] Info = new byte[seekPos];
            rl = stream.Read(Info, 0, seekPos);
            fs.Close();
            stream.Close();
            return Info;
        }
        /// <summary>
        /// 获取MP3歌曲的相关信息
        /// </summary>
        /// <param name = "Info">从MP3文件中截取的二进制信息</param>
        /// <returns>返回一个Mp3Info结构</returns>
        public static Mp3Info getMp3Info(byte[] Info)
        {
            Mp3Info mp3Info = new Mp3Info();
            string str = null;
            int i;
            int position = 0;//循环的起始值
            int currentIndex = 0;//Info的当前索引值
                                 //获取TAG标识
            for (i = currentIndex; i < currentIndex + 3; i++)
            {
                str = str + (char)Info[i];
                position++;
            }
            currentIndex = position;
            mp3Info.identify = str;
            //获取歌名
            str = null;
            byte[] bytTitle = new byte[30];//将歌名部分读到一个单独的数组中
            int j = 0;
            for (i = currentIndex; i < currentIndex + 30; i++)
            {
                bytTitle[j] = Info[i];
                position++;
                j++;
            }
            currentIndex = position;
            mp3Info.Title = Mp3FileHelper.byteToString(bytTitle);
            //获取歌手名
            str = null;
            j = 0;
            byte[] bytArtist = new byte[30];//将歌手名部分读到一个单独的数组中
            for (i = currentIndex; i < currentIndex + 30; i++)
            {
                bytArtist[j] = Info[i];
                position++;
                j++;
            }
            currentIndex = position;
            mp3Info.Artist = Mp3FileHelper.byteToString(bytArtist);
            //获取唱片名
            str = null;
            j = 0;
            byte[] bytAlbum = new byte[30];//将唱片名部分读到一个单独的数组中
            for (i = currentIndex; i < currentIndex + 30; i++)
            {
                bytAlbum[j] = Info[i];
                position++;
                j++;
            }
            currentIndex = position;
            mp3Info.Album = Mp3FileHelper.byteToString(bytAlbum);
            //获取年
            str = null;
            j = 0;
            byte[] bytYear = new byte[4];//将年部分读到一个单独的数组中
            for (i = currentIndex; i < currentIndex + 4; i++)
            {
                bytYear[j] = Info[i];
                position++;
                j++;
            }
            currentIndex = position;
            mp3Info.Year = Mp3FileHelper.byteToString(bytYear);
            //获取注释
            str = null;
            j = 0;
            byte[] bytComment = new byte[28];//将注释部分读到一个单独的数组中
            for (i = currentIndex; i < currentIndex + 25; i++)
            {
                bytComment[j] = Info[i];
                position++;
                j++;
            }
            currentIndex = position;
            mp3Info.Comment = Mp3FileHelper.byteToString(bytComment);
            //以下获取保留位
            mp3Info.reserved1 = (char)Info[++position];
            mp3Info.reserved2 = (char)Info[++position];
            mp3Info.reserved3 = (char)Info[++position];
            return mp3Info;
        }
        /// <summary>
        /// 将字节数组转换成字符串
        /// </summary>
        /// <param name = "b">字节数组</param>
        /// <returns>返回转换后的字符串</returns>
        public static string byteToString(byte[] b)
        {
            string str = Encoding.Default.GetString(b);
            str = str.Substring(0, str.IndexOf("#CONTENT#") >= 0 ? str.IndexOf("#CONTENT#") : str.Length);//去掉无用字符
            return str;
        }
    }

    public class Mp3FileNewHelper 
    {
        /// <summary>
        /// MP3文件信息
        /// </summary>
        private Mp3Info info;

        /// <summary>
        /// 雪花ID
        /// </summary>
        private static IdWorker SnowId=null;

        /// <summary>
        /// MP3文件路径
        /// </summary>
        private string _mp3FilePath = string.Empty;

        /// <summary>
        /// 返回获取的MP3信息
        /// </summary>
        public Mp3Info Mp3Info
        {
            get
            {
                return info;
            }
        }

        /// <summary>
        /// 获取Shell32的命名空间文件夹
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        private Folder GetShell32NameSpaceFolder(Object folder)
        {
            var shellAppType = Type.GetTypeFromProgID("Shell.Application");
            var shell = Activator.CreateInstance(shellAppType);
            return (Folder)shellAppType.InvokeMember("NameSpace",
            System.Reflection.BindingFlags.InvokeMethod, null, shell, new[] { folder });
        }

        /// <summary>
        /// 获取MP3封面
        /// </summary>
        /// <param name="mp3FilePath">MP3文件路径</param>
        /// <param name="FileId">文件编号</param>
        public string SaveMp3TagImage(string mp3FilePath,long FileId) 
        {
            #region 声明变量

            //返回数据
            string result = string.Empty;

            //使用Id3读取mp3文件
            Id3.Mp3 mp3=new Id3.Mp3(new FileInfo(mp3FilePath),Id3.Mp3Permissions.ReadWrite);

            //当前程序目录
            string AppPath=Directory.GetCurrentDirectory();

            //获取mp3所有标签信息
            var allTags=mp3.GetAllTags();

            //封面图片的扩展名
            string fileExtension = string.Empty;

            //封面保存的路径
            string FullPath = string.Empty;

            //文件保存的全路径
            string FileFullPath = string.Empty;
            #endregion

            #region 判断mp3文件是否有标签信息
            if (allTags == null || allTags.Count() < 0)
                return "";
            #endregion

            #region 获取带有封面图片数据的信息
            if (!allTags.Any(qury => qury.Pictures.Count > 0))
                return "";
            Id3.Id3Tag id3Tag = allTags.First(qury => qury.Pictures.Count > 0);
            if (id3Tag == null)
                return "";
            #endregion

            //获取封面图片
            Id3.Frames.PictureFrame picture = id3Tag.Pictures.First(query => query.PictureData != null && query.PictureData.Length > 0);
            
            //获取扩展名
            fileExtension = HeyRed.Mime.MimeTypesMap.GetExtension(picture.MimeType);
            
            //封面图片保存路径
            FullPath = $@"{AppPath}\Mp3Cover";

            #region 保存封面图片到文件(picture.PictureData是封面图片的二进制信息)
            if (!Directory.Exists(FullPath))
                Directory.CreateDirectory(FullPath);
            FileFullPath = $@"{FullPath}\{FileId.ToString("0#")}.{fileExtension}";
            try
            {
                if(File.Exists(FileFullPath))
                   File.Delete(FileFullPath);
                using (FileStream fs = File.Open(FileFullPath, FileMode.OpenOrCreate))
                {
                    BinaryWriter binWriter = new BinaryWriter(fs);
                    binWriter.Write(picture.PictureData, 0, picture.PictureData.Length);
                    binWriter.Close();
                    fs.Close();
                }
                //id3Tag.Pictures.Remove(picture);
                //bool isUpdate=mp3.UpdateTag(id3Tag);
                result = $@"Mp3Cover\{FileId.ToString("0#")}.{fileExtension}";
            }
            catch(Exception ex) 
            { 
            }
            #endregion

            return result;
        }

        /// <summary>
        /// 获取MP3歌词信息
        /// </summary>
        /// <param name="mp3FilePath"></param>
        public void GetLyricInfo(string mp3FilePath) 
        {
            //InfoProvider chartLyricsProvider = new ChartLyricsInfoProvider();
            //InfoProvider lyrDbLyricsProvider = new LyrDbInfoProvider();
        }

        /// <summary>
        /// 获取MP3文件的信息
        /// </summary>
        /// <param name="mp3FilePath">mp3文件路径</param>
        public Mp3FileNewHelper(string mp3FilePath) 
        {
            #region 声明和初始化

            //shell
            ShellClass sh = new ShellClass();

            //文件信息
            FileInfo fInfo= new FileInfo(mp3FilePath);

            //Folder文件夹
            Folder dir = GetShell32NameSpaceFolder(Path.GetDirectoryName(mp3FilePath));

            //文件夹内容
            FolderItem item = dir.ParseName(Path.GetFileName(mp3FilePath));
            #endregion

            #region 赋值MP3文件
            _mp3FilePath = mp3FilePath;
            info = new Mp3Info();
            if (Mp3FileNewHelper.SnowId == null)
                Mp3FileNewHelper.SnowId = new IdWorker(1, 1);
            info.FileId = Mp3FileNewHelper.SnowId.NextId();//文件ID唯一值
            info.FileName = dir.GetDetailsOf(item, 0);//文件名
            info.FileSize = fInfo.Length;//文件大小
            info.Title = dir.GetDetailsOf(item, 21);//歌名
            info.Artist = dir.GetDetailsOf(item, 13);//歌手名
            info.Album = dir.GetDetailsOf(item, 14);//所属唱片
            
            #region 获取播放时长(单位秒)
            string details = dir.GetDetailsOf(item, -1);
            string duration = Regex.Match(dir.GetDetailsOf(item, -1), "\\d:\\d{2}:\\d{2}").Value;//将时间转换为时分秒          
            info.duration= ToolsHelper.ConvertDuration(duration);
            #endregion

            info.Cover=SaveMp3TagImage(mp3FilePath, info.FileId);//封面
            #endregion
        }

        /// <summary>
        /// mp3文件分析
        /// </summary>
        /// <param name="MP3FilePath">文件绝对路径</param>
        /// <returns>MP3数据库数据</returns>
        public MP3FileInfo AnalyseMp3File() 
        {
            IdWorker SnowId = new IdWorker(1, 1);
            MP3FileInfo SqliteFile = new MP3FileInfo();
            SqliteFile.FileId = SnowId.NextId();
            SqliteFile.FileName = System.IO.Path.GetFileName(_mp3FilePath);
            SqliteFile.Album = info.Album;
            SqliteFile.Comment = info.Comment;
            SqliteFile.Artist = info.Artist;
            SqliteFile.Title = info.Title;
            SqliteFile.duration = info.duration;
            SqliteFile.Year = info.Year;
            SqliteFile.Cover = info.Cover;
            SqliteFile.FileSize = info.FileSize;
            SqliteFile.CreateTime = DateTime.Now;
            SqliteFile.FileFullPath = _mp3FilePath;
            return SqliteFile;
        }
    }
}
