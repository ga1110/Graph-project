using Handlers;
using Structures;

namespace Structures
{
    // Публичный класс Graph, представляющий граф
    public class Graph
    {
        // Внутреннее поле adjacencyList, хранящее список смежности графа
        internal Dictionary<Vertex, List<Edge>> adjacencyList;

        public string GraphName { get; set; }

        // Публичное свойство IsDirected, определяющее, является ли граф ориентированным
        public bool IsDirected { get; private set; }

        // Инициализирует новый пустой граф с указанным направлением (ориентированный или неориентированный)
        public Graph(string name, bool isDirected = false)
        {
            GraphName = string.IsNullOrEmpty(name) ? "Nameless Graph" : name;
            IsDirected = isDirected;
            adjacencyList = new Dictionary<Vertex, List<Edge>>();
        }

        // Конструктор графа из файла
        public Graph(string filePath, string name)
        {
            GraphName = string.IsNullOrEmpty(name) ? "Nameless Graph" : name;
            adjacencyList = new Dictionary<Vertex, List<Edge>>();
            // Открываем файл для чтения
            using (StreamReader reader = new StreamReader(filePath))
            {
                // Читаем первую строку файла (тип графа: ориентированный или неориентированный)
                string firstLine = reader.ReadLine();

                // Если файл пустой, вызываем исключение
                if (firstLine == null)
                {
                    throw new InvalidDataException("Файл пуст.");
                }

                // Проверяем, является ли граф ориентированным
                IsDirected = firstLine.Trim().ToLower() == "directed";

                string currentLine;

                // Читаем файл построчно до конца
                while ((currentLine = reader.ReadLine()) != null)
                {
                    // Разбиваем строку на имена вершин и опционально вес
                    var lineElems = currentLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                    // Пропускаем пустые строки
                    if (lineElems.Length == 0)
                    {
                        continue;
                    }

                    // Получаем имя исходной вершины
                    string sourceVertexName = lineElems[0];
                    
                    // Ищем вершину по имени или создаём новую, если она не найдена
                    Vertex sourceVertex = GraphSearcher.FindVertexByName(sourceVertexName, this);
                    
                    if (sourceVertex == null)
                    {
                        sourceVertex = new Vertex(sourceVertexName);
                        GraphManager.AddVertex(sourceVertex, this);
                    }

                    // Если в строке указана вторая вершина (ребро)
                    if (lineElems.Length > 1)
                    {
                        // Получаем имя конечной вершины
                        string destinationVertexName = lineElems[1];
                        Vertex destinationVertex = GraphSearcher.FindVertexByName(destinationVertexName, this);

                        // Если конечная вершина не найдена, создаём и добавляем её
                        if (destinationVertex == null)
                        {
                            destinationVertex = new Vertex(destinationVertexName);
                            GraphManager.AddVertex(destinationVertex, this);
                        }

                        // Вес ребра (опциональный параметр)
                        double? weight = null;

                        // Если в строке указан вес
                        if (lineElems.Length > 2)
                        {
                            // Пытаемся распарсить третий элемент как вес ребра
                            if (double.TryParse(lineElems[2], out double parsedWeight))
                            {
                                weight = parsedWeight;
                            }
                        }

                        // Добавляем ребро в граф с указанными параметрами через GraphManager
                        GraphManager.AddEdge(sourceVertexName, destinationVertexName, this, weight);
                    }
                }
            }

            // Выводим сообщение об успешной загрузке графа
            Console.WriteLine($"Граф успешно загружен из файла '{filePath}'. Тип графа: {(IsDirected ? "Ориентированный" : "Неориентированный")}.");
        }

        // Конструктор копирования
        public Graph(Graph other)
        {
            GraphName = other.GraphName + "_copy";
            // Проверяем, что переданный граф не равен null
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            // Копируем значение свойства IsDirected из другого графа
            IsDirected = other.IsDirected;

            // Инициализируем новый пустой список смежности
            adjacencyList = new Dictionary<Vertex, List<Edge>>();

            // Создаем словарь для отображения вершин из оригинального графа в новые вершины копии
            var vertexMapping = new Dictionary<Vertex, Vertex>();

            // Копируем вершины
            foreach (var vertex in other.adjacencyList.Keys)
            {
                var vertexCopy = new Vertex(vertex.Name);
                adjacencyList[vertexCopy] = new List<Edge>();
                vertexMapping[vertex] = vertexCopy;
            }

            // Копируем ребра
            foreach (var kvp in other.adjacencyList)
            {
                var sourceCopy = vertexMapping[kvp.Key];
                foreach (var edge in kvp.Value)
                {
                    var destinationCopy = vertexMapping[edge.Destination];
                    var edgeCopy = new Edge(destinationCopy, edge.Weight);
                    adjacencyList[sourceCopy].Add(edgeCopy);
                }
            }
        }

    }
}