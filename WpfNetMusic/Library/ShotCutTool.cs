using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfNetMusic.Library
{
    public class ShotCutTool
    {
        private static IWshRuntimeLibrary.WshShell shell;

        /// <summary>
        /// 为srcFile文件创建快捷方式
        /// </summary>
        /// <param name="srcFile">待创建快捷方式的文件</param>
        /// <param name="linkPath">快捷方式保存完整路径</param>
        /// <param name="arguments">快捷方式传递的参数信息</param>
        /// <param name="description">描述</param>
        /// <param name="hotkey">系统热键</param>
        /// <param name="iconLocation">快捷方式图标路径</param>
        public static void CreateShotCut(string srcFile, string linkPath = null, string arguments = null, string description = null, string hotkey = null, string iconLocation = null)
        {
            if (linkPath == null) linkPath = srcFile;
            linkPath += ".lnk";
            if (File.Exists(linkPath)) File.Delete(linkPath);               // 删除原有的lnk文件

            if (shell == null) shell = new IWshRuntimeLibrary.WshShell();
            IWshRuntimeLibrary.IWshShortcut shotcut = shell.CreateShortcut(linkPath);   // 创建一个指定名称路径的lnk
            shotcut.TargetPath = srcFile;                                               // 待创建链接的原文件

            if (arguments != null) shotcut.Arguments = arguments;            // 传递参数
            if (description != null) shotcut.Description = description;     // 链接描述
            if (hotkey != null) shotcut.Hotkey = hotkey;                    // 全局热键， 如："CTRL+SHIFT+N"
            if (iconLocation != null) shotcut.IconLocation = iconLocation;  // 设置Icon图标

            shotcut.Save();                     // 保存link
        }

        /// <summary>
        /// 获取快捷方式的链接地址
        /// </summary>
        /// <param name="linkPath">快捷方式路径</param>
        /// <returns></returns>
        public static string GetTargetPath(string linkPath)
        {
            if (shell == null) shell = new IWshRuntimeLibrary.WshShell();
            IWshRuntimeLibrary.IWshShortcut shotcut = shell.CreateShortcut(linkPath);   // 创建一个指定名称路径的lnk

            string targetPath = shotcut.TargetPath;
            if (targetPath == null) targetPath = "";
            return targetPath;
        }
    }
}
