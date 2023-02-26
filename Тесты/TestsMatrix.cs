using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Игра_ходилка.Классы;

namespace Тесты
{
    [TestClass]
    public class TestsMatrix
    {
        [TestMethod]
        public void Create()
        {
            Random random = new Random();
            int rowsCount, columnsCount;
            Matrix<int> matrix;

            #region 0 строк
            columnsCount = random.Next(0, 100);
            matrix = new Matrix<int>(0, columnsCount);
            Assert.AreEqual(0, matrix.Rows);
            Assert.AreEqual(0, matrix.Columns);
            #endregion

            #region 0 столбцов
            rowsCount = random.Next(1, 100);
            matrix = new Matrix<int>(rowsCount, 0);
            Assert.AreEqual(rowsCount, matrix.Rows);
            Assert.AreEqual(0, matrix.Columns);
            #endregion

            #region Случайное количество строк и столбцов (от 1 до 100)
            int matrixCount = 100;
            for (int i = 0; i < matrixCount; i++)
            {
                rowsCount = random.Next(1, 100);
                columnsCount = random.Next(1, 100);
                matrix = new Matrix<int>(rowsCount, columnsCount);
                Assert.AreEqual(rowsCount, matrix.Rows);
                Assert.AreEqual(columnsCount, matrix.Columns);
            }
            #endregion

        }

        [TestMethod]
        public void AddRow()
        {
            Random random = new Random();
            int rowsCount, columnsCount, itemsCount, incrementRows;
            List<int> items;
            Matrix<int> matrix;
            rowsCount = random.Next(1, 100);
            columnsCount = random.Next(1, 100);
            matrix = new Matrix<int>(rowsCount, columnsCount);
            #region Вставка нескольких строк
            incrementRows = random.Next(1, 5);
            matrix.AddRow(incrementRows); // Тут же проверяется и AddRow() без параметров
            Assert.AreEqual(rowsCount + incrementRows, matrix.Rows);
            #endregion
            #region Вставка пустой строки
            try
            {
                matrix.AddRow(null);
                Assert.AreEqual(rowsCount, matrix.Rows);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Передана пустая строка", ex.Message);
            }
            #endregion
            #region Вставка несоразмерной строки
            itemsCount = matrix.Columns + random.Next(1, 100);
            try
            {
                items = new List<int>();
                for (int i = 0; i < itemsCount; i++)
                {
                    items.Add(random.Next(0, 100));
                }

                matrix.AddRow(items);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(String.Format("Не удалось вставить строку, ожидаемая длина:{0} фактическая длина:{1}",
                    matrix.Columns, itemsCount), ex.Message);
            }
            #endregion
            #region Вставка заготовленной строки
            items = new List<int>();
            for (int i = 0; i < matrix.Columns; i++)
            {
                items.Add(random.Next(0, 100));
            }

            matrix.AddRow(items);
            Assert.AreNotEqual(items, matrix[matrix.Rows - 1]);
            for (int i = 0; i < items.Count; i++)
            {
                Assert.AreEqual(items[i], matrix[matrix.Rows - 1][i]);
            }
            #endregion

        }

        [TestMethod]
        public void RemoveRow()
        {
            Random random = new Random();
            int rowsCount, columnsCount, decrementRows;
            Matrix<int> matrix;

            #region Удаление последней строки
            rowsCount = random.Next(1, 100);
            columnsCount = random.Next(1, 100);
            matrix = new Matrix<int>(rowsCount, columnsCount);

            matrix.RemoveRow();

            Assert.AreEqual(rowsCount - 1, matrix.Rows);
            #endregion
            #region Удаление строки у пустой матрицы
            matrix = new Matrix<int>(0, 0);

            matrix.RemoveRow();

            Assert.AreEqual(0, matrix.Rows);
            #endregion
            #region Удаление нескольких строк
            rowsCount = random.Next(10, 100);
            columnsCount = random.Next(10, 100);
            decrementRows = random.Next(2, 10);
            matrix = new Matrix<int>(rowsCount, columnsCount);

            matrix.RemoveRow(decrementRows);

            Assert.AreEqual(rowsCount - decrementRows, matrix.Rows);
            #endregion
            #region Удаление строк больше, чем имеется
            rowsCount = random.Next(5, 10);
            columnsCount = random.Next(5, 10);
            decrementRows = 12;
            matrix = new Matrix<int>(rowsCount, columnsCount);

            matrix.RemoveRow(decrementRows);

            Assert.AreEqual(matrix.Rows, 0);
            #endregion
        }

        [TestMethod]
        public void AddColumn()
        {
            Random random = new Random();
            int rowsCount, columnsCount, itemsCount, incrementColumns;
            List<int> items;
            Matrix<int> matrix;
            rowsCount = random.Next(1, 100);
            columnsCount = random.Next(1, 100);
            matrix = new Matrix<int>(rowsCount, columnsCount);
            #region Вставка нескольких столбцов
            incrementColumns = random.Next(1, 5);
            matrix.AddColumn(incrementColumns); // Тут же проверяется и AddColumn() без параметров
            Assert.AreEqual(columnsCount + incrementColumns, matrix.Columns);
            #endregion
            #region Вставка пустого столбца
            try
            {
                matrix.AddColumn(null);
                Assert.AreEqual(columnsCount, matrix.Columns);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Передан пустой столбец", ex.Message);
            }
            #endregion
            #region Вставка несоразмерного столбца
            itemsCount = matrix.Rows + random.Next(1, 100);
            try
            {
                items = new List<int>();
                for (int i = 0; i < itemsCount; i++)
                {
                    items.Add(random.Next(0, 100));
                }

                matrix.AddColumn(items);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(String.Format("Не удалось вставить столбец, ожидаемая длина:{0} фактическая длина:{1}",
                    matrix.Rows, itemsCount), ex.Message);
            }
            #endregion
            #region Вставка заготовленного столбца
            items = new List<int>();
            for (int i = 0; i < matrix.Rows; i++)
            {
                items.Add(random.Next(0, 100));
            }

            matrix.AddColumn(items);
            //Assert.AreNotEqual(items, matrix[matrix.Rows - 1]);
            for (int i = 0; i < items.Count; i++)
            {
                Assert.AreEqual(items[i], matrix[i][matrix.Columns - 1]);
            }
            #endregion
        }

        [TestMethod]
        public void RemoveColumn()
        {
            Random random = new Random();
            int rowsCount, columnsCount, decrementColumns;
            Matrix<int> matrix;

            #region Удаление последнего столбца
            rowsCount = random.Next(1, 100);
            columnsCount = random.Next(1, 100);
            matrix = new Matrix<int>(rowsCount, columnsCount);

            matrix.RemoveColumn();

            Assert.AreEqual(columnsCount - 1, matrix.Columns);
            #endregion
            #region Удаление столбца у пустой матрицы
            matrix = new Matrix<int>(0, 0);

            matrix.RemoveColumn();

            Assert.AreEqual(0, matrix.Columns);
            #endregion
            #region Удаление нескольких столбцов
            rowsCount = random.Next(10, 100);
            columnsCount = random.Next(10, 100);
            decrementColumns = random.Next(2, 10);
            matrix = new Matrix<int>(rowsCount, columnsCount);

            matrix.RemoveColumn(decrementColumns);

            Assert.AreEqual(columnsCount - decrementColumns, matrix.Columns);
            #endregion
            #region Удаление столбцов больше, чем имеется
            rowsCount = random.Next(5, 10);
            columnsCount = random.Next(5, 10);
            decrementColumns = 3;
            matrix = new Matrix<int>(4, 2);

            matrix.RemoveColumn(decrementColumns);

            Assert.AreEqual(matrix.Columns, 0);
            #endregion
        }
    }
}
