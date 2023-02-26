using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Игра_ходилка.Классы;

namespace Тесты
{
    [TestClass]
    public class TestGame
    {
        [TestMethod]
        public void Create()
        {
            Random random = new Random();
            int playersCount = random.Next(2, 10),
                verticesCount = random.Next(5, 100);
            List<Player> players = new List<Player>();

            for (int i = 1; i <= playersCount; i++)
            {
                Player player = new Player(i, $"Игрок №{i}");
                players.Add(player);
            }

            Graph<int> graph = new Graph<int>(verticesCount);
            Game game = new Game(players, graph);
            Assert.AreEqual(verticesCount, game.Graph.Vertices.Count);
            Assert.AreEqual(playersCount, game.Players.Count);
        }
    }
}
