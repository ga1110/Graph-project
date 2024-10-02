using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Structures
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
        public int? GetCurrentGraphNum()
        {
            if (_graphs.Count() == 0)
            {
                return null;
            }
            else
            {
                return _currentGraph + 1;
            }
        }

        // Метод добавления нового графа в список
        public void AddNewGraph(Graph graph)
        {
            _graphs.Add(graph);
            _currentGraph = _graphs.Count() - 1;
        }

        // Метод который заменяет текущий граф 
        public void ReplaceCurrentGraph(Graph graph)
        {
            _graphs[_currentGraph] = graph;
        }

        // Метод копирования текущего графа
        public void CopyCurrentGrahp()
        {
            _graphs.Add(_graphs[_currentGraph]);
            _currentGraph = _graphs.Count() - 1;
        }

        // Метод смены текущего графа
        public void ChangeCurrentGraph(int newCurrentGrahp)
        {
            CheckUserInput(newCurrentGrahp);
            _currentGraph = newCurrentGrahp - 1;
        }

        // Метод удаления графа по номеру
        public void RemoveGraph(int graphToDelete)
        {
            if(CheckUserInput(graphToDelete))
            {
                _graphs.RemoveAt(graphToDelete - 1);
                return;
            }
            Console.WriteLine($"Графа с номером {graphToDelete} не существует");
            return;
        }
        
        // Проверка ввода пользователя
        private bool CheckUserInput(int newCurrentGrahp)
        {
            if (newCurrentGrahp - 1 < 0 || newCurrentGrahp - 1 >= _graphs.Count() || newCurrentGrahp < 0)
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
