# 1. 

������� ����� ���������� ����� �� u �� ���� ��������� ������.

��������: ��������-�����

�������: �������� ��������-����� �������� ��� ������ � �������������� ������ � ��������� �������� ���������� ���� �� ����� ������� �� ���� ���������. �� ����� ����� ������������ ������������� �����.

# 2. 

N-���������� ��� ������� ���������� ��������� ������, ���������� �� ������� �� �������� ������� ������ N. ���������� N-��������� ��� �������� ������� �����.

��������: ������

�������: �������� ������-�������� ������� ���������� ���� ����� ����� ������ ������, ��� ��������� ����� ���������� N-��������� ��� �������� �������, �������� ���������� �� ���� ��������� ������.

        <Menu VerticalAlignment="Top" HorizontalAlignment="Stretch">
            <!-- ����� ���� � ����������� -->
            <MenuItem Header="����">
                <!-- ��������� ��������� -->
                <MenuItem Header="�������" />
                <MenuItem Header="���������" />
                <MenuItem Header="�����" />
            </MenuItem>
            <!-- ����� ���� � ��������� ���������� -->
            <MenuItem Header="��������">
                <StackPanel>
                    <Button Content="������" Margin="5" />
                    <Button Content="����������" Margin="5"  />
                </StackPanel>
            </MenuItem>
        </Menu>
        <StackPanel VerticalAlignment="Top" HorizontalAlignment="Right" Margin="10">
            <!-- �������������� ���� -->
            <Expander Header="��������" IsExpanded="True" Width="200">
                <StackPanel>
                    <Button Content="������" Margin="5"  />
                    <Button Content="����������" Margin="5"  />
                    <Button Content="�����" Margin="5" />
                </StackPanel>
            </Expander>

            <Expander Header="����" IsExpanded="False" Width="200">
                <StackPanel>
                    <Button Content="������� ����" Margin="5" />
                    <Button Content="��������� ����" Margin="5"  />
                    <Button Content="�����" Margin="5"  />
                </StackPanel>
            </Expander>
        </StackPanel>