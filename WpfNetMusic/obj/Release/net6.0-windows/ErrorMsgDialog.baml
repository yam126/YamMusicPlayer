   M S B A M L   `   `   `  ���� F  BWpfNetMusic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:WpfNetMusicWpfNetMusic       WpfNetMusic.ErrorMsgDialog   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  %localclr-namespace:WpfNetMusic  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  -     � Title$  ErrorMsgDialog��$	��800��$	��450�� � WindowStyle$
 None=�*  $Microsoft.Xaml.Behaviors.Interaction   	Behaviors $   WpfNetMusic.WindowDragBehavior  .���  � ColumnDefinitions ��  � RowDefinitions �� $
��0.2* ��� $��1* �.+�� $��0��$��0�����	#FFF11886 ��  �� $��1* ��� $��1* �.+��� $��5,0,0,0q� �� Padding$ 10q�$
��Left=�$��Center=�$��25(����	#FFFCFCFC$��错误信息���� -    ��Name$ WindowClose��$��0��$��1��$��20��$��20��$��0,0,20,0q�$��Right=� � Cursor$
 Hand��$��/Image/CloseIcon-01.png���� -    ��Name$ txtErrorMessage��$��1��$��0��$��5,5,5,5q�	 �� AcceptsReturn$
	 True��$��FZShuTi)�$��25(�$
��Bold"�
 �� 
IsReadOnly$
 False�� �� TextWrapping$
 Wrap=�" �� VerticalScrollBarVisibility$ Visible=����� *
ErrorMsg+.�� � 	Resources  �� 
TargetType �� Property �� Value��%U   '��       �� " ��.���  �@��CornerRadius$ 5����  � �� �#c1d0dc.����
� .���  � Color$ 	#FFE525B7���  � Offset$ 1Q�$ 	#FFEB6710��.��