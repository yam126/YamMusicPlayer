using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Threading.Tasks;
using WpfNetMusic.Model;
using System.Xml.Serialization;

namespace WpfNetMusic.Library
{
    /// <summary>
    /// 设置帮助
    /// </summary>
    public class SettingHelper
    {
        /// <summary>
        /// 应用程序设置文件
        /// </summary>
        private static string SettingFileName = "appSetting.xml";

        /// <summary>
        /// 设置文件是否存在
        /// </summary>
        /// <returns></returns>
        public static bool IsHaveSettingFile() 
        {
            //应用程序所在目录
            string AppPath = Directory.GetCurrentDirectory();

            //设置文件全局路径
            string SettingFileFullPath = string.Empty;

            SettingFileFullPath = $@"{AppPath}\{SettingFileName}";
            return File.Exists(SettingFileFullPath);
        }

        /// <summary>
        /// 保存设置
        /// </summary>
        /// <param name="setting">全局设置</param>
        /// <returns>成功或者失败</returns>
        public static bool SaveSetting(GlobalSetting setting,out string Message) 
        {
            #region 声明变量

            //设置文件全局路径
            string SettingFileFullPath = string.Empty;

            //应用程序所在目录
            string AppPath = Directory.GetCurrentDirectory();

            //返回值
            bool result = false;

            //错误消息
            Message = string.Empty;
            #endregion

            #region 检查设置信息
            if (setting==null)
            {
                Message = "设置信息不能为空";
                return false;
            }
            #endregion

            #region 判断设置文件是否存在,存在先删后存,覆盖文件
            SettingFileFullPath = $@"{AppPath}\{SettingFileName}";
            if (File.Exists(SettingFileFullPath))
                File.Delete(SettingFileFullPath);
            #endregion

            #region 保存xml文件

            //声明文档
            XDocument doc = new XDocument();

            //根节点
            XElement root = new XElement("AppSetting");

            #region 路径设置保存
            if(!string.IsNullOrEmpty(SettingFileFullPath))
            {
                XElement FilePath = new XElement("FilePath");
                FilePath.Value = $@"<![CDATA[{SettingFileFullPath}]]>";
                root.Add(FilePath);
            }
            #endregion

            #region 应用程序退出设置
            if (setting.closeSetting == null)
            {
                setting.closeSetting = new CloseSetting()
                {
                    IsMinTaskBar = false,
                    QuitSystem=false
                };
            }
            XElement xCloseSetting = new XElement("CloseSetting");
            if(setting.closeSetting != null) 
            {
                xCloseSetting.Add(new XElement("IsMinTaskBar") 
                {
                    Value=setting.closeSetting.IsMinTaskBar?"True":"False"
                });
                xCloseSetting.Add(new XElement("QuitSystem")
                {
                    Value = setting.closeSetting.QuitSystem ? "True" : "False"
                });
            }
            root.Add(xCloseSetting);
            #endregion

            #region 保存设置
            doc.Add(root);
            try
            {
                doc.Save(SettingFileFullPath);
                result = true;
            }
            catch(Exception exp) 
            {
                Message=exp.Message;
                result=false;
            }
            #endregion

            #endregion

            return result;
        }

        /// <summary>
        /// 读取设置信息
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <returns>成功或失败</returns>
        public static bool LoadingSetting(out string message) 
        {
            #region 声明和初始化

            //设置文件全局路径
            string SettingFileFullPath = string.Empty;

            //应用程序所在目录
            string AppPath = Directory.GetCurrentDirectory();

            //返回值
            bool result = false;

            //错误消息
            message= string.Empty;

            //XML操作类
            XDocument doc = null;
            #endregion

            if (Global.setting == null)
                Global.setting = new GlobalSetting();

            #region 判断文件是否存在
            SettingFileFullPath = $@"{AppPath}\{SettingFileName}";
            if (!File.Exists(SettingFileFullPath)) 
                return false;
            #endregion

            #region 读取文件
            try 
            {
                doc = XDocument.Load(SettingFileFullPath);
            }
            catch(Exception exp) 
            {
                message = exp.Message;
                return false;
            }
            #endregion

            #region 解析文件
            try 
            {
                XElement? root= doc.Root;
                if(root == null) 
                {
                    message = "设置文件缺少根元素";
                    return false;
                }

                #region 读取文件路径
                XElement? FilePath=root.XPathSelectElement($"/{root.Name}/FilePath");
                if(FilePath!=null)
                    Global.setting.MusicFilePath = FilePath.Value;
                #endregion

                #region 读取退出设置
                XElement? CloseSetting = root.XPathSelectElement($"/{root.Name}/CloseSetting");
                Global.setting.closeSetting = new CloseSetting()
                {
                    IsMinTaskBar = false,
                    QuitSystem = false
                };
                if (CloseSetting != null)
                {
                    XElement? xIsMinTaskBar = root.XPathSelectElement($"/{root.Name}/CloseSetting/IsMinTaskBar");
                    XElement? xQuitSystem = root.XPathSelectElement($"/{root.Name}/CloseSetting/QuitSystem");
                    if(xIsMinTaskBar!=null)
                        Global.setting.closeSetting.IsMinTaskBar= xIsMinTaskBar.Value=="True"?true:false;
                    if (xQuitSystem != null)
                        Global.setting.closeSetting.QuitSystem = xQuitSystem.Value == "True" ? true : false;
                }
                #endregion

            }
            catch (Exception exp) 
            { 
                message = exp.Message; 
                return false; 
            }
            #endregion

            return result;
        }
    }
}
