# 1. 

Вывести длины кратчайших путей от u до всех остальных вершин.

Алгоритм: Беллмана-Форда

Причина: Алгоритм Беллмана-Форда подходит для графов с отрицательными весами и позволяет находить кратчайшие пути от одной вершины до всех остальных. Он также может обнаруживать отрицательные циклы.

# 2. 

N-периферией для вершины называется множество вершин, расстояние от которых до заданной вершины больше N. Определить N-периферию для заданной вершины графа.

Алгоритм: Флойда

Причина: Алгоритм Флойда-Уоршелла находит кратчайшие пути между всеми парами вершин, что позволяет легко определить N-периферию для заданной вершины, проверяя расстояния до всех остальных вершин.

        <Menu VerticalAlignment="Top" HorizontalAlignment="Stretch">
            <!-- Пункт меню с подпунктами -->
            <MenuItem Header="Файл">
                <!-- Вложенные подпункты -->
                <MenuItem Header="Открыть" />
                <MenuItem Header="Сохранить" />
                <MenuItem Header="Выход" />
            </MenuItem>
            <!-- Пункт меню с кастомным содержимым -->
            <MenuItem Header="Действия">
                <StackPanel>
                    <Button Content="Запуск" Margin="5" />
                    <Button Content="Остановить" Margin="5"  />
                </StackPanel>
            </MenuItem>
        </Menu>
        <StackPanel VerticalAlignment="Top" HorizontalAlignment="Right" Margin="10">
            <!-- Развертываемое меню -->
            <Expander Header="Действия" IsExpanded="True" Width="200">
                <StackPanel>
                    <Button Content="Запуск" Margin="5"  />
                    <Button Content="Остановить" Margin="5"  />
                    <Button Content="Сброс" Margin="5" />
                </StackPanel>
            </Expander>

            <Expander Header="Файл" IsExpanded="False" Width="200">
                <StackPanel>
                    <Button Content="Открыть файл" Margin="5" />
                    <Button Content="Сохранить файл" Margin="5"  />
                    <Button Content="Выход" Margin="5"  />
                </StackPanel>
            </Expander>
        </StackPanel>