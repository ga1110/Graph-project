   M S B A M L   `   `   `  ����  GraphVisualization,    &GraphVisualization.AddRemoveEdgeWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � WindowStyle$  
ToolWindow=�5   	   $	��250��5   	   $	��300��6   .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5	      $
��Auto �6   �� 5
      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6   $(��"Начальная вершина:��6-   $��Center=�6J   �� 5   
   -   5         ��Name$ StartVertexTextBox��5   /   $��0��6   $��1��6   $��5q�6K   �� 5   
   $��1��6   $��0��6   $&�� Конечная вершина:��6-   $��Center=�6I   �� 5   
   -   5        $ EndVertexTextBox��5   /   $��1��6   $��1��6   $��5q�6I   �� 5   
   -   5         ��Name$ WeightLabel��5   g   $��2��6   $��0��6   $*��$Вес (необязательно):��6-   $��Center=�6L   ${�False��6|   $y�Hidden=�6�   �� 5   
   -   5        $ WeightTextBox��5   /   $��2��6   $��1��6   $��5q�6F   ${�False��6Q   $y�Hidden=�6c   �� 5   
   -   5        $ CapacityLabel��5   k   $��3��6   $��0��6   $2��,Ёмкость (необязательно):��6-   $��Center=�6P   ${�False��6�   $y�Hidden=�6�   �� 5    
   -   5        $ CapacityTextBox��5    /   $��3��6   $��1��6   $��5q�6H   ${�False��6S   $y�Hidden=�6e   �� 5"   
   $��4��6   $��2��6"    �� Orientation$ 
Horizontal=�66   $��Right=�6O   $��5q�6k   .+�5#      �� -   5        $��OK��5#      $��75��6"   $��5q�6-    �� 	IsDefault$
 True��68   6I   �� 5$      $��Отмена��6   $��75��6&   $��5q�61    �� IsCancel$
 True��6<   5%      5&      5'      