   M S B A M L   `   `   `  ���� F  BWpfNetMusic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:WpfNetMusicWpfNetMusic      WpfNetMusic.LyricMusic   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  %localclr-namespace:WpfNetMusic  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  -     � Title$  
LyricMusic��$
��auto��$	��135�� � AllowsTransparency$
 True�����	#A3FFFFFF � 
ResizeMode$ NoResize=� � WindowStyle$
 None=�*  $Microsoft.Xaml.Behaviors.Interaction   	Behaviors $   WpfNetMusic.WindowDragBehavior  .��� ���	#25000000 � RowDefinitions �� $
��0.5* ��� $��1* � � ColumnDefinitions �� .+��� $��0��$��0��$��70��$��30�� �� Orientation$ 
Horizontal=�.+��� -   $��20��$��20��$��7,3,0,0q�$��Right=� � Cursor$
 Hand��$��/Image/folder.png��	 � ToolTip$	 打开歌词文件���� -   $��20��$��20��$��7,3,0,0q�$��Right=�$
 Hand��$��/Image/CloseIcon-01.png��$	 关闭歌词显示���� $��1��$��0��$
��auto��$��65��.+��� $
��auto��$��65��
������ 
������ ���� *Lyrics+���� 
 �� 
TargetType"
 ��.R��  K� IsItemsHost$
 True�� � Orientation$ 
Horizontal=����� .R�(� .���� $��3��$��7��$��1,3,0,0q�$��3(����� *Color+���� *Char+