   M S B A M L   `   `   `  ���� F  BWpfNetMusic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:WpfNetMusicWpfNetMusic      WpfNetMusic.MoveMusic   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  %localclr-namespace:WpfNetMusic  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  -     � Title$  移动歌曲到歌单��$	��300��$	��180�� � 
ResizeMode$ NoResize=� � WindowStyle$
 None=�����  �� 
TargetType" �.���  �� Property � �� �� Value ��  �� 
TargetType" �.R��� ���	#FF229DBE$��2q� �� CornerRadius$ 8�� J� Opacity$ 1Q����� 	 �� 
StartPoint$	 0.5,0&�
 �� EndPoint$
 0.5,1&�.���  � Color$ #FFFFFF���  � Offset$ 1Q�$ #FFFFFF��._��� *  $Microsoft.Xaml.Behaviors.Interaction   	Behaviors $   WpfNetMusic.WindowDragBehavior  .���  � RowDefinitions �� $
��0.2* ��� $��1* � � ColumnDefinitions �� .+�� $��0��$��0�����#4791EB ��  �� $��1* ��� $��1* �.+���  �� Padding$ 10q�$
��Left=�$��Center=�$��15(����	#FFFCFCFC$��移动歌曲到歌单���� -   $��0��$��1��$��20��$��20��$��0,0,20,0q�$��Right=� � Cursor$
 Hand��$��/Image/CloseIcon-01.png��� $��1��$��0�� �� $��1* ��� $
��0.5* � �� .+��� -    ��Name$ 
cbSongList��$	��265��$��35��$��Center=�$��Center=� �� DisplayMemberPath$ SongListName��$��21(� �� SelectedValue$ 
SongListId��   Combobox_Base#��[  ���� *	SongLists+ �� SelectedItem �� .R��� 
 � Tag �� *
SongListId+���� *SongListName+ � 	Resources ��%V   '��       �� " ��.���  �@��CornerRadius$ 11����  � �� �#c1d0dc� $��1��$��0�� ��  �� �� .+��� -    ��Name$ 	btnCancel��$��1��$��0��$��95��$��31�����	#FF843D7D$��取消��$
 Hand��$��15(�
������� ��%U   '��       �� " ��.���  �@��CornerRadius$ 8����  � �� �#c1d0dc�� -   $ 
btnConfirm��$��1��$��1��$��95��$��31�����	#FF3B50B5$��确定��$
 Hand��$��15(�
������� ��%U   '��       �� " ��.���  �@��CornerRadius$ 8����  � �� �#c1d0dc