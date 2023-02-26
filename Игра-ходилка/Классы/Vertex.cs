using System.Windows;
using System.Windows.Media;

namespace Игра_ходилка.Классы
{
    /// <summary>
    /// Тип вершины
    /// </summary>
    public enum VertexType
    {
        Standart,
        Start,
        Finish
    }

    /// <summary>
    /// Вершина графа
    /// </summary>
    public class Vertex
    {

        #region Поля


        #endregion

        #region Свойства
        /// <summary>
        /// Id вершины
        /// </summary>
        public int Id { set; get; }
        /// <summary>
        /// Название вершины
        /// </summary>
        public string Title { set; get; }
        /// <summary>
        /// Тип вершины
        /// </summary>
        public VertexType Type { set; get; }
        /// <summary>
        /// Цвет вершины (внутренний)
        /// </summary>
        public Color ColorInner
        {
            get
            {
                switch (this.Type)
                {
                    case VertexType.Standart:
                        return Colors.White;
                    case VertexType.Start:
                        return Colors.Wheat;
                    case VertexType.Finish:
                        return Colors.LightCyan;
                    default:
                        return Colors.Violet;
                }

            }
        }
        /// <summary>
        /// Цвет вершины (внешний)
        /// </summary>
        public Color ColorOuter
        {
            get
            {
                switch (this.Type)
                {
                    case VertexType.Standart:
                        return Colors.Black;
                    case VertexType.Start:
                        return Colors.Red;
                    case VertexType.Finish:
                        return Colors.Red;
                    default:
                        return Colors.White;
                }

            }
        }
        /// <summary>
        /// Точка координат на холсте (для графического отображения)
        /// </summary>
        public Point Point { set; get; }
        #endregion

        #region Методы

        #endregion

        #region Конструкторы/Деструкторы
        /// <summary>
        /// Создает вершину с указанными параметрами
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="title">название</param>
        /// <param name="type">тип</param>
        public Vertex(int id, Point point, string title = "", VertexType type = VertexType.Standart)
        {
            this.Id = id;
            this.Title = title;
            this.Type = type;
            this.Point = new Point(point.X, point.Y);
        }
        /// <summary>
        /// Создает вершину с указанными параметрами
        /// </summary>
        /// <param name="title">название</param>
        /// <param name="type">тип</param>
        public Vertex(Point point, string title = "", VertexType type = VertexType.Standart) : this(-1, point, title, type)
        {

        }
        /// <summary>
        /// Создает вершину с координатами 0,0
        /// </summary>
        public Vertex() : this(new Point(0, 0))
        {

        }
        /// <summary>
        /// Копирует указанную вершину
        /// </summary>
        /// <param name="vertex"></param>
        public Vertex(Vertex vertex) : this(vertex.Id, vertex.Point, vertex.Title, vertex.Type)
        {

        }
        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий

        #endregion

    }
}
