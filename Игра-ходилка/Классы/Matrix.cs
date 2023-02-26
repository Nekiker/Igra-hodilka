using System;
using System.Collections.Generic;

namespace Игра_ходилка.Классы
{
    /// <summary>
    /// Матрица
    /// </summary>
    public class Matrix<ItemType>
    {

        #region Поля
        List<List<ItemType>> items;

        #endregion

        #region Свойства
        /// <summary>
        /// Элементы матрицы
        /// </summary>
        public List<List<ItemType>> Items { get => this.items; set => this.items = value; }
        /// <summary>
        /// Количество строк
        /// </summary>
        public int Rows
        {
            get => this.Items == null ? 0 : this.Items.Count;
            set
            {
                if (value < 0)
                {
                    throw new Exception("Количество строк не может быть меньше нуля");
                }

                int dif = value - this.Rows;
                if (dif > 0)
                {
                    this.AddRow(dif);
                }
                else
                {
                    this.RemoveRow(dif);
                }
            }
        }
        /// <summary>
        /// Количество столбцов - выбирается минимальное число столбов из всех строк
        /// </summary>
        public int Columns
        {
            get
            {
                if (this.items == null)
                {
                    return 0;
                }

                if (this.items.Count == 0)
                {
                    return 0;
                }

                int min = int.MaxValue;
                foreach (var row in this.Items)
                {
                    if (row != null && min > row.Count)
                    {
                        min = row.Count;
                    }
                }
                return min;
            }
            set
            {
                if (value < 0)
                {
                    throw new Exception("Количество столбцов не может быть меньше нуля");
                }

                int dif = value - this.Columns;
                if (dif > 0)
                {
                    this.AddColumn(dif);
                }
                else
                {
                    this.RemoveColumn(dif);
                }
            }
        }
        /// <summary>
        /// Количество нулей в матрице (для объектов - ссылок null)
        /// </summary>
        public int ZeroCount
        {
            get
            {
                int count = 0;
                for (int i = 0; i < this.Rows; i++)
                {
                    for (int j = 0; j < this.Columns; j++)
                    {
                        if (this.Items[i][j].GetType() == typeof(int))
                        {
                            if ((int)(object)this.Items[i][j] == 0)
                            {
                                count++;
                            }
                        }
                        if (this.Items[i][j].GetType() == typeof(double))
                        {
                            if ((double)(object)this.Items[i][j] == 0)
                            {
                                count++;
                            }
                        }
                        if (this.Items[i][j].GetType() == typeof(float))
                        {
                            if ((float)(object)this.Items[i][j] == 0)
                            {
                                count++;
                            }
                        }
                        else if (this.Items[i][j] == null)
                        {
                            count++;
                        }
                    }
                }

                return count;
            }
        }
        /// <summary>
        /// Содержит ли пустые строки
        /// </summary>
        public bool IsContainsEmptyRows
        {
            get
            {
                foreach (var item in this.Items)
                {
                    if (item == null)
                    {
                        return true;
                    }
                }

                return false;
            }
        }
        #endregion

        #region Методы

        #region Добавить
        /// <summary>
        /// Добавляет одну строку в конец матрицы
        /// </summary>
        public void AddRow()
        {
            var row = new List<ItemType>();
            for (int i = 0; i < this.Columns; i++)
            {
                row.Add((ItemType)Activator.CreateInstance(typeof(ItemType)));
            }

            this.AddRow(row);
        }
        /// <summary>
        /// Добавляет указанное количество строк в конец матрицы
        /// </summary>
        /// <param name="rowCount">количество вставляемых строк</param>
        public void AddRow(int rowCount)
        {
            for (int i = 0; i < rowCount; i++)
            {
                this.AddRow();
            }
        }
        /// <summary>
        /// Добавляет указанную строку в конец матрицы
        /// </summary>
        /// <param name="row">строка (количество элементов должно совпадать с количеством столбцов)</param>
        public void AddRow(List<ItemType> row)
        {
            if (row == null)
            {
                throw new Exception("Передана пустая строка");
            }

            if (row.Count != this.Columns)
            {
                throw new Exception(String.Format("Не удалось вставить строку, ожидаемая длина:{0} фактическая длина:{1}",
                    this.Columns, row.Count));
            }
            this.Items.Add(new List<ItemType>(row));
        }
        /// <summary>
        /// Добавляет один столбец в конец матрицы
        /// </summary>
        public void AddColumn()
        {
            var column = new List<ItemType>();
            for (int i = 0; i < this.Rows; i++)
            {
                column.Add((ItemType)Activator.CreateInstance(typeof(ItemType)));
            }
            this.AddColumn(column);
        }
        /// <summary>
        /// Добавляет указанное количество столбцов в конец матрицы
        /// </summary>
        /// <param name="columnCount">количество вставляемых столбцов</param>
        public void AddColumn(int columnCount)
        {
            for (int i = 0; i < columnCount; i++)
            {
                this.AddColumn();
            }
        }
        /// <summary>
        /// Добавляет указанный столбец в конец матрицы
        /// </summary>
        /// <param name="column">столбец (количество элементов должно совпадать с количеством строк)</param>
        public void AddColumn(List<ItemType> column)
        {
            if (column == null)
            {
                throw new Exception("Передан пустой столбец");
            }

            if (column.Count != this.Rows)
            {
                throw new Exception(String.Format("Не удалось вставить столбец, ожидаемая длина:{0} фактическая длина:{1}",
                    this.Rows, column.Count));
            }

            for (int i = 0; i < this.Items.Count; i++)
            {
                this[i].Add(column[i]);
            }
        }
        #endregion

        #region Удалить
        /// <summary>
        /// Удаляет строку в конце матрицы
        /// </summary>
        public void RemoveRow()
        {
            if (this.Rows > 0)
            {
                this.Items.RemoveAt(this.Rows - 1);
            }
        }
        /// <summary>
        /// Удаляет указанное количество строк в конце матрицы
        /// </summary>
        /// <param name="rowCount">количество удаляемых строк</param>
        public void RemoveRow(int rowCount)
        {
            for (int i = 0; i < rowCount; i++)
            {
                this.RemoveRow();
            }
        }
        /// <summary>
        /// Удаляет столбец в конце матрицы
        /// </summary>
        public void RemoveColumn()
        {
            if (this.Columns > 0)
            {
                foreach (var row in this.Items)
                {
                    row.RemoveAt(row.Count - 1);
                }
            }
        }
        /// <summary>
        /// Удаляет указанное количество столбцов в конце матрицы
        /// </summary>
        /// <param name="columnCount">количество удаляемых столбцов</param>
        public void RemoveColumn(int columnCount)
        {
            for (int i = 0; i < columnCount; i++)
            {
                this.RemoveColumn();
            }
        }
        #endregion

        /// <summary>
        /// Проверка индексов на корректность
        /// </summary>
        /// <param name="rowIndex">индекс строки</param>
        /// <param name="columnIndex">индекс столбца</param>
        /// <returns></returns>
        public bool IsIndexCorrect(int rowIndex, int columnIndex)
        {
            return rowIndex >= 0 && rowIndex < this.Rows && columnIndex >= 0 && columnIndex < this.Columns;
        }
        /// <summary>
        /// Очищает матрицу, делает ее размером 0х0
        /// </summary>
        public void Clear()
        {
            this.Rows = 0;
            this.Columns = 0;
        }
        public override string ToString()
        {
            string result = String.Empty;
            int maxLength = 0;
            for (int i = 0; i < this.Rows; i++)
            {
                for (int j = 0; j < this.Columns; j++)
                {
                    maxLength = Math.Max(maxLength, this.Items[i][j].ToString().Length);
                }
            }

            maxLength++;
            string format = "{0," + maxLength.ToString() + "}";
            for (int i = 0; i < this.Rows; i++)
            {
                for (int j = 0; j < this.Columns; j++)
                {
                    result += String.Format(format, this.Items[i][j].ToString());
                }
                result += '\n';
            }
            return result;
        }
        #endregion

        #region Конструкторы/Деструкторы
        public Matrix(int rows, int columns)
        {
            this.Items = new List<List<ItemType>>();
            this.Rows = rows;
            this.Columns = columns;
        }
        public Matrix() : this(0, 0)
        {
        }
        #endregion

        #region Операторы
        public List<ItemType> this[int index]
        {
            get
            {
                if (index < 0 || index > this.Rows)
                {
                    throw new Exception("Указан некорректный индекс строки");
                }

                return this.Items[index];
            }
            set
            {
                if (index < 0 || index > this.Rows)
                {
                    throw new Exception("Указан некорректный индекс строки");
                }

                this.Items[index] = new List<ItemType>(value);
            }
        }
        #endregion

        #region Обработчики событий

        #endregion

    }
}
