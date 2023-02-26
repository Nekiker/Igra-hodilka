using Logger;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Игра_ходилка.Классы
{
    /// <summary>
    /// Игра-ходилка
    /// </summary>
    public class Game
    {

        #region Поля
        int currentPlayerIndex;
        #endregion

        #region Свойства
        /// <summary>
        /// Лог игры
        /// </summary>
        public Log Log { set; get; }
        /// <summary>
        /// Игроки
        /// </summary>
        public List<Player> Players { get; set; }
        /// <summary>
        /// Граф
        /// </summary>
        public Graph<int> Graph { get; set; }
        /// <summary>
        /// Индекс текущего игрока
        /// </summary>
        public int CurrentPlayerIndex
        {
            get => this.currentPlayerIndex;
            set
            {
                if (value < 0)
                {
                    throw new Exception("Индекс игрока не может быть меньше нуля");
                }
                if (this.Players.Count > 0)
                {
                    this.currentPlayerIndex = this.Players != null ? value % this.Players.Count : 0;
                }
                else
                {
                    this.currentPlayerIndex = 0;
                }
            }
        }
        /// <summary>
        /// Текущий игрок
        /// </summary>
        public Player CurrentPlayer => this.Players != null ? this.Players[this.CurrentPlayerIndex] : null;
        /// <summary>
        /// Началась игра или нет
        /// </summary>
        public bool IsStarted { set; get; }
        #endregion

        #region Методы
        /// <summary>
        /// Бросок кубика
        /// </summary>
        /// <returns>Выпавшее число</returns>
        public int Roll()
        {
            Random random = new Random();
            return random.Next(1, 7);
        }
        /// <summary>
        /// Отрисовывает игру на холсте
        /// </summary>
        /// <param name="canvasControl">холст для отрисовки</param>
        public void Draw(Canvas canvasControl)
        {
            canvasControl.Children.Clear();
            this.Graph.Draw(canvasControl);
            foreach (var vertiex in this.Graph.Vertices)
            {
                int step = 10;
                for (int i = 0, j = 0; i < this.Players.Count; i++)
                {
                    if (vertiex.Point == this.Players[i].CurrentVertex.Point)
                    {
                        this.Players[i].Draw(canvasControl, 0, -(j++) * step);
                    }
                }
            }
        }
        /// <summary>
        /// Делает следующий ход в игре
        /// </summary>
        /// <param name="roll">количество клеток, на которые перемещается фишка текущего игрока</param>
        public void Next(int roll)
        {
            if (this.IsStarted)
            {
                this.Log.Add(String.Format(Common.Strings.Messages.diceRollFormated, roll));
                this.Log.Add(Common.Strings.Messages.currentPlayerMoveFormated, this.CurrentPlayer.Title);
                if (roll >= 0 && roll < this.Graph.Vertices.Count)
                {
                    for (int i = 0; i < roll; i++)
                    {
                        try
                        {
                            this.Next();
                        }
                        catch (Exception ex)
                        {
                            this.IsStarted = false;
                            this.Log.Add(ex.Message, MessageType.Error);
                            this.Log.Add(Common.Strings.Messages.playerWonFormatted, this.CurrentPlayer.Title);
                        }
                    }
                    this.CurrentPlayerIndex++;
                }
            }
            else
            {
                this.Log.Add(Common.Strings.Messages.gameNotStarted, MessageType.Error);
            }
        }
        /// <summary>
        /// Переносит фишку текущего игрока на одну клетку вперед.
        /// Передачи хода не происходит.
        /// </summary>
        public void Next()
        {
            if (this.IsStarted)
            {
                List<Vertex> neighbours = this.Graph.GetNeighbours(this.CurrentPlayer.CurrentVertex);
                if (neighbours.Count > 0)
                {
                    this.CurrentPlayer.CurrentVertex = new Vertex(neighbours[0]);
                    if (this.CurrentPlayer.CurrentVertex.Type == VertexType.Finish)
                    {
                        throw new Exception(Common.Strings.Messages.playerReachFinish);

                    }
                }
                else
                {
                    throw new Exception(Common.Strings.Errors.noVertexToMove);
                }
            }
            else
            {
                this.Log.Add(Common.Strings.Messages.gameNotStarted, MessageType.Error);
            }
        }
        /// <summary>
        /// Генерирует граф и расставляет игроков
        /// </summary>
        public void Generate()
        {
            this.Graph.Generate(
            40,
            50,
            10,
            20,
            1,
            1,
            GenerateType.Snake,
            600,
            800);
            if (this.Graph.Vertices.Count > 2)
            {
                this.Graph.Vertices[0].Type = VertexType.Start;
                this.Graph.Vertices[this.Graph.Vertices.Count - 1].Type = VertexType.Finish;
                this.Players.Add(new Player(id: 1, title: "Красный", color: Colors.Red, this.Graph[0]));
                this.Players.Add(new Player(id: 2, title: "Синий", color: Colors.Blue, this.Graph[0]));
                this.Players.Add(new Player(id: 3, title: "Зеленый", color: Colors.Green, this.Graph[0]));
                this.Players.Add(new Player(id: 4, title: "Желтый", color: Colors.Yellow, this.Graph[0]));
            }
        }
        #endregion

        #region Конструкторы/Деструкторы
        public Game(List<Player> players, Graph<int> graph)
        {
            this.Players = players ?? throw new ArgumentNullException(nameof(players));
            this.Graph = graph ?? throw new ArgumentNullException(nameof(graph));
            this.CurrentPlayerIndex = 0;
            this.IsStarted = true;
            this.Log = new Log();
            this.Log.Add(Common.Strings.Messages.gameStarted);
        }
        public Game() : this(new List<Player>(), new Graph<int>())
        {

        }
        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий

        #endregion

    }
}
