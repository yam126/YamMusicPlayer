   M S B A M L   `   `   `  ���� F  BWpfNetMusic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:WpfNetMusicWpfNetMusic      WpfNetMusic.MainWindow   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  %localclr-namespace:WpfNetMusic  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  -     �Name$  MainWindowHandle�� � Title$ 
MainWindow��$	��900��$	��650�� � Icon$ /Image/Logo.png�� � WindowStyle$
 None=� � 	Resources    DataGridPopMenu  SongListPopMenu  DiscAnimation g� RepeatBehavior g� 	BeginTime �� 
TargetName �� TargetProperty	 W� From	
 W� To g� Duration��%�  &	        &	 j     &	     �� .@�m� $��MoveSongList��-   $��移动到歌单��m� $��DeleteMusic��-   $��删除歌曲���� .@�m� $��AddSongList��-   $��添加歌单��m� $��RenameSongList��-   $��重命名歌单��m� $��DeleteSongList��-   $��删除歌单���� $ Forever��.��W�  00:00:00$ AngleRotate��$ Angle�	 0
 360$ 00:00:02D�*  $Microsoft.Xaml.Behaviors.Interaction   	Behaviors $   WpfNetMusic.WindowDragBehavior  .���  � RowDefinitions �� $��60 ��� $��1* �.+�� -   $��0��$��0�����#4791EB � Cursor$
 Hand�� ��  � ColumnDefinitions �� $��1* ��� $��2* ��� $��5* ��� $
��0.5* ��� $
��0.5* ��� $
��0.5* ��� $
��0.5* �.+��� $��0��$��0��$��45��$��45��$��10,0,0,0q�$
��Left=�$��Center=�$��/Image/Logo.png��$
��Fill=��� $��1��$	��175��$��30��$��10,0,0,0q�$
��Left=�$��Center=�$��Microsoft YaHei UI)�$��18(�$
��Bold"�
�������$��YAM Music Player��$
��Wrap=��� -    ��Name$ 	txtSearch��$��2��$	��175��$��20��$��30,0,0,0q�$	��8,0q�$
��Left=�$��Center=�$
��Left=�$��Center=�$�� �� �� TextAlignment$
 Left=� �� TextWrapping$
 Wrap=�.��   HintText j� 
AlignmentX �� Opacity j� Stretch j� TileMode !� Visual �� 
TargetType �� Triggers P� Property P� Value �� Property �� Value��%K  &	       '�� w     7  7  '�� �     !� $
 Left=�$ 0Q�$
 None=�$
 None=� �� $��11(�$��Italic$����	#FFCB3D3D$��双击进入选择���� " �� P�  � �� Z� . ���  � ��8   P�  � ��$  ��. ���  � ��8  �� " ��.���  �@��CornerRadius$ 11����  � �� �#c1d0dc.������ $
 0.26Q� �� Color$ White��.���� -	    ��Name$ txtSearchHit��$��2��$��42,0,0,0q�$
��Left=�$��Center=�$��Italic$����	#FFEEDCDC$��搜索当前歌单音乐��$
��Wrap=��� -
     ��Name$  btnMainMenu��$��3��$��20��$��20��$��3,5,0,0q�$
 Hand��! J� OpacityMask! �	#FFF7F7F7$��/Image/WhiteMenuIcon.png���� -   $��4��$��20��$��20��$��3,5,0,0q�$
 Hand��! �	#FFF7F7F7$ ��/Image/MinCloseIcon-01.png��$
��Fill=��� -   $  NormalMaxBtn��$��5��$��20��$��20��$��3,5,0,0q�$
 Hand��! �	#FFF7F7F7$��/Image/MaxIcon-01.png��$
��Fill=��� -   $��6��$��20��$��20��$��3,5,0,0q�$
 Hand��! �	#FFF7F7F7$��/Image/CloseIcon-01.png��$
��Fill=�� $��1��$��0�� �� $��1* ��� $
��0.2* � �� .+��  ��  �� $
��0.2* ��� $��1* �.+��� $��0��$��0�����	#FF305EF0$��0,0,1,0q�._��� .+�� $��0,10,0,0q� �� $��51 � �� $
��0.3* ��� .+��� $��20��$��20��$��9,7,0,0q�$��/Image/MenuList20x20.png���� $��0��$��1��$��33��$��9,20,0,5q�$
��Left=�$��Center=�$��17(�$��歌单列表��(� .���� -   " ��Name$" SongListScrollView��$��85��$	��170��$��5,10,5,0q����	#FFFAFAFA$��Hidden=�$��Hidden=�.���� # �� Orientation$# Vertical=�.+��� -   $ ��Name$$ 
lbSongList��$��85��$	��170��$��0,0,0,0q�% � ContextMenu#% [ $��Hidden=�$��Visible=�& �� SelectedItem$& lbSongList_Selected������ *	SongLists+���� .R��� $ songListItem��-   
' � Tag' �� *
SongListId+���� *SongListName+� -   ( �Name$( 	MainPanel��$��0��$��1��$y�Visible=� ��  �� $
��0.2* ��� $��1* �.+�� $��0��$��0�� �� ��  �� .+�� $��0��$��0�� ��  �� $	��21* ��� $	��10* ��� $	��22* ��� $	��25* �.+�(� $��0��$��0��.��9!clr-namespace:WpfNetMusic.LibraryWpfNetMusic.Library  �� -local!clr-namespace:WpfNetMusic.Library  -   $ txtFolderPath��$	��123��$��16��$��5,1,5,0q�$��5,2,0,0q�$
��Left=�$��Center=�*   $WpfNetMusic.Library.WatermarkService)   IsWatermarkEnabled$
) True��*   	Watermark$* 请输入MP3存放路径��$��Microsoft YaHei UI Light)�$��10(����	#FF848181+ �� 
IsReadOnly$
+ True��  TextBoxStyle1#���  .������ $	 0.3Q�$ 	#7FDDDCDC��.��(� $��0��$��1��.���  ��  �� .+��� -   $  btnBrowseFolder��$��0��$��0��$��75��$��25��$��3,2,3,0q�$
 Hand��$��/Image/BrowseFolder.png���� -   $ CurrentSongList��$��0��$��2��$��Center=�$��Center=�$��10(�$��歌单名称:��$��WordEllipsis=�$��NoWrap=�� $��0��$��3�� �� ��  �� .+��� $��0��$��0��$��Center=�$��Center=�$��12(�$=��7提示:可以将MP3拖拽至列播放列表添加文件��� $��1��$��0�� ��  �� .+��� $��0��$��0��$��Center=�$��Center=�$��12(�$=��7提示:可以将列表歌曲拖拽至歌单移动文件��� -   , �Name$, popup��- J� 	AllowDrop$
- True��. � AllowsTransparency$
. True��/ � HorizontalOffset$/ 0��$��False��$��Relative=�$
��Fade=�0 � 	StaysOpen$
0 True��1 � VerticalOffset$1 0��2 � PlacementTarget2 �� 3 �� ElementName$3 MainWindowHandle��.���� 
������ 
������ $��1q�._��� $��50��$��50��.+��� -   $  
PopupImage��$��/Image/NoCover.png��&   System.Windows.Controls.DataGrid  -   4  Name$4 dg_MusicList��$��1��$��0��$
- True��
������ 5   CanUserResizeColumns$
5 True��6   CanUserSortColumns$
6 True��#% [  ���� *	MusicList+7   	CellStyle7 �� *  $System.Windows.Controls.DataGridCell"  .�-   ��  � �� �� 8 �� 
TargetType"8  .R��� 9 ��Name$9 Bg��
������ : � UseLayoutRounding$
: True��._��� $��Center=�$��Center=� P�  �@ 
IsSelected$
 True��. ���  � ��
 ���� ��  � �� �#000000;   Columns; 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn<   Width5  /System.Windows.Controls.DataGridLengthConverter$
< auto =   CanUserResize$
= True��>   CanUserSort$
> True��?   Header$? 序号��@   SortMemberPath$@ Index��1	  +System.Windows.Controls.DataGridBoundColumnA 	  BindingA �� *Index+  $
< auto $
= True��$
> True��$? 编号��$@ FileId��A �� *FileId+  $
< auto $
= True��$
> True��$? 标题��$@ Title��A �� *Title+  $
< auto $
= True��$
> True��$? 歌手��$@ Artist��A �� *Artist+  $
< auto $
= True��$
> True��$? 所属唱片��$@ Album��A �� *Album+  $
< auto $
= True��$
> True��$? 时间��$@ 
CreateTime��A �� *
CreateTime+�� -   B ��Name$B NoFile��$��1��$��0��$��Center=�$# Vertical=�$y�Hidden=�.+��� $	��350��$	��350��$��0,10,0,0q�$��/Image/NoMusic.png��� -   $( PlayerPanel��$��0��$��1��C � ShowGridLines$C False��$y�Hidden=� ��  �� $��0.09* ��� $��1* �.+��� $��0��$��0��$# 
Horizontal=�.+��� -   $��50��$��30��$��5,0,0,0q�$
 Hand��$��/Image/arrows01.png��(� .���� $��5,3,0,0q�$��30(����#2792C1$��返回列表��� $��1��$��0��$C False�� ��  �� $��1* ��� $��1* �.+�� $��0��$��0��$C False�� ��  �� .+�(� .���  ��  �� .+�r� -   D r�Name$D mediaPlayer��$��0��$��0��E r� LoadedBehavior$E Manual=��� $��30��$��30��$��5,0,5,0q�$��/Image/disc.png���� -   $  
MusicCover��$��20��$��20��$��7,0,7,0q�F J� RenderTransformOrigin$F 0.5,0.5&�$��/Image/NoCover.png��$
��Fill=�G J� ClipG �� H �� RadiusX$	H 180Q�I �� RadiusY$	I 180Q�J �� Rect$J 	0,0,20,20�z��� -   AngleRotate���� -   $  Stylus��$��15��$��25��$��
12,-15,0,0q�$��/Image/stylus01.png��z�V� .~��� �� �� K �� AngleK �� *StylusAngle+T� (� $��0��$��1��.���� $# Vertical=�.+��� -    $ tbSongTitle��L �� Padding$L 3,0,3,0q�$��Center=�$��3(�$
��Bold"����� M �� 
StartPoint$M 0.5,0&�N �� EndPoint$N 0.5,1&�.��� O � Color$O 	#FF8FC5C3��� P � Offset$P 1Q�$O 	#FF1D9E9A���� -!   $ tbSongFileId��$L 3,0,3,0q�$��1(�$
��Bold"����� $M 0.5,0&�$N 0.5,1&�.��� $O 	#FF8FC5C3��� $P 1Q�$O 	#FF1D9E9A���� -"   $ tbSongFileName��$L 3,0,3,0q�$��1(�$
��Bold"����� $M 0.5,0&�$N 0.5,1&�.��� $O 	#FF8FC5C3��� $P 1Q�$O 	#FF1D9E9A���� -#   $ tbSongAlbum��$L 3,0,3,0q�$��1(�$
��Bold"����� $M 0.5,0&�$N 0.5,1&�.��� $O 	#FF8FC5C3��� $P 1Q�$O 	#FF1D9E9A���� -$   $ tbSongArtist��$L 3,0,3,0q�$��1(�$
��Bold"����� $M 0.5,0&�$N 0.5,1&�.��� $O 	#FF8FC5C3��� $P 1Q�$O 	#FF1D9E9A���� -%   $ tbSongFileSize��$L 3,0,3,0q�$��1(�$
��Bold"����� $M 0.5,0&�$N 0.5,1&�.��� $O 	#FF8FC5C3��� $P 1Q�$O 	#FF1D9E9A���� -&   $ tbSongDuration��$L 3,0,3,0q�$��1(�$
��Bold"����� $M 0.5,0&�$N 0.5,1&�.��� $O 	#FF8FC5C3��� $P 1Q�$O 	#FF1D9E9A���� $��1��$��0�����	#FF305EF0$��0,1,0,0q�._��  �� �� $
��0.5* � �� $��1* ��� $��3* ��� $��1* �.+�� $��0��$��0��$C False�� ��  �� $��1* ��� $��2* �.+��� -'   $  	MiniCover��$��0��$��0��$��35��$��35��$��8,5,0,5q�$��/Image/NoCover.png��$
��Fill=�G �� $H 5Q�$I 5Q�$J 	0,0,35,35��� $��0��$��1��$# Vertical=�.+��� -(   $ tbMiniSongTitle��$��2,7,0,0q�$��18(�$
��Bold"�$�� ������ $M 0.5,0&�$N 0.5,1&�.��� $O 	#FF35CB53��� $P 1Q�$O 	#FF1166BD���� -)   $ tbMiniSongArtist��$��2,6,0,0q�$��15(�$
��Bold"�$�� ����
� .��� $P 0.162Q�$O 	#FF35CB53��� $P 1Q�$O 	#FFBD93D2��� $P 0.009Q�$O 	#FF3F8A47��� $P 0.749Q�$O 	#FFD4BA56��� $��0��$��1��$	��310�� ��  �� �� �� �� �� �� .+��� -*   $  BtnPlaySequence��$��0��$��0��$��25��$��25��$
 Hand��$��/Image/LoopPlayIcon.png��Q � ToolTip$Q  ���� -+   $  PrevoiusBtn��$��0��$��1��$��25��$��25��$
 Hand��$��/Image/PreviousBtn.png���� -,   $��0��$��2��$��2��$��40��$��40�����	#FFF1EBEB���	#FFC8BDBD ��%X   '��       �� " ��.���  �@��CornerRadius$ 25����  � �� �	#FFA1BACE�� --   $  PlayButtonImage��$��0��$��2��$��2��$��25��$��25��$��5,0,0,0q�$
 Hand��$��/Image/PlayIcon.png���� -.   $  
NextButton��$��0��$��4��$��25��$��25��$
 Hand��$��/Image/NextBtnIcon.png���� -/   $��0��$��5��$��Center=�$��Center=�$
 Hand��$��Microsoft YaHei UI)�$��20(�$
��Bold"�$	��词���� $��0��$��2��$# 
Horizontal=�.+��� -0   $  
VolumeIcon��$��25��$��25��$��5,0,0,0q�$
 Hand��$��/Image/VolumeIcon.png���� -1   R ��Name$R SliderVolume��$	��135��$��20��$��3,5,0,0q�  SliderStyle#���  � $��1��$��1��$	��516�� ��  �� $
��0.2* ��� $��1* ��� $
��0.2* �.+��� -2   $ CurrentPlayTime��$��0��$��0��$��Center=�$��Center=����	#FFC5C2C2$��00:00:00���� -3   $R CurrentMusicSlider��$��0��$��1��$��0,5,0,0q�#���  �� -4   $ CurrentMusicDuration��$��0��$��2��$��Center=�$��Center=����	#FFC5C2C2$��00:00:00��r� $��1��$	��100��$��33��$��	99,22,0,0q�$
��Left=�$	��Top=�