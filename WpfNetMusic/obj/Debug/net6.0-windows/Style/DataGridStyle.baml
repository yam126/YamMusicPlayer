   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   DataGridRow.Height  DataGrid.BorderThickness  u� Bottom u� Left u� Right
 u� Top   ColumnHeader.BorderThickness  DataGridRow.BorderThickness  DataGrid.BorderBrush �� Color  ColumnHeader.Background  ColumnHeader.BorderBrush  DataGridRow.BorderBrush ! DataGridRow.Normal.Background !	 DataGridRow.Normal.Foreground $
  DataGridRow.MouseOver.Background $  DataGridRow.MouseOver.Foreground # DataGridRow.Selected.Background # DataGridRow.Selected.Foreground&    System.Windows.Controls.DataGrid �� 
TargetType �� Property �� Value=  7System.Windows.Controls.Primitives.DataGridColumnHeader �� 
TargetType	 ��Name
 J� SnapsToDevicePixels � UseLayoutRounding)  #System.Windows.Controls.DataGridRow?  9System.Windows.Controls.Primitives.SelectiveScrollingGrid � ColumnDefinitions � RowDefinitions?  9System.Windows.Controls.Primitives.DataGridCellsPresenter   
ItemsPanelA  ;System.Windows.Controls.Primitives.DataGridDetailsPresenter   DetailsVisibility$   SelectiveScrollingOrientation;  5System.Windows.Controls.SelectiveScrollingOrientation   Vertical �� ConverterParameter#    RowDetailsScrollingConverter �� 	Converter �� RelativeSource �� AncestorType:  4System.Windows.Controls.Primitives.DataGridRowHeader7  1System.Windows.Controls.DataGridHeadersVisibility
   Row!    HeadersVisibilityConverter �� Triggers P� Property P� Value*	  $System.Windows.Controls.DataGridCell �� Triggers�� 5      H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      :x,http://schemas.microsoft.com/winfx/2006/xaml     5      .sys&clr-namespace:System;assembly=mscorlib  5      %  &	        &	      &	 l     &	 �     &	     &	 1    &	 R    &	 s    &	 �    &		 �    &	
 �    &	 �    &	     &	 9    '   X    7  7  7  7   '  g    7  7  '  �    7  7  7  7  7   7  7 
 7  7  '	  �    X�5      33Q�60   u� 5      $  1Q�5
   	   $ 1Q�5   	   $ 1Q�5   	   $ 1Q�5   	   u� 5      $  0Q�5   	   $ 0Q�5   	   $ 1Q�5   	   $ 0Q�5   	   u� 5      $  0Q�5   	   $ 0Q�5   	   $ 0Q�5   	   $ 1Q�5   	   �� 5      $ #E9ECF1��63   �� 5       $ #F6F7FB��66   �� 5"      $ #E9ECF1��67   �� 5$      $ #E9ECF1��66   �� 5&      $ #FFFFFF��6<   �� 5(      $ #000000��6<   �� 5*      $ #F6F7FB��6?   �� 5,      $ #000000��6?   �� 5.      $ #99FFFF��6>   �� 50      $ White��6>   �� 52      "   6   .�54   
   ��  �@  RowHeaderWidth$ 0Q�6+   �� 55   
    � ��8   6,   �� 56   
    �@  HeadersVisibility$ Column=�6.   �� 57   
    � ��8  6'   �� 58   
    � ��8  6(   �� 59   
     �@  HorizontalGridLinesBrush �	#00E9ECF165   �� 5:   
    �@  VerticalGridLinesBrush �	#00E9ECF163   �� 5;   
    �@�UseLayoutRounding$
 True��6.   �� 5<   
    �@J�SnapsToDevicePixels$
 True��60   �� 5=   
    �@  AutoGenerateColumns$ False��60   �� 5>   
    �@  CanUserAddRows$ False��6+   �� 5?   
    �@  CanUserReorderColumns$
 True��62   �� 5@   
    �@  CanUserResizeColumns$
 True��61   �� 5A   
    �@  CanUserResizeRows$ False��6.   �� 5B   
    �@  CanUserSortColumns$
 True��6/   �� 5C   
    �@  GridLinesVisibility$
 None=�60   �� 5D   
    �@  
IsReadOnly$
 True��6'   �� 5E   
    �@  	RowHeight8  6&   �� 5F   
    �@  SelectionMode$ Single=�6*   5G      �� 5I      "  6   .�5J   
   ��  �@J�SnapsToDevicePixels$
 True��60   �� 5K   
    � �� �#0000006'   �� 5L   
    � ��$ 12(�6%   �� 5M   
    �@�Cursor$
 Hand��6#   �� 5N   
    � ��$ 28��6#   �� 5O   
    � �� 5P      �� 5Q      "  6"   .R�5R      �� $	 BackgroundBorder��5S      $
��Auto��5T      $
��-1,0q�5U      8��  8�� $

 True��5X      $
 True��5Y      ._�5Z      �� $	��5,0q�5[      $��Center=�5\      $��Center=�5]      5^      5_      5a      5b      �� 5d      "  6   .�5e   
   ��  � ��8   6'   �� 5f   
    � ��8  6'   �� 5g   
    �@J�SnapsToDevicePixels$
 true��60   �� 5h   
    �@�UseLayoutRounding$
 True��6.   �� 5i   
    �@=�ErrorTemplate 65   Z� �� 5j   
    � ��8  6,   �� 5k   
    � ��8  6(   �� 5l   
    �@ ValidationErrorTemplate 5m      �� 5n      .R�5o      �� $��0,0,0,0q�5p      $��Center=�5q      
���  ��$��!��5s      5t      5v      �� 5w   
    � �� 5x      �� 5y      "  6"   .R�5z      �� $	 
DGR_Border��5{      $��0,0,0,-1q�5|      #��z��5}      #��z��5~      #��z��5      $

 True��5�      $
 True��5�      ._�5�         5�      �� 5�   "   $
��Auto �63   �� 5�   "   $��* �63    5�      �� 5�   "   $��* �60   8�� 6;   �� 5�   "   $
��Auto �60   .+�5�        $��1��5�   !   #��z 5�   !   #
 z
 5�   !     5�      $��1��5�   !   $��1��5�   !   #y�z 5�   !    5�   !   �� *AreRowDetailsFrozen+# Z # Z  �� "   5�   !     5�      $��2��5�   !   $ Vertical=�5�   !   y�5�   !   �� *HeadersVisibility+# Z # Z  �� "   5�      5�       5�      P� 5�       �@J�IsMouseOver$
 True��69   . �5�      �� 8   � ��6;   �� 5�      8   � ��6;   5�      P� 5�       �@ 
IsSelected$
 True��68   . �5�      �� 8   � ��6;   �� 5�      8   � ��6;   5�      5�      5�      5�      �� 5�      " 	 6   .�5�   
   ��  � �� 5�      �� 5�      " 	 6"   .R�5�      �� $	 Bg��5�      
������ $
 True��5�      ._�5�      �� $��Center=�6+   $��Center=�6H   5�      5�      5�       5�   
   P� 5�       �@	 
IsSelected$
 True��6,   . �5�      ��  � ��
 ���� 6/   �� 5�       � �� �#0000006/   5�      5�      5�      