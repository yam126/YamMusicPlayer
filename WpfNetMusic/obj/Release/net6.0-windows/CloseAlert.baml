   M S B A M L   `   `   `  ���� F  BWpfNetMusic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:WpfNetMusicWpfNetMusic      WpfNetMusic.CloseAlert   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  %localclr-namespace:WpfNetMusic  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  -     � Title$  
CloseAlert��$	��475��$	��275�� � AllowsTransparency$
 True��
������� J� OpacityMask
 ����� � 
ResizeMode$ NoResize=� � WindowStyle$
 None=� � 	Resources    WpfNetMusic.Global   setting��%   &	          ����  �� 
TargetType" �.���  �� Property � �� �� Value �� 	 �� 
TargetType"	 �.R��� ���	#FF229DBE$��2q�
 �� CornerRadius$
 5�� J� Opacity$ 1Q�����  �� 
StartPoint$ 0.5,0&� �� EndPoint$ 0.5,1&�.���  � Color$ #FFFFFF���  � Offset$ 1Q�$ #FFFFFF��._��� *  $Microsoft.Xaml.Behaviors.Interaction   	Behaviors $   WpfNetMusic.WindowDragBehavior  .���  � RowDefinitions �� $
��0.2* ��� $��1* � � ColumnDefinitions �� .+�� $��0��$��0�����#4791EB ��  �� $��1* ��� $��1* �.+���  �� Padding$ 10q�$
��Left=�$��Center=�$��25(����	#FFFCFCFC$��关闭选项���� -   $��0��$��1��$��20��$��20��$��0,0,20,0q�$��Right=� � Cursor$
 Hand��$��/Image/CloseIcon-01.png���� $��1��$��0��$	��390��$	��190��.+�	� -   $��25��$��0,15,0,0q�$��Center=� d� 	IsChecked �� *IsMinTaskBar+.���� $��21(�$��最小化到任务栏��	� -   $��25��$��0,15,0,0q�$��Center=� �� *
QuitSystem+.���� $��21(�$��退出程序���� $��0,15,0,0q� �� *IsSave+.���� $��21(�$��记住当前设置��� $��0,9,0,0q� �� $��55 � �� �� .+��� -   $��1��$��0��$��95��$��37�����	#FF843D7D$��取消��$
 Hand��$��21(�
������� ��%U   '��       �� " ��.���  �@��CornerRadius$ 8����  � �� �#c1d0dc�� -   $��1��$��1��$��95��$��37�����	#FF3B50B5$��确定��$
 Hand��$��21(�
������� ��%U   '��       �� " ��.���  �@��CornerRadius$ 8����  � �� �#c1d0dc