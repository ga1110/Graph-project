   M S B A M L   `   `   `  ����  GraphVisualization*    $GraphVisualization.CreateGraphWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � WindowStyle$  
ToolWindow=�5   	   $	��202��5   	   $	��300��6   .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5	      $
��Auto �6   �� 5
      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6   $��Имя графа:��6-   $��Center=�6B   �� 5   
   -   5         ��Name$ GraphNameTextBox��5   /   $��0��6   $��1��6   $��5q�6I   �� 5   
   $��1��6   $��0��6   $.��(Граф ориентированный?��6-   $��Center=�6M   �� 5   
   -   5         ��Name$ IsDirectedCheckBox��5   0   $��1��6   $��1��6    $��5q�6L   �� 5   
   $��2��6   $��0��6   $/��)Заменить текущий граф?��6-   $��Center=�6N   �� 5   
   -   5        $ 
AddToVoult��5   0   $��2��6   $��1��6    $��5q�6D   �� 5    
   $��3��6   $��2��6"    �� Orientation$ 
Horizontal=�66   $��Right=�6O   $��Bottom=�6k   $��5q�6�   .+�5!      �� -   5        $��OK��5!      $��75��6"   $��5q�6-    �� 	IsDefault$
 True��68   6I   �� 5"      $��Отмена��6   $��75��6&   $��5q�61    �� IsCancel$
 True��6<   5#      5$      5%      