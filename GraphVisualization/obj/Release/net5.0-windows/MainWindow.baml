   M S B A M L   `   `   `  ���� f  bMicrosoft.Msagl.WpfGraphControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6baa2c1345e83b6ezVclr-namespace:Microsoft.Msagl.WpfGraphControl;assembly=Microsoft.Msagl.WpfGraphControlMicrosoft.Msagl.WpfGraphControl   GraphVisualization#   GraphVisualization.MainWindow   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     hmsaglVclr-namespace:Microsoft.Msagl.WpfGraphControl;assembly=Microsoft.Msagl.WpfGraphControl     Aav2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     #wpfgraphcontrolhttp://mlsagl   -   $	��900��$
��1600��$	��900��$
��1600��  � WindowStyle$  
ToolWindow=�
������  � AllowsTransparency$ False�� � 	Resources  �� MergedDictionaries �� Source��  �� $ Style/ExpanderStyle.xaml?�%    .���� ���#0e1621._��  � ColumnDefinitions �� $	��300 ��� �� $	��250 � � RowDefinitions �� $	��100 ��� �� $	��100 �.+��� $��0��$��0��$��3�� �� CornerRadius$ 0��._��� $��0��$��0�� �� Orientation$ 
Horizontal=�.+��� $��Resources\logo.png��$��50��$��50��$
��Left=�$��Center=�$��10,0,0,0q��� $��Graph Visualization��$��25(�
�������$
��Bold"�$	��230��$��34���� $��Hidden=�$��1��$��0��.���� .+�9� -   $'��!Управление графом��$��
10,0,10,10q�	 9� 
IsExpanded$	 False��.���� .+��� -   
 ��Name$
 CreateGraphButton��$��Создать граф��$��10q�   CustomButtonStyle#��[  �� -   $
 LoadGraphButton��$!��Загрузить граф��$��10q�#��[  �� -   $
 SaveGraphToFileButton��$"��Записать в файл��$��10q�#��[  �� -   $
 CopyGraphButton��$%��Скопировать граф��$��10q�#��[  �� -   $
 OpenGraphVoult��$��Список графов��$��10q�#��[  9� -   $(��"Операции с рёбрами��$	 False��$��10q�.���� .+��� -	   $
 AddEdgeButton��$!��Добавить ребро��$��10q�#��[  �� -
   $
 RemoveEdgeButton��$��Удалить ребро��$��10q�#��[  �� -   $
 RemoveLeafEdgeButton��$@��:Удалить ребра ведущие к листьям��$��10q�#��[  9� -   $,��&Операции с вершинами��$	 False��$��10q�.���� .+��� -   $
 	AddVertex��$%��Добавить вершину��$��10q�#��[  �� -   $
 RemoveVertex��$#��Удалить вершину��$��10q�#��[  9� -   $��Поиск вершин��$	 False��$��10q�.���� .+��� -   $
 NonAdjacentVertex��$8��2Вершины не смежные с данной��$��10q�#��[  �� -   $"
 VerticesWithGreaterOutDegree��$L��FВершины с большей полуступенью исхода��$��10q�#��[  �� -   $
 FindUnreachableVertices��$-��'Недостижимые вершины��$��10q�#��[  �� -   $
 FindVerticesDistanceLessN��$D��>Вершины с расстоянием меньше чем N��$��10q�#��[  �� -   $
 FindShortestPath��$7��1Кротчайшие пути от вершины��$��10q�#��[  �� -   $
 FindNPeriphery��$%��Найти N-переферию��$��10q�#��[  9� -   $��Разное��$	 False��$��10q�.���� .+��� -   $
 FindMST��$��Найти MST��$��10q�#��[  �� -   $
 FindMaxFlow��$4��.Найти максимальный поток��$��10q�#��[  �� $��Hidden=�$��1��$��3��.���� .+�9� $(��"Информация о графе��$��
10,0,10,10q�$	 False��.���� -    ��Name$ InfoTextBox��$��10q� �� 
IsReadOnly$
 True�� �� TextWrapping$
 Wrap=�" �� VerticalScrollBarVisibility$ Hidden=�$ �� HorizontalScrollBarVisibility$ Disabled=�
�������
���   �$��2q�$��15(�9� $0��*Список смежности графа��$��10q�$	 False��.���� -   $ 
AdjTextBox��$��10q�$
 True��$
 Wrap=�$ Hidden=�$ Disabled=�
�������
���   �$��2q�$��15(�9� $%��История действий��$��10q�$	 False��.���� -   $ HistoryTextBox��$��10q�$
 True��$
 Wrap=�$ Hidden=�$ Disabled=�
�������
���   �$��2q�$��13(��� 
�������$��1��$��1��$ 5��._�A   ;Microsoft.Msagl.WpfGraphControl.AutomaticGraphLayoutControl  -     Name$ graphControl��$��1��$��1��$��10q�$��Disabled=�$��Disabled=�$}�False���� $��3��$��0��$
��Left=�$��Bottom=�
�������$Z��TДля получения справки по управлению нажмите  I��