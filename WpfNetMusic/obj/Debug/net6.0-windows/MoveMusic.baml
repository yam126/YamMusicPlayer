   M S B A M L   `   `   `  ����F  BWpfNetMusic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:WpfNetMusicWpfNetMusic  5          WpfNetMusic.MoveMusic   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5      %localclr-namespace:WpfNetMusic  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      -   5          � Title$  移动歌曲到歌单��5	      $	��300��5
      $	��180��5       � 
ResizeMode$ NoResize=�5       � WindowStyle$
 None=�5      ��5      �� 5   
    �� 
TargetType" �6   .�5      ��  �� Property � �� �� Value 5      �� 5       �� 
TargetType" �6&   .R�5      �� ���	#FF229DBE$��2q�5       �� CornerRadius$ 8��5       J� Opacity$ 1Q�5      ��5      �� 5   "   	 �� 
StartPoint$	 0.5,0&�66   
 �� EndPoint$
 0.5,1&�6I   .��5   &   �  � Color$ #FFFFFF��63   � 5   &    � Offset$ 1Q�63   $ #FFFFFF��6>   5   #   ._�5       �� 5!      5"      5$      5%      *  $Microsoft.Xaml.Behaviors.Interaction   	Behaviors 5'      $   WpfNetMusic.WindowDragBehavior  5(   
   .��5*      �  � RowDefinitions 5+   
   �� 5,      $
��0.2* �6   �� 5-      $��1* �6    � ColumnDefinitions 5/   
   �� 50      .+�53   
   � $��0��54      $��0��55      ���#4791EB 57      �� 58       5:      �� 5;      $��1* �6#   �� 5<      $��1* �6#   .+�5>      ��  �� Padding$ 10q�5?      $
��Left=�5@      $��Center=�5A      $��15(�5B      ���	#FFFCFCFC$��移动歌曲到歌单��5D      �� 5E      -   5        $��0��5F      $��1��5G      $��20��5H      $��20��5I      $��0,0,20,0q�5J      $��Right=�5K       � Cursor$
 Hand��5L      $��/Image/CloseIcon-01.png��5N      5O      � 5Q   
   $��1��6   $��0��6    5R      �� 5S      $��1* �6    �� 5T      $
��0.5* �6     5V      �� 5W      .+�5Y      �� -   5         ��Name$ 
cbSongList��5Z      $	��265��5[      $��35��5\      $��Center=�5]      $��Center=�5^       �� DisplayMemberPath$ SongListName��5_      $��21(�5`       �� SelectedValue$ 
SongListId��5b         Combobox_Base#��[  5c      ��5a      �� *	SongLists+ �� SelectedItem 5d      �� 5e      .R�5f      �� 
 � Tag 6$   �� *
SongListId+��6?   �� *SongListName+5g       � 	Resources 5i      ��%�   '��       �� 5j      " ��6   .�5k      ��  �@��CornerRadius$ 11��69   �� 5l       � �� �#c1d0dc68   5m      5n      5o      � 5p      $��1��6   $��0��6     5q      �� 5r       5t      �� 5u      �� 5v      .+�5x      �� -   5         ��Name$ 	btnCancel��5y      $��1��5z      $��0��5{      $��95��5|      $��31��5}      ���	#FF843D7D$��取消��5�      $
 Hand��5�      $��15(�5�      
������� 5�      ��%�   '��       �� 5�      " ��6    .�5�      ��  �@��CornerRadius$ 8��6=   �� 5�       � �� �#c1d0dc6<   5�      5�      5�      �� 5�      -   5        $ 
btnConfirm��5�      $��1��5�      $��1��5�      $��95��5�      $��31��5�      ���	#FF3B50B5$��确定��5�      $
 Hand��5�      $��15(�5�      
������� 5�      ��%�   '��       �� 5�      " ��6    .�5�      ��  �@��CornerRadius$ 8��6=   �� 5�       � �� �#c1d0dc6<   5�      5�      5�      5�      5�      5�      5�      