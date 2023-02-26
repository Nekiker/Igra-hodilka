using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Игра_ходилка.Классы;

namespace Тесты
{
    [TestClass]
    public class TestGraph
    {
        [TestMethod]
        public void Create()
        {
            Graph<int> graph = new Graph<int>();
            Assert.IsNotNull(graph.Vertices);
            Assert.IsNotNull(graph.Matrix);
        }

        [TestMethod]
        public void Generate()
        {
            Random random = new Random();

            Graph<int> graph = new Graph<int>();
            graph.Generate(
                random.Next(5, 10),
                random.Next(50, 100),
                random.Next(5, 10),
                random.Next(50, 100),
                random.Next(1, 4),
                random.Next(5, 10));
            Assert.AreEqual(graph.Vertices.Count, graph.Matrix.Rows);
            Assert.AreEqual(graph.Vertices.Count, graph.Matrix.Columns);
            #region Проверка исключений на отрицательные параметры
            try
            {
                graph.Generate(
                    -1,
                    random.Next(50, 100),
                    random.Next(5, 10),
                    random.Next(50, 100),
                    random.Next(1, 4),
                    random.Next(5, 10));
            }
            catch (Exception ex)
            {
                Assert.AreEqual(String.Format(Common.Strings.Errors.negativeArgumentFormated, "vertexMinCount"), ex.Message);
            }
            try
            {
                graph.Generate(
                    random.Next(50, 100),
                    -1,
                    random.Next(5, 10),
                    random.Next(50, 100),
                    random.Next(1, 4),
                    random.Next(5, 10));
            }
            catch (Exception ex)
            {
                Assert.AreEqual(String.Format(Common.Strings.Errors.negativeArgumentFormated, "vertexMaxCount"), ex.Message);
            }
            try
            {
                graph.Generate(
                    random.Next(50, 100),
                    random.Next(50, 101),
                    -1,
                    random.Next(50, 100),
                    random.Next(1, 4),
                    random.Next(5, 10));
            }
            catch (Exception ex)
            {
                Assert.AreEqual(String.Format(Common.Strings.Errors.negativeArgumentFormated, "edgesMinCount"), ex.Message);
            }
            try
            {
                graph.Generate(
                    random.Next(50, 100),
                    random.Next(50, 101),
                    random.Next(50, 101),
                    -1,
                    random.Next(1, 4),
                    random.Next(5, 10));
            }
            catch (Exception ex)
            {
                Assert.AreEqual(String.Format(Common.Strings.Errors.negativeArgumentFormated, "edgesMaxCount"), ex.Message);
            }
            try
            {
                graph.Generate(
                    random.Next(50, 100),
                    random.Next(50, 101),
                    random.Next(50, 101),
                    random.Next(50, 101),
                    -1,
                    random.Next(5, 10));
            }
            catch (Exception ex)
            {
                Assert.AreEqual(String.Format(Common.Strings.Errors.negativeArgumentFormated, "weightMin"), ex.Message);
            }
            try
            {
                graph.Generate(
                    random.Next(50, 100),
                    random.Next(50, 101),
                    random.Next(50, 101),
                    random.Next(50, 101),
                    random.Next(5, 10),
                    -1);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(String.Format(Common.Strings.Errors.negativeArgumentFormated, "weightMax"), ex.Message);
            }
            #endregion
            //TODO: проверка на заполнение
            int count = graph.Matrix.ZeroCount;

        }

        [TestMethod]
        public void Add()
        {
            int vertexCount;
            Random random = new Random();
            vertexCount = random.Next(1, 100);
            Graph<int> graph = new Graph<int>(vertexCount);
            for (int i = 0; i < vertexCount; i++)
            {
                Assert.AreEqual(graph[i].Id, i + 1);
            }
            Assert.AreEqual(vertexCount, graph.Vertices.Count);
            Assert.AreEqual(vertexCount, graph.Matrix.Rows);
            Assert.AreEqual(vertexCount, graph.Matrix.Columns);
        }

        [TestMethod]
        public void SetGet()
        {
            Random random = new Random();
            int vertexCount = random.Next(10, 100);
            Graph<int> graph = new Graph<int>(vertexCount);
            int edgesCount = random.Next(1000, 10000);
            for (int i = 0; i < edgesCount; i++)
            {
                try
                {
                    int row = random.Next(0, vertexCount * 2);
                    int column = random.Next(0, vertexCount * 2);
                    int weight = random.Next(0, 100);
                    graph.Set(row, column, weight);
                    Assert.AreEqual(graph.Get(row, column), graph.Matrix[row][column]);
                }
                catch (Exception ex)
                {
                    Assert.AreEqual("Указаны некорректные идкесы строки/столбца!", ex.Message);
                }

            }
        }
    }
}
