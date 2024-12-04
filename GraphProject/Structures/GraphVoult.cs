using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using GraphProject.Structures;

namespace GraphProject.Structures
{
    public class GraphVoult
    {
        private List<Graph> _graphs = new List<Graph>();
        private int _currentGraph = 0;

        // Метод получения списка графов
        public List<Graph> GetGrahpsList()
        {
            return _graphs;
        }

        // Метод получения текущего графа
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

        // Метод получения номера текущего графа
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

        // Метод добавления нового графа в список
        public void AddNewGraph(Graph graph)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph), "Граф - пустой и/или равен null");

            _graphs.Add(graph);
            _currentGraph = _graphs.Count() - 1;
        }

        // Метод который заменяет текущий граф 
        public void ReplaceCurrentGraph(Graph graph)
        {
            if (graph == null)
                throw new ArgumentNullException(nameof(graph), "Граф - пустой и/или равен null");

            _graphs[_currentGraph] = graph;
        }

        // Метод копирования текущего графа
        public void CopyCurrentGrahp()
        {
            Graph tmpGraph = new Graph(_graphs[_currentGraph]);
            _graphs.Add(tmpGraph);
            _currentGraph = _graphs.Count();
        }

        // Метод смены текущего графа
        public bool ChangeCurrentGraph(int newCurrentGrahp)
        {
            if (CheckUserInput(newCurrentGrahp))
            {
                _currentGraph = newCurrentGrahp;
                return true;
            }
            return false;
        }

        // Метод удаления графа по номеру
        public void RemoveGraph(int graphToDelete)
        {
            if (CheckUserInput(graphToDelete))
            {
                if(_graphs.Count() - 1 == 0)
                {
                    throw new Exception("Нельзя удалить последний граф");
                }    
                _graphs.RemoveAt(graphToDelete);
                return;
            }
            throw new Exception("Не удалось удалить граф из списка");
        }

        // Проверка ввода пользователя
        private bool CheckUserInput(int newCurrentGrahp)
        {
            if (newCurrentGrahp < 0 || newCurrentGrahp >= _graphs.Count())
            {
                return false;
            }
            return true;
        }

        // Проверка пустой ли список
        public bool isVoultEmpty()
        {
            return !(_graphs.Count() > 0);
        }
    }
}
