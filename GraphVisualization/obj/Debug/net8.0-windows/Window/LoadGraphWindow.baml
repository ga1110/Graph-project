   M S B A M L   `   `   `  ����  GraphVisualization(    "GraphVisualization.LoadGraphWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$!  Загрузка графа��5   	   $	��202��6    $	��400��6-   .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5	      $
��Auto �6   �� 5
      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6   $��Путь файла:��6-   $��Center=�6C   �� 5   
   -   5         ��Name$ FilePathTextBox��5   /   $��0��6   $��1��6   $��5q�6H    �� 
IsReadOnly$
 True��6S   �� 5   
   $��1��6   $��0��6   $��Имя графа:��6-   $��Center=�6B   �� 5   
   -   5        $ GraphNameTextBox��5   /   $��1��6   $��1��6   $��5q�6I   �� 5   
   $��2��6   $��0��6   $/��)Заменить текущий граф?��6-   $��Center=�6N   �� 5   
   -   5         ��Name$ 
AddToVoult��5   0   $��2��6   $��1��6    $��5q�6D   �� 5   
   $��3��6   $��2��6"    �� Orientation$ 
Horizontal=�66   $��Right=�6O   $��Bottom=�6k   $��5q�6�   .+�5      �� -   5        $!��Загрузить файл��5      $
��Auto��6.   $��5q�6;    �� 	IsDefault$
 True��6F   6W   �� 5       -   5        $��OK��5       $��75��6"   $��5q�6-   $
 True��68   6I   �� 5!      $��Отмена��6   $��75��6&   $��5q�61    �� IsCancel$
 True��6<   5"      5#      5$      