   M S B A M L   `   `   `  ����F  BWpfNetMusic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:WpfNetMusicWpfNetMusic  5           WpfNetMusic.ErrorMsgDialog   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5      %localclr-namespace:WpfNetMusic  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      -   5          � Title$  ErrorMsgDialog��5	      $	��800��5
      $	��450��5       � WindowStyle$
 None=�5      *  $Microsoft.Xaml.Behaviors.Interaction   	Behaviors 5      $   WpfNetMusic.WindowDragBehavior  5   
   .��5      �  � ColumnDefinitions 5   
   �� 5       � RowDefinitions 5   
   �� 5      $
��0.2* �6   �� 5      $��1* �6   .+�5   
   � $��0��5      $��0��5      ���	#FFF11886 5      �� 5        5"      �� 5#      $��1* �6#   �� 5$      $��1* �6#   .+�5&      �� $��5,0,0,0q�5'       �� Padding$ 10q�5(      $
��Left=�5)      $��Center=�5*      $��25(�5+      ���	#FFFCFCFC$��错误信息��5-      �� 5.      -   5         ��Name$ WindowClose��5/      $��0��50      $��1��51      $��20��52      $��20��53      $��0,0,20,0q�54      $��Right=�55       � Cursor$
 Hand��56      $��/Image/CloseIcon-01.png��58      59      �� 5;   
   -   5         ��Name$ txtErrorMessage��5<      $��1��5=      $��0��5>      $��5,5,5,5q�5?      	 �� AcceptsReturn$
	 True��5@      $��FZShuTi)�5A      $��25(�5B      $
��Bold"�5C      
 �� 
IsReadOnly$
 False��5D       �� TextWrapping$
 Wrap=�5F      " �� VerticalScrollBarVisibility$ Visible=�5G      ��5E      �� *
ErrorMsg+.��5G   3    � 	Resources 5H       �� 
TargetType �� Property �� Value��%�   '��       �� 5I      " ��6   .�5J      ��  �@��CornerRadius$ 5��65   �� 5K       � �� �#c1d0dc64   5L      5M      .��6!   ��5N      
� 5O      .��5P      �  � Color$ 	#FFE525B7��6#   � 5Q       � Offset$ 1Q�6#   $ 	#FFEB6710��6.   5R      .��5S   "   5T      5U      5V      