﻿#pragma checksum "..\..\..\MainWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7C15B85A536F7B5AB96ECED09CA2880BCD01A726"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using HandyControl.Controls;
using HandyControl.Data;
using HandyControl.Expression.Media;
using HandyControl.Expression.Shapes;
using HandyControl.Interactivity;
using HandyControl.Media.Animation;
using HandyControl.Media.Effects;
using HandyControl.Properties.Langs;
using HandyControl.Themes;
using HandyControl.Tools;
using HandyControl.Tools.Converter;
using HandyControl.Tools.Extension;
using Microsoft.Xaml.Behaviors;
using Microsoft.Xaml.Behaviors.Core;
using Microsoft.Xaml.Behaviors.Input;
using Microsoft.Xaml.Behaviors.Layout;
using Microsoft.Xaml.Behaviors.Media;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using WpfNetMusic;
using WpfNetMusic.Library;


namespace WpfNetMusic {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 10 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal WpfNetMusic.MainWindow MainWindowHandle;
        
        #line default
        #line hidden
        
        
        #line 114 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtSearch;
        
        #line default
        #line hidden
        
        
        #line 166 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtSearchHit;
        
        #line default
        #line hidden
        
        
        #line 176 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image btnMainMenu;
        
        #line default
        #line hidden
        
        
        #line 196 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image NormalMaxBtn;
        
        #line default
        #line hidden
        
        
        #line 270 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer SongListScrollView;
        
        #line default
        #line hidden
        
        
        #line 280 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox lbSongList;
        
        #line default
        #line hidden
        
        
        #line 312 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid MainPanel;
        
        #line default
        #line hidden
        
        
        #line 346 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtFolderPath;
        
        #line default
        #line hidden
        
        
        #line 376 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image btnBrowseFolder;
        
        #line default
        #line hidden
        
        
        #line 390 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock CurrentSongList;
        
        #line default
        #line hidden
        
        
        #line 437 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.Popup popup;
        
        #line default
        #line hidden
        
        
        #line 452 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image PopupImage;
        
        #line default
        #line hidden
        
        
        #line 458 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dg_MusicList;
        
        #line default
        #line hidden
        
        
        #line 544 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel NoFile;
        
        #line default
        #line hidden
        
        
        #line 561 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid PlayerPanel;
        
        #line default
        #line hidden
        
        
        #line 624 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MediaElement mediaPlayer;
        
        #line default
        #line hidden
        
        
        #line 637 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image MusicCover;
        
        #line default
        #line hidden
        
        
        #line 651 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.RotateTransform AngleRotate;
        
        #line default
        #line hidden
        
        
        #line 657 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image Stylus;
        
        #line default
        #line hidden
        
        
        #line 682 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbSongTitle;
        
        #line default
        #line hidden
        
        
        #line 695 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbSongFileId;
        
        #line default
        #line hidden
        
        
        #line 707 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbSongFileName;
        
        #line default
        #line hidden
        
        
        #line 719 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbSongAlbum;
        
        #line default
        #line hidden
        
        
        #line 731 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbSongArtist;
        
        #line default
        #line hidden
        
        
        #line 743 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbSongFileSize;
        
        #line default
        #line hidden
        
        
        #line 755 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbSongDuration;
        
        #line default
        #line hidden
        
        
        #line 804 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image MiniCover;
        
        #line default
        #line hidden
        
        
        #line 824 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbMiniSongTitle;
        
        #line default
        #line hidden
        
        
        #line 837 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbMiniSongArtist;
        
        #line default
        #line hidden
        
        
        #line 871 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image BtnPlaySequence;
        
        #line default
        #line hidden
        
        
        #line 881 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image PrevoiusBtn;
        
        #line default
        #line hidden
        
        
        #line 907 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image PlayButtonImage;
        
        #line default
        #line hidden
        
        
        #line 919 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image NextButton;
        
        #line default
        #line hidden
        
        
        #line 946 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image VolumeIcon;
        
        #line default
        #line hidden
        
        
        #line 954 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider SliderVolume;
        
        #line default
        #line hidden
        
        
        #line 977 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock CurrentPlayTime;
        
        #line default
        #line hidden
        
        
        #line 986 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider CurrentMusicSlider;
        
        #line default
        #line hidden
        
        
        #line 996 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock CurrentMusicDuration;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.7.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WpfNetMusic;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.7.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.7.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.MainWindowHandle = ((WpfNetMusic.MainWindow)(target));
            
            #line 15 "..\..\..\MainWindow.xaml"
            this.MainWindowHandle.Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            
            #line 16 "..\..\..\MainWindow.xaml"
            this.MainWindowHandle.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Window_MouseDown);
            
            #line default
            #line hidden
            
            #line 17 "..\..\..\MainWindow.xaml"
            this.MainWindowHandle.MouseMove += new System.Windows.Input.MouseEventHandler(this.MainWindowHandle_MouseMove);
            
            #line default
            #line hidden
            
            #line 18 "..\..\..\MainWindow.xaml"
            this.MainWindowHandle.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.MainWindowHandle_MouseUp);
            
            #line default
            #line hidden
            
            #line 19 "..\..\..\MainWindow.xaml"
            this.MainWindowHandle.StateChanged += new System.EventHandler(this.Window_StateChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 27 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MoveSongList_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 31 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.DeleteMusic_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 39 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddSongList_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 43 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.RenameSongList_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 47 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.DeleteSongList_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 77 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Grid_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 8:
            this.txtSearch = ((System.Windows.Controls.TextBox)(target));
            
            #line 124 "..\..\..\MainWindow.xaml"
            this.txtSearch.KeyDown += new System.Windows.Input.KeyEventHandler(this.txtSearch_KeyDown);
            
            #line default
            #line hidden
            
            #line 125 "..\..\..\MainWindow.xaml"
            this.txtSearch.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.txtSearch_MouseDown);
            
            #line default
            #line hidden
            
            #line 126 "..\..\..\MainWindow.xaml"
            this.txtSearch.MouseEnter += new System.Windows.Input.MouseEventHandler(this.txtSearch_MouseEnter);
            
            #line default
            #line hidden
            
            #line 127 "..\..\..\MainWindow.xaml"
            this.txtSearch.MouseLeave += new System.Windows.Input.MouseEventHandler(this.txtSearch_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 9:
            this.txtSearchHit = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 10:
            this.btnMainMenu = ((System.Windows.Controls.Image)(target));
            
            #line 182 "..\..\..\MainWindow.xaml"
            this.btnMainMenu.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.btnMainMenu_MouseDown);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 191 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.MinIcon_MouseDown);
            
            #line default
            #line hidden
            return;
            case 12:
            this.NormalMaxBtn = ((System.Windows.Controls.Image)(target));
            
            #line 202 "..\..\..\MainWindow.xaml"
            this.NormalMaxBtn.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.MaxIcon_MouseDown);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 212 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Close_MouseDown);
            
            #line default
            #line hidden
            return;
            case 14:
            this.SongListScrollView = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 15:
            this.lbSongList = ((System.Windows.Controls.ListBox)(target));
            
            #line 286 "..\..\..\MainWindow.xaml"
            this.lbSongList.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.lbSongList_MouseDown);
            
            #line default
            #line hidden
            
            #line 287 "..\..\..\MainWindow.xaml"
            this.lbSongList.MouseWheel += new System.Windows.Input.MouseWheelEventHandler(this.ListBox_MouseWheel);
            
            #line default
            #line hidden
            return;
            case 17:
            this.MainPanel = ((System.Windows.Controls.Grid)(target));
            return;
            case 18:
            this.txtFolderPath = ((System.Windows.Controls.TextBox)(target));
            return;
            case 19:
            this.btnBrowseFolder = ((System.Windows.Controls.Image)(target));
            
            #line 383 "..\..\..\MainWindow.xaml"
            this.btnBrowseFolder.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.btnBrowseFolder_MouseDown);
            
            #line default
            #line hidden
            return;
            case 20:
            this.CurrentSongList = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 21:
            this.popup = ((System.Windows.Controls.Primitives.Popup)(target));
            return;
            case 22:
            this.PopupImage = ((System.Windows.Controls.Image)(target));
            return;
            case 23:
            this.dg_MusicList = ((System.Windows.Controls.DataGrid)(target));
            
            #line 466 "..\..\..\MainWindow.xaml"
            this.dg_MusicList.DragEnter += new System.Windows.DragEventHandler(this.dg_MusicList_DragEnter);
            
            #line default
            #line hidden
            
            #line 467 "..\..\..\MainWindow.xaml"
            this.dg_MusicList.Drop += new System.Windows.DragEventHandler(this.dg_MusicList_Drop);
            
            #line default
            #line hidden
            
            #line 469 "..\..\..\MainWindow.xaml"
            this.dg_MusicList.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.dg_MusicList_MouseDoubleClick);
            
            #line default
            #line hidden
            
            #line 470 "..\..\..\MainWindow.xaml"
            this.dg_MusicList.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.dg_MusicList_MouseDown);
            
            #line default
            #line hidden
            
            #line 471 "..\..\..\MainWindow.xaml"
            this.dg_MusicList.Sorting += new System.Windows.Controls.DataGridSortingEventHandler(this.dg_MusicList_Sorting);
            
            #line default
            #line hidden
            return;
            case 25:
            this.NoFile = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 26:
            this.PlayerPanel = ((System.Windows.Controls.Grid)(target));
            return;
            case 27:
            
            #line 582 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Image_MouseDown);
            
            #line default
            #line hidden
            return;
            case 28:
            this.mediaPlayer = ((System.Windows.Controls.MediaElement)(target));
            return;
            case 29:
            this.MusicCover = ((System.Windows.Controls.Image)(target));
            return;
            case 30:
            this.AngleRotate = ((System.Windows.Media.RotateTransform)(target));
            return;
            case 31:
            this.Stylus = ((System.Windows.Controls.Image)(target));
            return;
            case 32:
            this.tbSongTitle = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 33:
            this.tbSongFileId = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 34:
            this.tbSongFileName = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 35:
            this.tbSongAlbum = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 36:
            this.tbSongArtist = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 37:
            this.tbSongFileSize = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 38:
            this.tbSongDuration = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 39:
            this.MiniCover = ((System.Windows.Controls.Image)(target));
            return;
            case 40:
            this.tbMiniSongTitle = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 41:
            this.tbMiniSongArtist = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 42:
            this.BtnPlaySequence = ((System.Windows.Controls.Image)(target));
            
            #line 877 "..\..\..\MainWindow.xaml"
            this.BtnPlaySequence.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.BtnPlaySequence_MouseDown);
            
            #line default
            #line hidden
            return;
            case 43:
            this.PrevoiusBtn = ((System.Windows.Controls.Image)(target));
            
            #line 887 "..\..\..\MainWindow.xaml"
            this.PrevoiusBtn.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.PrevoiusBtn_MouseDown);
            
            #line default
            #line hidden
            return;
            case 44:
            
            #line 898 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 45:
            this.PlayButtonImage = ((System.Windows.Controls.Image)(target));
            
            #line 915 "..\..\..\MainWindow.xaml"
            this.PlayButtonImage.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.PlayButtonImage_MouseDown);
            
            #line default
            #line hidden
            return;
            case 46:
            this.NextButton = ((System.Windows.Controls.Image)(target));
            
            #line 925 "..\..\..\MainWindow.xaml"
            this.NextButton.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.NextButton_MouseDown);
            
            #line default
            #line hidden
            return;
            case 47:
            
            #line 936 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.TextBlock)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.TextBlock_MouseDown);
            
            #line default
            #line hidden
            return;
            case 48:
            this.VolumeIcon = ((System.Windows.Controls.Image)(target));
            
            #line 951 "..\..\..\MainWindow.xaml"
            this.VolumeIcon.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.VolumeIcon_MouseDown);
            
            #line default
            #line hidden
            return;
            case 49:
            this.SliderVolume = ((System.Windows.Controls.Slider)(target));
            
            #line 959 "..\..\..\MainWindow.xaml"
            this.SliderVolume.AddHandler(System.Windows.Controls.Primitives.Thumb.DragCompletedEvent, new System.Windows.Controls.Primitives.DragCompletedEventHandler(this.SliderVolume_DragCompleted));
            
            #line default
            #line hidden
            return;
            case 50:
            this.CurrentPlayTime = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 51:
            this.CurrentMusicSlider = ((System.Windows.Controls.Slider)(target));
            
            #line 991 "..\..\..\MainWindow.xaml"
            this.CurrentMusicSlider.AddHandler(System.Windows.Controls.Primitives.Thumb.DragCompletedEvent, new System.Windows.Controls.Primitives.DragCompletedEventHandler(this.CurrentMusicSlider_DragCompleted));
            
            #line default
            #line hidden
            
            #line 992 "..\..\..\MainWindow.xaml"
            this.CurrentMusicSlider.AddHandler(System.Windows.Controls.Primitives.Thumb.DragStartedEvent, new System.Windows.Controls.Primitives.DragStartedEventHandler(this.CurrentMusicSlider_DragStarted));
            
            #line default
            #line hidden
            
            #line 993 "..\..\..\MainWindow.xaml"
            this.CurrentMusicSlider.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.CurrentMusicSlider_ValueChanged);
            
            #line default
            #line hidden
            return;
            case 52:
            this.CurrentMusicDuration = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.7.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            System.Windows.EventSetter eventSetter;
            switch (connectionId)
            {
            case 16:
            
            #line 295 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.TextBlock)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.TextBlock_MouseLeftButtonDown);
            
            #line default
            #line hidden
            
            #line 296 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.TextBlock)(target)).MouseMove += new System.Windows.Input.MouseEventHandler(this.songListItem_MouseMove);
            
            #line default
            #line hidden
            
            #line 297 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.TextBlock)(target)).MouseRightButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.TextBlock_MouseRightButtonDown);
            
            #line default
            #line hidden
            break;
            case 24:
            eventSetter = new System.Windows.EventSetter();
            eventSetter.Event = System.Windows.UIElement.MouseDownEvent;
            
            #line 474 "..\..\..\MainWindow.xaml"
            eventSetter.Handler = new System.Windows.Input.MouseButtonEventHandler(this.DataGridCell_MouseDown);
            
            #line default
            #line hidden
            ((System.Windows.Style)(target)).Setters.Add(eventSetter);
            eventSetter = new System.Windows.EventSetter();
            eventSetter.Event = System.Windows.UIElement.MouseLeftButtonDownEvent;
            
            #line 475 "..\..\..\MainWindow.xaml"
            eventSetter.Handler = new System.Windows.Input.MouseButtonEventHandler(this.DataGridCell_MouseLeftButtonDown);
            
            #line default
            #line hidden
            ((System.Windows.Style)(target)).Setters.Add(eventSetter);
            break;
            }
        }
    }
}

