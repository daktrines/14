using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using LibMas;
using Microsoft.Extensions.Options;
using Масивы;
using static _13.Settings;
using System.IO;


namespace _13
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        int[,] matr;
        DispatcherTimer _timer;// Описываем таймер

        private void Information_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Калион Екатерина " +
                "\n13 пр" +
                "\nДана матрица размера M * N. " +
                "\nНайти количество ее столбцов, элементы которых " +
                "\nупорядочены по убыванию", "Информация", MessageBoxButton.OK, MessageBoxImage.Question);
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Windows_Loaded(object sender, RoutedEventArgs e)
        {
            //Добавляем таймер
            _timer = new DispatcherTimer();
            _timer.Tick += Timer_Tick;
            _timer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            _timer.IsEnabled = true;

            Password pas = new Password();
            pas.Owner = this;//Получение ссылки на родителя
            pas.ShowDialog();

            try
            {
                //Открываем файл, который сохранили
                StreamReader Open = new StreamReader("config.ini");
                Data1._strok   = Convert.ToInt32(Open.ReadLine());
                Data1._stolbcov = Convert.ToInt32(Open.ReadLine());
                Open.Close();

                KolStrok.Text = Data1._strok.ToString();
                KolStolbcov.Text = Data1._stolbcov.ToString();
            }
            catch
            {
                MessageBox.Show("Автозаполнение таблицы не было выполнено. Необходимо установить значение в настройках", "Ошибка", MessageBoxButton.OK,
                  MessageBoxImage.Error);
            }
        }

        //Создаем вручную событие таймера
        private void Timer_Tick(object sender, EventArgs e)
        {
            DateTime d = DateTime.Now;//Создание обьекта
            time.Text = d.ToString("HH:mm");//Время
            date.Text = d.ToString("dd.MM.yyyy");//Дата
        }

        //Редактирование ячеек
        private void MatrData_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            //Очищаем textbox с результатом 
            Rez1.Clear();

            //Определяем номер столбца
            int columnIndex = e.Column.DisplayIndex;
            //Определяем номер строки
            int rowIndex = e.Row.GetIndex();

            //Заносим  отредоктированое значение в соответствующую ячейку матрицы
            if (Int32.TryParse(((TextBox)e.EditingElement).Text, out matr[rowIndex, columnIndex]))
            { }
            else MessageBox.Show("Неверные данные!", "Ошибка", MessageBoxButton.OK,
                    MessageBoxImage.Error);
        }

        //Заполнение матрицы
        private void Fill_Click(object sender, RoutedEventArgs e)
        {
            //Проверка поля на корректность введенных данных
            try
            {
                int row = Convert.ToInt32(KolStrok.Text);
                int column = Convert.ToInt32(KolStolbcov.Text);
                //Определяем размер матрицы
                MatrixSize.Text = $"Matrix: {KolStrok.Text}" + "*" + $"{KolStolbcov.Text}";
               
                Class1.Fill(row, column, out matr);

                //Выводим матрицу на форму
                MatrData.ItemsSource = VisualArray.ToDataTable(matr).DefaultView;

                //очищаем результат
                Rez1.Clear();
            }
            catch
            {
                MessageBox.Show("Неверные данные!", "Ошибка", MessageBoxButton.OK,
                  MessageBoxImage.Error);
                KolStrok.Focus();
            }
        }

        //Расчет задания для матрицы
        private void Find_Click(object sender, RoutedEventArgs e)
        {
            Rez1.Clear();

            if (matr == null || matr.Length == 0)
            {
                MessageBox.Show("Вы не создали матрицу, укажите размеры матрицы и нажмите кнопку Заполнить", "Ошибка", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            else
            {
                try
                {
                    int row = Convert.ToInt32(KolStrok.Text);
                    int column = Convert.ToInt32(KolStolbcov.Text);
                    int kol = Rez.Рассчитать(row, column, matr);
                    Rez1.Text = Convert.ToString(kol);
                }
                catch
                {
                    MessageBox.Show("Неверные данные!", "Ошибка", MessageBoxButton.OK,
                      MessageBoxImage.Error);
                    KolStrok.Focus();
                }
            }
        }
        

        //Очищение матрицы
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            //Очищаем остальные текстбоксы
            KolStrok.Focus();
            KolStolbcov.Clear();
            KolStrok.Clear();
            Rez1.Clear();

            if (matr != null && matr.Length != 0)
            {
                MatrData.ItemsSource = null;
            }
            else MessageBox.Show("Вы не создали матрицу, укажите размеры матрицы и нажмите кнопку \"Заполнить" , "Ошибка",  MessageBoxButton.OK,
                    MessageBoxImage.Error);
        }

        //Сохранение матрицы
        private void SaveMatr_Click(object sender, RoutedEventArgs e)
        {
            Class1.Savematr(matr);
        }

        //Открытие матрицы
        private void OpenMatr_Click(object sender, RoutedEventArgs e)
        {
            Class1.Openmatr(out matr);
            for (int i = 0; i < matr.GetLength(0); i++)
            {
                for (int j = 0; j < matr.GetLength(1); j++)
                {
                    //Выводим матрицу на форму
                    MatrData.ItemsSource = VisualArray.ToDataTable(matr).DefaultView;
                }
            }
        }

        //Когда изменяем текстбокс, очищает остальные текстбоксы
        private void KolOfRowsAndColumns_TextChanged(object sender, TextChangedEventArgs e)
        {
            Rez1.Clear();
        }
       
        //Определяем номер ячейки в матрице
        private void MatrData_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //Определяем номер столбца
            int indexColumn = MatrData.CurrentCell.Column.DisplayIndex;
            //Определяем номер строки
            int indexRow = MatrData.SelectedIndex;

            CellNumber.Text = $"Строка {indexRow + 1}" + " / " + $"Столбец {indexColumn + 1}";
        }

        //Закрытие программы
        private void Windows_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы желаете выйти из программы?", "Выход из программы", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No) e.Cancel = true;//Если нет, то мы не выходим из программы
        }

      //Кнопка настройки 
        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            //Открываем окно настройки
            Settings sett = new Settings();
            sett.Owner = this; //Получение ссылки на родителя
            sett.ShowDialog();//Открываем диалоговое окно
            //берем полученные значения и заносим их  в соотв. элементы
            KolStrok.Text = Data1._strok.ToString();
            KolStolbcov.Text = Data1._stolbcov.ToString();
            
            //Сохраняем файл
            StreamWriter Save = new StreamWriter("config.ini");
            Save.WriteLine(Data1._strok);
            Save.WriteLine(Data1._stolbcov);
            Save.Close();

            //Используем событие заполнения матрицы
            Fill_Click(sender, e);
        }
    }
}


