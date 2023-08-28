using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace WpfNetMusic.Library
{
    /// <summary>
    /// 音乐播放核心类
    /// </summary>
    public class MusicPlayer
    {
        /// <summary>
        /// 是否重复播放
        /// </summary>
        public static bool isRepeat = false;//是否重复播放

        /// <summary>
        /// 声明系统播放函数
        /// </summary>
        /// <param name="command"></param>
        /// <param name="buffer"></param>
        /// <param name="bufferSize"></param>
        /// <param name="hwndCallback"></param>
        /// <returns></returns>
        [DllImport("winmm.dll")]
        public static extern Int32 mciSendString(string command, StringBuilder buffer, int bufferSize, IntPtr hwndCallback);


        /// <summary>
        /// 返回对执行状态错误代码的描述
        /// </summary>
        /// <param name="errorCode">mciSendCommand或者mciSendString返回的错误代码</param>
        /// <param name="errorText">对错误代码的描述字符串</param>
        /// <param name="errorTextSize">指定字符串的大小</param>
        /// <returns>如果ERROR Code未知，返回false</returns>
        [DllImport("winmm.dll")]
        static extern bool mciGetErrorString(Int32 errorCode, StringBuilder errorText, Int32 errorTextSize);

        /// <summary>
        /// 播放音乐文件
        /// </summary>
        /// <param name="p_FileName">文件路径</param>
        public static void PlayMusic(string p_FileName,out string message)
        {
            message = string.Empty;
            try
            {
                mciSendString(@"close temp_music", new StringBuilder(), 0, IntPtr.Zero);
                string cmd = @"open """+ p_FileName.Replace("\\", @"\") + "\" alias temp_music";
                int errorstate=mciSendString(cmd, new StringBuilder(), 0, IntPtr.Zero);
                if (errorstate == 0)
                {
                    if (isRepeat)
                    {
                        mciSendString(@"play temp_music repeat", new StringBuilder(), 0, IntPtr.Zero);
                    }
                    else
                    {
                        mciSendString(@"play temp_music", new StringBuilder(), 0, IntPtr.Zero);
                    }
                }
                else
                {
                    StringBuilder errorText = new StringBuilder();
                    mciGetErrorString(errorstate, errorText, 50);
                    message=errorText.ToString();
                }

            }
            catch(Exception exp)
            {
                message= exp.Message;
            }
        }


        /// <summary>
        /// 获取总时长
        /// </summary>
        /// <returns>时长</returns>
        public static int getLength()
        {
            try
            {
                StringBuilder buffer = new StringBuilder();
                mciSendString("status temp_music length ", buffer, 128, IntPtr.Zero);
                Debug.WriteLine(buffer);
                if (String.IsNullOrEmpty(buffer.ToString())) return 0;
                else return Int32.Parse(buffer.ToString().Trim());
            }
            catch
            {
            }
            return 0;
        }
        
        /// <summary>
        /// 获取当前位置
        /// </summary>
        /// <returns>当前位置</returns>
        public static int getPosition()
        {
            try
            {
                StringBuilder buffer = new StringBuilder();
                mciSendString("status temp_music position ", buffer, 128, IntPtr.Zero);
                if (String.IsNullOrEmpty(buffer.ToString())) return 0;
                else return Int32.Parse(buffer.ToString().Trim());
            }
            catch
            {
            }
            return 0;
        }
        
        /// <summary>
        /// 指定位置开始播放 配合进度条使用
        /// </summary>
        /// <param name="pos">指定位置</param>
        public static void setPosition(int pos)
        {
            try
            {
                mciSendString("seek temp_music to " + (pos).ToString(), new StringBuilder(), 0, IntPtr.Zero);
                if (isRepeat)
                {
                    mciSendString(@"play temp_music repeat", new StringBuilder(), 0, IntPtr.Zero);
                }
                else
                {
                    mciSendString(@"play temp_music", new StringBuilder(), 0, IntPtr.Zero);
                }
            }
            catch
            {
            }
        }
        
        /// <summary>
        /// 获取播放状态
        /// </summary>
        /// <returns></returns>
        public static string getStatus()
        {
            try
            {
                //string moveStatus = "";
                //moveStatus = moveStatus.PadLeft(128, Convert.ToChar(" "));
                StringBuilder buffer = new StringBuilder();
                mciSendString("status temp_music mode ", buffer, 128, IntPtr.Zero);
                Debug.WriteLine(buffer.ToString());
                return buffer.ToString();
            }
            catch
            {
            }
            return "";
        }

        /// <summary>
        /// 暂停播放
        /// </summary>
        public static void pause()
        {
            try
            {
                mciSendString("pause temp_music", new StringBuilder(), 0, IntPtr.Zero);
            }
            catch
            {
            }
        }
        
        /// <summary>
        /// 恢复
        /// </summary>
        public static void resume()
        {
            try
            {
                mciSendString("resume temp_music", new StringBuilder(), 0, IntPtr.Zero);
            }
            catch
            {
            }
        }

        /// <summary>
        /// 停止播放
        /// </summary>
        public static void stop()
        {
            try
            {
                mciSendString("stop temp_music", new StringBuilder(), 0, IntPtr.Zero);
            }
            catch
            {
            }
        }
        
        /// <summary>
        /// 重复播放
        /// </summary>
        public static void repeat()
        {
            try
            {
                if (isRepeat)
                {
                    mciSendString("play temp_music repeat", new StringBuilder(), 0, IntPtr.Zero);
                }
                else
                {
                    mciSendString("play temp_music", new StringBuilder(), 0, IntPtr.Zero);
                }

            }
            catch
            {
            }
        }
    }
}
