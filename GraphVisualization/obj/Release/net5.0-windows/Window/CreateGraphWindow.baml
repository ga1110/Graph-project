   M S B A M L   `   `   `  ����   GraphVisualization*    $GraphVisualization.CreateGraphWindow   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � WindowStyle$  
ToolWindow=�$	��202��$	��300��.��� $��10q� � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $
��Auto ��� $��* �.+��� $��0��$��0��$��Имя графа:��$��Center=��� -    ��Name$ GraphNameTextBox��$��0��$��1��$��5q��� $��1��$��0��$.��(Граф ориентированный?��$��Center=��� -    ��Name$ IsDirectedCheckBox��$��1��$��1��$��5q��� $��2��$��0��$/��)Заменить текущий граф?��$��Center=��� -   $ 
AddToVoult��$��2��$��1��$��5q��� $��3��$��2�� �� Orientation$ 
Horizontal=�$��Right=�$��Bottom=�$��5q�.+��� -   $��OK��$��75��$��5q� �� 	IsDefault$
 True���� $��Отмена��$��75��$��5q� �� IsCancel$
 True��