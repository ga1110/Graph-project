   M S B A M L   `   `   `  ���� f  bMicrosoft.Msagl.WpfGraphControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=6baa2c1345e83b6ezVclr-namespace:Microsoft.Msagl.WpfGraphControl;assembly=Microsoft.Msagl.WpfGraphControlMicrosoft.Msagl.WpfGraphControl   GraphVisualization#   GraphVisualization.MainWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     hmsaglVclr-namespace:Microsoft.Msagl.WpfGraphControl;assembly=Microsoft.Msagl.WpfGraphControl     Aav2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     #wpfgraphcontrolhttp://mlsagl     � WindowState$  	Maximized=� � 
ResizeMode$ NoResize=� � Title$ Graph Visualization��.��� -    �Name$ mainGrid��$
}�True��.+���  �� Orientation$ 
Horizontal=�$��10q�$	��Top=�$
��Left=� K� ZIndex$ 2��.+��� $ Vertical=�$��10q�.+��� -    ��Name$ CreateGraphButton��$��Создать граф��$	��120��$��40��
����ح�$
��Bold"�$��10,10,10,10q�$ 2���� $ Vertical=�$��10q�$	��Top=�$
��Left=�$ 2��.+��� -   $ AddEdgeButton��$!��Добавить ребро��$	��120��$��40��
����ح�$
��Bold"�$��10,10,10,10q�$ 2���� -   $ RemoveEdgeButton��$��Удалить ребро��$	��120��$��40��
����ح�$
��Bold"�$��10,10,10,10q�$ 2���� -    ��Name$ InfoTextBox��$��Bottom=�$
��Left=�$	��300��$	��200��$��	10,0,0,10q� �� 
IsReadOnly$
 True��	 �� TextWrapping$
	 Wrap=�"
 �� VerticalScrollBarVisibility$

 Auto=�$ �� HorizontalScrollBarVisibility$ Disabled=�
�������
���   �$��2q�$��15(�$ 2��$)��#Информация о графе:��A   ;Microsoft.Msagl.WpfGraphControl.AutomaticGraphLayoutControl  -     Name$ graphControl��$��Stretch=�$��Stretch=�$��10q�$��Disabled=�$��Disabled=�$
}�True��