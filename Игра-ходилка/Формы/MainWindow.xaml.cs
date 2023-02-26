using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using Игра_ходилка.Классы;

namespace Игра_ходилка
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        #region Поля

        #endregion

        #region Свойства
        Game Game { set; get; }
        #endregion

        #region Методы
        public List<string> GetFilesPath(string filter = "Все файлы (*.*)|*.*")
        {
            List<string> filesList = new List<string>();
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = filter,
                Multiselect = true,
                RestoreDirectory = true
            };
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                for (int i = 0; i < openFileDialog.FileNames.Length; i++)
                {
                    filesList.Add(openFileDialog.FileNames[i]);
                }
            }

            return filesList;
        }
        public string GetSaveFilePath(string filter = "Все файлы (*.*)|*.*")
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog
            {
                Title = "Сохранения файла",
                CheckFileExists = false,
                CheckPathExists = true,
                DefaultExt = "txt",
                Filter = filter,
                FilterIndex = 2,
                RestoreDirectory = true
            };
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                return saveFileDialog1.FileName;
            }

            return String.Empty;
        }
        public string[] GetLoadFilePath(
            string filter = "Все файлы (*.*)|*.*",
            bool isMulti = false,
            int filterIndex = 1,
            string defaultExtension = "txt",
            bool checkFileExists = false,
            bool checkPathExists = true,
            string Title = "Сохранение файла"
    )
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = Title,
                CheckFileExists = checkFileExists,
                CheckPathExists = checkPathExists,
                DefaultExt = defaultExtension,
                Filter = filter,
                Multiselect = isMulti,
                FilterIndex = filterIndex,
                RestoreDirectory = true
            };
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (openFileDialog.Multiselect)
                {
                    return openFileDialog.FileNames;
                }
                else
                {
                    return new string[] { openFileDialog.FileName };
                }
            }
            return null;
        }
        /// <summary>
        /// Обновляет игровое поле и лог игры
        /// </summary>
        public void Update()
        {
            if (this.Game != null && this.Game.Log != null)
            {
                this.lvLog.DataContext = this.Game.Log.Table;
                if (this.lvLog.Items.Count > 0)
                {
                    this.lvLog.ScrollIntoView(this.lvLog.Items[this.lvLog.Items.Count - 1]);
                }
            }
            if (this.Game.Graph != null)
            {
                this.canvasGame.Children.Clear();
                this.Game.Draw(this.canvasGame);
                this.canvasGame.UpdateLayout();
                this.canvasGameScroll.UpdateLayout();
            }
        }
        public void ShowError(string message)
        {
            System.Windows.MessageBox.Show(message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public void ShowError(Exception ex)
        {
            System.Windows.MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        #endregion

        #region Конструкторы/Деструкторы
        public MainWindow()
        {
            this.InitializeComponent();
            this.Game = new Game();
            this.Game.Generate();
        }


        #endregion

        #region Операторы

        #endregion

        #region Обработчики событий
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Update();
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
            }
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string path = this.GetSaveFilePath("Текстовый файл | *.txt");
                File.WriteAllText(path, JsonConvert.SerializeObject(this.Game));
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
            }
        }
        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var fileNames = this.GetLoadFilePath("Текстовый файл | *.txt");
                if (fileNames != null && fileNames.Length > 0)
                {
                    string path = fileNames[0];
                    this.Game = JsonConvert.DeserializeObject<Game>(File.ReadAllText(path));
                    this.Update();
                }
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
            }
        }
        private void btnNewGame_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Game.Graph = new Graph<int>();
                this.Game.Graph.Generate(40, 150, 0, 0, 1, 1, GenerateType.Snake);
                this.Update();
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
            }
        }
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.Game != null)
                {
                    int roll = this.Game.Roll();
                    this.Game.Next(roll);
                    this.Update();
                }
            }
            catch (Exception ex)
            {
                this.ShowError(ex);
            }
        }
        #endregion


    }
}
