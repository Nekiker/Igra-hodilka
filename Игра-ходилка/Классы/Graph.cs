using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Игра_ходилка.Классы
{
    public enum GenerateType
    {
        Random,
        LinkedList,
        Snake,
    }
    /// <summary>
    /// Граф
    /// </summary>
    public class Graph<WeightType>
    {

        #region Поля
        int idCurrent;
        #endregion

        #region Свойства
        /// <summary>
        /// Список вершин
        /// </summary>
        public List<Vertex> Vertices { get; }
        /// <summary>
        /// Матрица смежности
        /// </summary>
        public Matrix<WeightType> Matrix { get; }

        public Point TopLeft => new Point(
                    this.Vertices.Min(x => x.Point.X),
                    this.Vertices.Min(x => x.Point.Y));
        public Point BottomRight => new Point(
                    this.Vertices.Max(x => x.Point.X),
                    this.Vertices.Max(x => x.Point.Y));
        #endregion

        #region Методы
        /// <summary>
        /// Добавляет вершину
        /// </summary>
        /// <param name="vertex">добавляемая вершина</param>
        public void Add(Vertex vertex)
        {
            vertex.Id = this.idCurrent++;
            this.Vertices.Add(vertex);
            this.Matrix.AddRow();
            this.Matrix.AddColumn();
        }
        /// <summary>
        /// Задает расстояние между двумя вершинами
        /// </summary>
        /// <param name="fromVertex">исходящая вершина</param>
        /// <param name="toVertex">индекс столбца</param>
        /// <param name="edgeWeight">вес ребра</param>
        public void Set(int fromVertex, int toVertex, WeightType edgeWeight)
        {
            if (!this.Matrix.IsIndexCorrect(fromVertex, toVertex))
            {
                throw new Exception("Указаны некорректные индкесы строки/столбца!");
            }

            this.Matrix[fromVertex][toVertex] = edgeWeight;
        }
        /// <summary>
        /// Получает расстояние между двумя вершинами
        /// </summary>
        /// <param name="fromVertex">исходящая вершина</param>
        /// <param name="toVertex">конечная вершина</param>
        public WeightType Get(int fromVertex, int toVertex)
        {
            if (!this.Matrix.IsIndexCorrect(fromVertex, toVertex))
            {
                throw new Exception("Указаны некорректные идкесы строки/столбца!");
            }

            return this.Matrix[fromVertex][toVertex];
        }
        /// <summary>
        /// Получает вершину по id
        /// </summary>
        /// <param name="idVertex">id вершины</param>
        /// <returns></returns>
        public Vertex Get(int idVertex)
        {
            return this.Vertices.FirstOrDefault(x => x.Id == idVertex);
        }
        /// <summary>
        /// Получает соседей заданной вершины
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public List<Vertex> GetNeighbours(Vertex vertex)
        {
            List<Vertex> result = new List<Vertex>();
            int index = -1;
            for (int i = 0; i < this.Vertices.Count; i++)
            {
                if (this.Vertices[i].Id == vertex.Id)
                {
                    index = i;
                    break;
                }
            }

            if (index != -1)
            {
                for (int i = 0; i < this.Matrix[index].Count; i++)
                {
                    if (!this.Matrix[index][i].Equals(0))
                    {
                        result.Add(this.Vertices[i]);
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// Генерирует граф с указанными параметрами
        /// </summary>
        /// <param name="vertexMinCount"></param>
        /// <param name="vertexMaxCount"></param>
        /// <param name="edgesMinCount"></param>
        /// <param name="edgesMaxCount"></param>
        /// <param name="weightMin"></param>
        /// <param name="weightMax"></param>
        public void Generate(
            int vertexMinCount,
            int vertexMaxCount,
            int edgesMinCount,
            int edgesMaxCount,
            int weightMin,
            int weightMax,
            GenerateType generateType = GenerateType.Random,
            int Xmax = 1024,
            int Ymax = 1024)
        {
            #region Проверка на отрицательные значения
            if (vertexMinCount < 0)
            {
                throw new Exception(
                    String.Format(Common.Strings.Errors.negativeArgumentFormated, "vertexMinCount"));
            }
            if (vertexMaxCount < 0)
            {
                throw new Exception(
                    String.Format(Common.Strings.Errors.negativeArgumentFormated, "vertexMaxCount"));
            }
            if (edgesMinCount < 0)
            {
                throw new Exception(
                    String.Format(Common.Strings.Errors.negativeArgumentFormated, "edgesMinCount"));
            }
            if (edgesMaxCount < 0)
            {
                throw new Exception(
                    String.Format(Common.Strings.Errors.negativeArgumentFormated, "edgesMaxCount"));
            }
            if (weightMin < 0)
            {
                throw new Exception(
                    String.Format(Common.Strings.Errors.negativeArgumentFormated, "weightMin"));
            }
            if (weightMax < 0)
            {
                throw new Exception(
                    String.Format(Common.Strings.Errors.negativeArgumentFormated, "weightMax"));
            }
            #endregion

            #region Проверка диапазонов
            if (vertexMinCount > vertexMaxCount)
            {
                throw new Exception(String.Format(Common.Strings.Errors.leftBiggerRightFormated, "vertexMinCount", "vertexMaxCount"));
            }

            if (edgesMinCount > edgesMaxCount)
            {
                throw new Exception(String.Format(Common.Strings.Errors.leftBiggerRightFormated, "edgesMinCount", "edgesMaxCount"));
            }

            if (weightMin > weightMax)
            {
                throw new Exception(String.Format(Common.Strings.Errors.leftBiggerRightFormated, "weightMin", "weightMax"));
            }
            #endregion

            Random rand = new Random();
            int verticesCount = rand.Next(vertexMinCount, vertexMaxCount);
            int edgesCount = rand.Next(edgesMinCount, edgesMaxCount);
            this.Vertices.Clear();
            for (int i = 0; i < verticesCount; i++)
            {
                this.Add(new Vertex(point: new Point(rand.Next(0, Xmax), rand.Next(0, Ymax)), title: i.ToString()));
            }

            this.Matrix.Clear();
            this.Matrix.Rows = verticesCount;
            this.Matrix.Columns = verticesCount;
            if (generateType == GenerateType.Random)
            {
                for (int i = 0; i < edgesCount; i++)
                {
                    this.Set
                        (
                            rand.Next(0, verticesCount),
                            rand.Next(0, verticesCount),
                            (WeightType)(object)rand.Next(weightMin, weightMax)
                        );
                }
            }
            if (generateType == GenerateType.LinkedList)
            {
                for (int i = 0; i < this.Matrix.Rows - 1; i++)
                {
                    this.Set
                        (
                            i,
                            i + 1,
                            (WeightType)(object)rand.Next(weightMin, weightMax)
                        );
                }
            }
            if (generateType == GenerateType.Snake)
            {
                for (int i = 0; i < this.Matrix.Rows - 1; i++)
                {
                    this.Set
                        (
                            i,
                            i + 1,
                            (WeightType)(object)rand.Next(weightMin, weightMax)
                        );
                }
                int startX = Math.Min(50, Xmax);
                int startY = Math.Min(50, Ymax);
                int stepX = 100;
                int directionX = 1;
                for (int i = 0; i < this.Vertices.Count; i++)
                {
                    this.Vertices[i].Point = new Point(startX, startY);
                    if (startX + directionX * stepX >= Xmax || startX + directionX * stepX <= 0)
                    {
                        directionX *= -1;
                        startY += stepX;
                    }
                    else
                    {
                        startX += directionX * stepX;
                    }
                }
            }
        }

        #region Рисование
        void DrawCircle(Canvas canvasControl, Point point, Color colorInner, Color colorOuter, int radius = 40)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Stroke = new SolidColorBrush(colorOuter);
            ellipse.Fill = new SolidColorBrush(colorInner);
            ellipse.Width = radius;
            ellipse.Height = radius;
            ellipse.StrokeThickness = 2;
            canvasControl.Children.Add(ellipse);
            Canvas.SetLeft(ellipse, point.X - radius / 2);
            Canvas.SetTop(ellipse, point.Y - radius / 2);
        }
        void DrawString(Canvas canvasControl, Point point, Color color, string text)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = text;
            textBlock.Foreground = new SolidColorBrush(color);
            canvasControl.Children.Add(textBlock);
            textBlock.Measure(new Size(16, Double.PositiveInfinity));
            textBlock.Arrange(new Rect(textBlock.DesiredSize));
            Canvas.SetLeft(textBlock, point.X - textBlock.ActualWidth / 2);
            Canvas.SetTop(textBlock, point.Y - textBlock.ActualHeight / 2);
        }
        void DrawCirlce(Canvas canvasControl, Vertex vertex)
        {
            this.DrawCircle(canvasControl, vertex.Point, vertex.ColorInner, vertex.ColorOuter);
            this.DrawString(canvasControl, vertex.Point, vertex.ColorOuter, vertex.Title);
        }
        void DrawLine(Canvas canvasControl, Point from, Point to, Color color)
        {
            Line line = new Line();
            line.Stroke = new SolidColorBrush(color);
            line.X1 = from.X;
            line.Y1 = from.Y;
            line.X2 = to.X;
            line.Y2 = to.Y;
            canvasControl.Children.Add(line);
        }
        void DrawLine(Canvas canvasControl, Vertex from, Vertex to, Color color)
        {
            this.DrawLine(canvasControl, from.Point, to.Point, color);
        }
        public void Draw(Canvas canvasControl, int margin = 50)
        {
            if (this.Vertices.Count > 0)
            {
                for (int i = 0; i < this.Vertices.Count - 1; i++)
                {
                    this.DrawLine(canvasControl, this.Vertices[i], this.Vertices[i + 1], Colors.Black);
                    this.DrawCirlce(canvasControl, this.Vertices[i]);
                }
                this.DrawCirlce(canvasControl, this.Vertices[this.Vertices.Count - 1]);
            }
            canvasControl.Arrange(new Rect(new Point(0, 0), this.BottomRight));
            canvasControl.Width = this.BottomRight.X + margin;
            canvasControl.Height = this.BottomRight.Y + margin;
        }
        #endregion
        
        #endregion

        #region Конструкторы/Деструкторы
        public Graph()
        {
            this.idCurrent = 1;
            this.Vertices = new List<Vertex>();
            this.Matrix = new Matrix<WeightType>();
        }
        public Graph(int verticesCount) : this()
        {
            for (int i = 0; i < verticesCount; i++)
            {
                this.Add(new Vertex());
            }
        }

        public Graph(
            int vertexMinCount,
            int vertexMaxCount,
            int edgesMinCount,
            int edgesMaxCount,
            int weightMin,
            int weightMax) : this()
        {
            this.Generate(vertexMinCount, vertexMaxCount, edgesMinCount, edgesMaxCount, weightMin, weightMax);
        }
        #endregion

        #region Операторы
        public Vertex this[int index]
        {
            set
            {
                if (index < 0 || index >= this.Vertices.Count)
                {
                    throw new Exception("Указан некорректный индекс веришны");
                }

                this.Vertices[index] = value;
            }
            get
            {
                if (index < 0 || index >= this.Vertices.Count)
                {
                    throw new Exception("Указан некорректный индекс веришны");
                }

                return this.Vertices[index];
            }
        }
        #endregion

        #region Обработчики событий

        #endregion

    }
}
