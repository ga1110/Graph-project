   M S B A M L   `   `   `  ����   GraphVisualization)    #GraphVisualization.FindVertexWindow   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � WindowStyle$  
ToolWindow=�$	��400��$	��600��.��� $��10q� � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $��* ��� $��* �.+��� -    ��Name$ NonAdjacentVertex��$8��2Вершины не смежные с данной��$
��Auto��$��40��
����ح�$
��Bold"�$��0��$��10,10,10,10q� K� ZIndex$ 2���� -   $" VerticesWithGreaterOutDegree��$L��FВершины с большей полуступенью исхода��$
��Auto��$��40��
����ح�$
��Bold"�$��1��$��10,10,10,10q�$ 2���� -   $ FindUnreachableVertices��$-��'Недостижимые вершины��$
��Auto��$��40��
����ح�$
��Bold"�$��2��$��10,10,10,10q�$ 2���� -   $ FindVerticesDistanceLessN��$B��<Вершины с дистанцией меньше чем N��$
��Auto��$��40��
����ح�$
��Bold"�$��3��$��10,10,10,10q�$ 2���� -   $ FindShortestPath��$7��1Кротчайшие пути от вершины��$
��Auto��$��40��
����ح�$
��Bold"�$��4��$��10,10,10,10q�$ 2���� $��5��$��2�� �� Orientation$ 
Horizontal=�$��Right=�$��Bottom=�$��5q�.+��� -   $��
Назад��$��75��$��5q� �� 	IsDefault$
 True��