using System;

namespace GraphProject.Structures
{
    //Класс, представляющий вершину графа
    /// <summary>
    /// Представляет вершину графа.
    /// </summary>
    public class Vertex
    {
        /// <summary>
        /// Имя вершины.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Уникальный идентификатор вершины.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Vertex"/>.
        /// </summary>
        /// <param name="name">Имя вершины.</param>
        /// <exception cref="ArgumentException">Если имя вершины является пустым или состоит только из пробелов.</exception>
        public Vertex(string name)
        {
            if (name == null || name.Trim() == "")
            {
                throw new ArgumentException("Имя вершины не может быть пустым или пробельным.", nameof(name));
            }

            Name = name;
            Id = -1;
        }

        /// <summary>
        /// Определяет, равны ли две вершины.
        /// </summary>
        /// <param name="obj">Объект для сравнения.</param>
        /// <returns>Значение <see langword="true"/>, если объекты равны, в противном случае - <see langword="false"/>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Vertex other)
            {
                return Name.Equals(other.Name, StringComparison.CurrentCultureIgnoreCase);
            }
            return false;
        }

        /// <summary>
        /// Возвращает хэш-код для текущего объекта.
        /// </summary>
        /// <returns>32-битное целое число со знаком, являющееся хэш-кодом для текущего объекта.</returns>
        public override int GetHashCode()
        {
            return Name.ToLower().GetHashCode();
        }

        /// <summary>
        /// Устанавливает идентификатор вершины.
        /// </summary>
        /// <param name="id">Идентификатор вершины.</param>
        public void SetId(int id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Возвращает строковое представление вершины.
        /// </summary>
        /// <returns>Строковое представление вершины.</returns>
        public override string ToString()
        {
            return Name.ToString();
        }
    }

}