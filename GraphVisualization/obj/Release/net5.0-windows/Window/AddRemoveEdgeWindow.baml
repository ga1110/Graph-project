   M S B A M L   `   `   `  ����   GraphVisualization,    &GraphVisualization.AddRemoveEdgeWindow   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � WindowStyle$  
ToolWindow=�$	��250��$	��300��.��� $��10q� � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $
��Auto ��� $��* �.+��� $��0��$��0��$(��"Начальная вершина:��$��Center=��� -    ��Name$ StartVertexTextBox��$��0��$��1��$��5q��� $��1��$��0��$&�� Конечная вершина:��$��Center=��� -   $ EndVertexTextBox��$��1��$��1��$��5q��� -    ��Name$ WeightLabel��$��2��$��0��$*��$Вес (необязательно):��$��Center=�${�False��$y�Hidden=��� -   $ WeightTextBox��$��2��$��1��$��5q�${�False��$y�Hidden=��� -   $ CapacityLabel��$��3��$��0��$2��,Ёмкость (необязательно):��$��Center=�${�False��$y�Hidden=��� -   $ CapacityTextBox��$��3��$��1��$��5q�${�False��$y�Hidden=��� $��4��$��2�� �� Orientation$ 
Horizontal=�$��Right=�$��5q�.+��� -   $��OK��$��75��$��5q� �� 	IsDefault$
 True���� $��Отмена��$��75��$��5q� �� IsCancel$
 True��