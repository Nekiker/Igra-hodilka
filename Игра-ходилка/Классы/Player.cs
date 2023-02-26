using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Игра_ходилка.Классы
{
    /// <summary>
    /// Игрок
    /// </summary>
    public class Player
    {

        #region Поля

        #endregion

        #region Свойства
        public int Id { get; set; }
        /// <summary>
        /// Имя игрока
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Цвет игрока
        /// </summary>
        public Color Color { get; set; }
        /// <summary>
        /// Текущая вершина в которой находится игрок
        /// </summary>
        public Vertex CurrentVertex { get; set; }
        #endregion

        #region Методы
        public void Draw(Canvas canvasControl, int marginX = 0, int marginY = 0)
        {
            if (this.CurrentVertex != null && this.CurrentVertex.Point != null)
            {
                Polygon polygon = new Polygon();
                int h = 20;
                int x = (int)this.CurrentVertex.Point.X + marginX;
                int y = (int)this.CurrentVertex.Point.Y + marginY;
                polygon.Points.Add(new Point(x, y - 2 * h / 3));
                polygon.Points.Add(new Point(x - 2 * h / 3, y + h / 3));
                polygon.Points.Add(new Point(x + 2 * h / 3, y + h / 3));
                polygon.Stroke = Brushes.Black;
                polygon.Fill = new SolidColorBrush(this.Color);
                polygon.StrokeThickness = 1;
                canvasControl.Children.Add(polygon);
            }
        }
        #endregion

        #region Конструкторы/Деструкторы
        public Player(int id = -1, string title = "")
        {
            this.Id = id;
            this.Title = title;
            this.Color = Colors.White;
            this.CurrentVertex = null;
        }
        public Player(int id, string title, Color color, Vertex vertex)
        {
            this.Id = id;
            this.Title = title;
            this.Color = color;
            this.CurrentVertex = new Vertex(vertex);
        }
        public Player() : this(-1, "")
        {

        }
        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий

        #endregion

    }
}
