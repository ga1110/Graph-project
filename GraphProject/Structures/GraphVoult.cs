using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphProject.Structures
{
    /// <summary>
    /// Класс, представляющий хранилище графов.
    /// </summary>
    public class GraphVoult
    {
        /// <summary>
        /// Список графов.
        /// </summary>
        private List<Graph> _graphs = new List<Graph>();

        /// <summary>
        /// Индекс текущего графа.
        /// </summary>
        private int _currentGraph = 0;

        /// <summary>
        /// Метод получения списка графов.
        /// </summary>
        /// <returns>Список графов.</returns>
        public List<Graph> GetGrahpsList()
        {
            return _graphs;
        }

        /// <summary>
        /// Метод получения текущего графа.
        /// </summary>
        /// <returns>Текущий граф.</returns>
        public Graph? GetCurrentGraph()
        {
            if (_graphs.Count() == 0)
            {
                return null;
            }
            else
            {
                return _graphs[_currentGraph];
            }
        }

        /// <summary>
        /// Метод получения номера текущего графа.
        /// </summary>
        /// <returns>Номер текущего графа.</returns>
        public int? GetCurrentGraphIndex()
        {
            if (_graphs.Count() == 0)
            {
                return null;
            }
            else
            {
                return _currentGraph;
            }
        }

        /// <summary>
        /// Метод добавления нового графа в список.
        /// </summary>
        /// <param name="graph">Граф, который нужно добавить.</param>
        public void AddNewGraph(Graph graph)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph), "Граф - пустой и/или равен null");

            _graphs.Add(graph);
            _currentGraph = _graphs.Count() - 1;
        }

        /// <summary>
        /// Метод замены текущего графа.
        /// </summary>
        /// <param name="graph">Граф, на который нужно заменить текущий.</param>
        public void ReplaceCurrentGraph(Graph graph)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph), "Граф - пустой и/или равен null");

            _graphs[_currentGraph] = graph;
        }

        /// <summary>
        /// Метод копирования текущего графа.
        /// </summary>
        public void CopyCurrentGrahp()
        {
            Graph tmpGraph = new Graph(_graphs[_currentGraph]);
            _graphs.Add(tmpGraph);
            _currentGraph = _graphs.Count();
        }

        /// <summary>
        /// Метод смены текущего графа.
        /// </summary>
        /// <param name="newCurrentGrahp">Новый текущий граф.</param>
        /// <returns>Результат смены текущего графа.</returns>
        public bool ChangeCurrentGraph(int newCurrentGrahp)
        {
            if (CheckUserInput(newCurrentGrahp))
            {
                _currentGraph = newCurrentGrahp;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Метод удаления графа по номеру.
        /// </summary>
        /// <param name="graphToDelete">Номер графа, который нужно удалить.</param>
        public void RemoveGraph(int graphToDelete)
        {
            if (CheckUserInput(graphToDelete))
            {
                if (_graphs.Count() - 1 == 0)
                {
                    throw new Exception("Нельзя удалить последний граф");
                }
                _graphs.RemoveAt(graphToDelete);
                return;
            }
            throw new Exception("Не удалось удалить граф из списка");
        }

        /// <summary>
        /// Проверка ввода пользователя.
        /// </summary>
        /// <param name="newCurrentGrahp">Номер графа, который нужно проверить.</param>
        /// <returns>Результат проверки.</returns>
        private bool CheckUserInput(int newCurrentGrahp)
        {
            if (newCurrentGrahp < 0 || newCurrentGrahp >= _graphs.Count())
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Проверка пустой ли список.
        /// </summary>
        /// <returns>Результат проверки.</returns>
        public bool IsEmpty()
        {
            return !(_graphs.Count() > 0);
        }
    }

}
