   M S B A M L   `   `   `  ����F  BWpfNetMusic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:WpfNetMusicWpfNetMusic  5          WpfNetMusic.EditSongList   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5      %localclr-namespace:WpfNetMusic  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      -   5          � Title$  编辑歌单��5	      $	��300��5
      $	��210��5       � 
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
��Left=�5@      $��Center=�5A      $��25(�5B      ���	#FFFCFCFC$��编辑歌单��5D      �� 5E      -   5        $��0��5F      $��1��5G      $��20��5H      $��20��5I      $��0,0,20,0q�5J      $��Right=�5K       � Cursor$
 Hand��5L      $��/Image/CloseIcon-01.png��5N      5O      � 5Q   
   $��1��6   $��0��6    5R      �� 5S      $
��0.3* �6    �� 5T      $��0.17* �6    .+�5V      �� $��0��5W      $��0��5X      $	��260��5Y      $	��112��5Z       �� Orientation$ Vertical=�5[      .+�5\      �� $��0,6,0,0q�5]      $��Center=�5^      $��21(�5_      $��请输入歌单名称��5`      �� 5a      -   5         ��Name$ tbSongListName��5b      $	��230��5c      $��28��5d      $��0,5,0,0q�5e      $��5,3,0,0q�5f      $��Center=�5g      $��15(�5h      ��5i      �� *
SongName+.��6/    � 	Resources 5j      ��%�   '��       �� 5k      " ��6    .�5l      ��  �@��CornerRadius$ 11��6=   �� 5m       � �� �#c1d0dc6<   5n      5o      .��6)   5p      �� 5q      $	��260��5r      $��30��5s      $��0,9,0,0q�5t      $ 
Horizontal=�5u      .+�5v      �� $��15��5w      $��15��5x      $��15,3,0,0q�5y       d� 	IsChecked 5z      �� *	IsDefault+�� 5{      $��9,1,0,0q�5|      $��Center=�5}      $��21(�5~      $��是否默认歌单��5      5�      5�      � 5�      $��1��6   $��0��6     5�      �� 5�       5�      �� 5�      �� 5�      .+�5�      �� -   5         ��Name$ 	btnCancel��5�      $��1��5�      $��0��5�      $��95��5�      $��31��5�      ���	#FF843D7D$��取消��5�      $
 Hand��5�      $��15(�5�      
������� 5�      ��%�   '��       �� 5�      " ��6    .�5�      ��  �@��CornerRadius$ 8��6=   �� 5�       � �� �#c1d0dc6<   5�      5�      5�      �� 5�      -   5        $ 
btnConfirm��5�      $��1��5�      $��1��5�      $��95��5�      $��31��5�      ���	#FF3B50B5$��确定��5�      $
 Hand��5�      $��15(�5�      
������� 5�      ��%�   '��       �� 5�      " ��6    .�5�      ��  �@��CornerRadius$ 8��6=   �� 5�       � �� �#c1d0dc6<   5�      5�      5�      5�      5�      5�      5�      