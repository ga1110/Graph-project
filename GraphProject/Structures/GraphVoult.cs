using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Structures
{
    public class GraphVault
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
            Graph tmpGraph = new Graph(_graphs[_currentGraph]);
            _graphs.Add(tmpGraph);
            _currentGraph = _graphs.Count() - 1;
        }

        // Метод смены текущего графа
        public bool ChangeCurrentGraph(int newCurrentGrahp)
        {
            if (CheckUserInput(newCurrentGrahp))
            {
                _currentGraph = newCurrentGrahp - 1;
                return true;
            }
            return false; 
        }

        // Метод удаления графа по номеру
        public bool RemoveGraph(int graphToDelete)
        {
            if(CheckUserInput(graphToDelete))
            {
                _graphs.RemoveAt(graphToDelete - 1);
                return true;
            }
            return false;
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
