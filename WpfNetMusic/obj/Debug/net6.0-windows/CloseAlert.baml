   M S B A M L   `   `   `  ����F  BWpfNetMusic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:WpfNetMusicWpfNetMusic  5          WpfNetMusic.CloseAlert   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5      %localclr-namespace:WpfNetMusic  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      -   5          � Title$  
CloseAlert��5	      $	��475��5
      $	��275��5       � AllowsTransparency$
 True��5      
������� J� OpacityMask
 ����� � 
ResizeMode$ NoResize=�5       � WindowStyle$
 None=�5       � 	Resources 5         WpfNetMusic.Global   setting��%   &	          5   
   6   5      ��5      �� 5   
    �� 
TargetType" �6   .�5      ��  �� Property � �� �� Value 5      �� 5      	 �� 
TargetType"	 �6&   .R�5      �� ���	#FF229DBE$��2q�5      
 �� CornerRadius$
 5��5       J� Opacity$ 1Q�5       ��5!      �� 5"   "    �� 
StartPoint$ 0.5,0&�66    �� EndPoint$ 0.5,1&�6I   .��5#   &   �  � Color$ #FFFFFF��63   � 5$   &    � Offset$ 1Q�63   $ #FFFFFF��6>   5%   #   ._�5'      �� 5(      5)      5+      5,      *  $Microsoft.Xaml.Behaviors.Interaction   	Behaviors 5.      $   WpfNetMusic.WindowDragBehavior  5/   
   .��51      �  � RowDefinitions 52   
   �� 53      $
��0.2* �6   �� 54      $��1* �6    � ColumnDefinitions 56   
   �� 57      .+�5:   
   � $��0��5;      $��0��5<      ���#4791EB 5>      �� 5?       5A      �� 5B      $��1* �6#   �� 5C      $��1* �6#   .+�5E      ��  �� Padding$ 10q�5F      $
��Left=�5G      $��Center=�5H      $��25(�5I      ���	#FFFCFCFC$��关闭选项��5K      �� 5L      -   5        $��0��5M      $��1��5N      $��20��5O      $��20��5P      $��0,0,20,0q�5Q      $��Right=�5R       � Cursor$
 Hand��5S      $��/Image/CloseIcon-01.png��5U      5V      �� 5X   
   $��1��5Y      $��0��5Z      $	��390��5[      $	��190��5\      .+�5]      	� -   5        $��25��5^      $��0,15,0,0q�5_      $��Center=�5`       d� 	IsChecked 5b      �� *IsMinTaskBar+.��5c      �� $��21(�6   $��最小化到任务栏��6*   5d      	� 5e      -   5        $��25��5f      $��0,15,0,0q�5g      $��Center=�5h       5j      �� *
QuitSystem+.��5k      �� $��21(�6   $��退出程序��6*   5l      �� 5m      $��0,15,0,0q�6    6)   �� *IsSave+.��5n      �� $��21(�6   $��记住当前设置��6*   5o      � 5p      $��0,9,0,0q�6    5q      �� 5r      $��55 �6$    5t      �� 5u      �� 5v      .+�5x      �� -   5        $��1��5y      $��0��5z      $��95��5{      $��37��5|      ���	#FF843D7D$��取消��5      $
 Hand��5�      $��21(�5�      
������� 5�      ��%�   '��       �� 5�      " ��6    .�5�      ��  �@��CornerRadius$ 8��6=   �� 5�       � �� �#c1d0dc6<   5�      5�      5�      �� 5�      -   5        $��1��5�      $��1��5�      $��95��5�      $��37��5�      ���	#FF3B50B5$��确定��5�      $
 Hand��5�      $��21(�5�      
������� 5�      ��%�   '��       �� 5�      " ��6    .�5�      ��  �@��CornerRadius$ 8��6=   �� 5�       � �� �#c1d0dc6<   5�      5�      5�      5�      5�      5�      5�      