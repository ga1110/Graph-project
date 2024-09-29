namespace Structures
{
    //Класс, представляющий вершину графа
    public class Vertex
    {
        public string Name { get; set; } //Имя вершины

        public Vertex(string name)
        {
            // Проверяем, что name не равно null и не состоит только из пробелов
            // Trim() Удаляет все начальные и конечные пробелы из текущей строки.
            if (name == null || name.Trim() == "")
            {
                throw new ArgumentException("Имя вершины не может быть пустым или пробельным.", nameof(name));
            }

            Name = name;
        }

        public override bool Equals(object obj)
        {
            if (obj is Vertex other)
            {
                // Сравниваем имена, приводя их к нижнему регистру
                return Name.ToLower() == other.Name.ToLower();
            }
            return false;
        }
        public override string ToString()
        {
            return Name;
        }
        public override int GetHashCode()
        {
            return Name.ToLower().GetHashCode();
        }
    }
}