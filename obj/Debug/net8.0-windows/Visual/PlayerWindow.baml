   M S B A M L   `   `   `  ����
  Basket     Basket.Visual.PlayerWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	     � Title$  PlayerWindow��5   	    � WindowState$ 	Maximized=�6    � 	Resources 5         MenuButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius �� Triggers	 P� Property
 P� Value  RoundedComboBoxStyle ��Name d�Name �� 	ClickMode d� 	IsChecked �� Path �� RelativeSource �� Mode F�Name ��Name J� IsHitTestVisible �� SelectionBoxItem �� SelectionBoxItemTemplate ��Name �� 
IsReadOnly �� 
IsReadOnly �Name �� IsDropDownOpen � AllowsTransparency �Name J� SnapsToDevicePixels � ActualWidth  �� MaxDropDownHeight! K� IsItemsHost-  'System.Windows.Input.KeyboardNavigation"   DirectionalNavigation# �� Triggers$ �� 
TargetName  FilterLabelStyle��%�  &	        &	 �    &	     �� 5
   
   " ��6(   .�5      ��  � ��
 ���� 6+   �� 5       � ��
 �   �6+   �� 5       � ��$ 1q�60   �� 5       � �� �#AA423A6,   �� 5       � ��$	 200��6&   �� 5       � ��$ 40��6'   �� 5       � ��$ 16(�6)   �� 5       � ��$
 Left=�6;   �� 5       � ��$ Center=�69   �� 5       � ��$
 10,5q�6'   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� #��z��6!   #��z��5   !   #��z��5   !   $ 10��5   !   #��z��5   !   #��z��5   !   ._�5      �� #��z��6/   #��z��5    /   $��20,0,0,0q�5!   /   5"      5#      5%       5(      P� 5)      	 �@J�IsMouseOver$

 True��61   . �5*      ��  � �� �#DB783E63   �� 5+       � �� �#AA423A64   5,      P� 5-      	 � {�$
 False��6/   . �5.      ��  � �� �#C0C9FE63   �� 5/       � �� �#A0A0A064   50      52      �� 55   
   " ��6-   .�56      ��  � ��
 �   �6+   �� 57       � ��
 �����6+   �� 58       � �� �#CCCCCC6,   �� 59       � ��$ 1q�60   �� 5:       � ��$ 8q�6(   �� 5;       � ��$	 200��6&   �� 5<       � ��$ 40��6'   �� 5=       � �� 5>      �� 5?      " ��6&   .R�5@      � .+�5B      �� $ Border��6%   #��z��5C   %   #��z��5D   %   #��z��5E   %   $ 10��5F   %   ._�5G   "   � .+�5I   &   d� $ ToggleButton��63   $|�false��5J   3   $ Press=�5L   3   
������ $��0q�5N   3   $��Right=�5O   3   $��Center=�5P   3   $��20��5Q   3    5K   3   �� $ IsDropDownOpen� �� *TemplatedParent+$ TwoWay=�.��5S   *   F� $ Arrow��6/   
���   �"����        � 	=  	= � z     	$��Center=�5V   /   $��Center=�5W   /   5X   '   �� 5[   &   $ ContentSite��67   $ False��5\   7   #��z 5]   7   #��z 5^   7   #��z��5_   7   $��3,3,30,3q�5`   7   $��Center=�5a   7   $
��Left=�5b   7   �� 5e   &   $ PART_EditableTextBox��6.   $y�Hidden=�5f   .   $
��Left=�5g   .   $��Center=�5h   .   $��3,3,30,3q�5i   .   $
|�True��5j   .   
������ # z 5l   .   5m   #   5n      � 5q      $ Popup��6$   $��Bottom=�5r   $   #��z 5s   $   $
 True��5t   $   $|�False��5u   $   $��Slide=�5v   $   .��5w   "   � $ DropDown��6'   $
 True��5x   '   #��z 5y   '   #��z  5z   '   .+�5{   &   �� $ DropDownBorder��6-   
�������$��1q�5}   -   $ 10��5~   -   �� 5   &   $��4,6,4,6q�63   $
 True��6D   .��5�   *   �� $
! True��65   $" 	Contained=�6H   5�   '   5�   #   5�      5�      # 5�      P� 5�      	 � {�$
 False��6;   . �5�   "   �� $$ Border��6)    � �� �#EEEEEE6S   �� 5�   "    � �� �#8888886?   5�      P� 5�      	 �@��
IsEditable$

 True��6<   . �5�   "   ��  � ��$ False��6>   �� 5�   "   $$ PART_EditableTextBox��6)    � y�$ Visible=�6a   �� 5�   "   $$ ContentSite��6)    � y�$ Hidden=�6X   5�      5�      5�      5�      �� 5�   
   " ��6)   .�5�      ��  � ��$ 14(�6)   �� 5�       � ��$ 0,10,0,5q�6'   �� 5�       � �� �#0000006+   5�      5�      .��5�      � % � ColumnDefinitions% 5�   
   �� 5�      $	��250 �6   �� 5�      $��* �6   .+�5�   
   �� $��0��6   $ 15��6!   $��10q�63   ��5�      �� 5�      & �� 
StartPoint$	& 0,0&�6*   ' �� EndPoint$	' 1,1&�6;   .��5�      � ( � Color$( #F2E3D8��6'   ) � Offset$) 0Q�67   � 5�      $( #DB783E��6'   $	) 0.8Q�67   � 5�      $( #AA423A��6'   $) 1Q�67   5�      ._�5�      � * � RowDefinitions* 5�      �� 5�      $
��Auto �6(   �� 5�      $��* �6(   �� 5�      $
��Auto �6(   .+�5�      �� $��0��6!   $��	0,20,0,40q�6.   $��Center=�6A   .+�5�      �� $/��)C:\\Users\\Scarlet\\Downloads\\Basket.png��6    $��80��6S   $��80��6^   �� 5�      $��Bienvenido A��6$   $��20(�68   $��Center=�6F   $��0,10,0,0q�6c   �� 5�      $��	HoopStats��6$   $��14(�65   ���#666666$��Center=�6X   5�      �� 5�      $��1��6!   $��10q�6.   .+�5�      �� $��Filtrar por Nombre:��6$   $
��Bold"�6?   $��5,5,0,5q�6Q   #��[ 6b   � 5�      .+�5�      �� -   5        + ��Name$+ 
NameFilter��5�   '   $
��Left=�6;   #��[ 6V   5�      �� 5�      $��Filtrar por Equipo:��6$   $
��Bold"�6?   $��5,5,0,5q�6Q   #��[ 6b   � 5�      .+�5�      �� -   5        $+ 
TeamFilter��5�   '   $
��Left=�6;   #��[ 6V   5�      �� 5�      $��Filtrar por Ciudad:��6$   $
��Bold"�6?   $��5,5,0,5q�6Q   #��[ 6b   � 5�      .+�5�      �� -   5        $+ 
CityFilter��5�   '   $
��Left=�6;   #��[ 6V   5�      �� 5�      $��Filtrar por Número:��6$   $
��Bold"�6?   $��5,5,0,5q�6Q   #��[ 6b   � 5�      .+�5�      �� -   5        $+ NumberFilter��5�   '   $
��Left=�6=   #��[ 6X   5�      �� 5�      -   5        , ��Name$, ApplyFilterButton��5�   !   $��Aplicar filtros��6<   #��[  6V   $��5,20,5,5q�6   6�   �� 5�      -   5        $, ClearFiltersButton��5�   !   $��Limpiar filtros��6=   #��[  6W   $��5,20,5,5q�6�   6�   �� 5�      -   5        $, CreatePlayerButton��5�   !   $��Crear un Jugador��6=   #��[  6X   $��5,20,5,5q�6�   6�   5�      �� 5�      -   5        $, CloseButton��5�   &   $��2��6   $��Cerrar Sesión��6;   #��[  6S   6|   5�      5�      � 5�   
   -	   5        $ MainContent��5�      $��1��6$   $��10q�64   * 5�      �� 5�      $
��Auto �6    �� 5�      $��* �6    .+�5�      �� $��	Jugadores��6   $��24(�5�      $��SemiBold"�5�      $��10,20,10,20q�5�      �� 5�      $��1��6   $
��Auto=�5�      $��Disabled=�5�      .��5�      � -
   5        - �Name$- CardsContainer��5�      . � Orientation$. 
Horizontal=�5�      $��5q�5�      5      5     5     5     5     