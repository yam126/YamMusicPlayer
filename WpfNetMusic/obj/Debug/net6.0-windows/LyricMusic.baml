   M S B A M L   `   `   `  ����F  BWpfNetMusic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:WpfNetMusicWpfNetMusic  5          WpfNetMusic.LyricMusic   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5      %localclr-namespace:WpfNetMusic  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      -   5          � Title$  
LyricMusic��5	      $
��auto��5
      $	��135��5       � AllowsTransparency$
 True��5      ���	#A3FFFFFF � 
ResizeMode$ NoResize=�5       � WindowStyle$
 None=�5      *  $Microsoft.Xaml.Behaviors.Interaction   	Behaviors 5      $   WpfNetMusic.WindowDragBehavior  5   
   .��5      � ���	#25000000 � RowDefinitions 5   
   �� 5      $
��0.5* �6   �� 5      $��1* �6    � ColumnDefinitions 5   
   �� 5      .+�5   
   �� $��0��5      $��0��5      $��70��5       $��30��5!       �� Orientation$ 
Horizontal=�5"      .+�5#      �� -   5        $��20��5$      $��20��5%      $��7,3,0,0q�5&      $��Right=�5'       � Cursor$
 Hand��5(      $��/Image/folder.png��5*      	 � ToolTip$	 打开歌词文件��5+      �� 5,      -   5        $��20��5-      $��20��5.      $��7,3,0,0q�5/      $��Right=�50      $
 Hand��51      $��/Image/CloseIcon-01.png��53      $	 关闭歌词显示��54      55      �� 57   
   $��1��58      $��0��59      $
��auto��5:      $��65��5;      .+�5<      �� $
��auto��5=      $��65��5>      
������ 
������ ��5A      �� *Lyrics+��5B      �� 5C      
 �� 
TargetType"
 ��6&   .R�5D      �  K� IsItemsHost$
 True��6$    � Orientation$ 
Horizontal=�67   5E      ��5G      �� 5H      .R�5I      (� .��5J      �� $��3��5K   !   $��7��5L   !   $��1,3,0,0q�5M   !   $��3(�5N   !   ��5O   !   �� *Color+��5P   !   �� *Char+5Q      5R      5T      5U      5W      5X      