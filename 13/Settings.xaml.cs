using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.IO;

namespace _13
{
    /// <summary>
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
        }

        public static class data1
        {
            public static int Strok;
            public static int Stolbcov;
        }

      
        //Кнопка сохранить
        private void Сохранить_Click(object sender, RoutedEventArgs e)
        {
            int value;
            if (Int32.TryParse(strok.Text, out value))
                data1.Strok = value;
            else
            {
                MessageBox.Show("Ошибка в количестве строк");
                strok.Focus();
                return;
            }
            if (Int32.TryParse(stolbcov.Text, out value))
                data1.Stolbcov = value;
            else
            {
                MessageBox.Show("Ошибка в количестве столбцов");
                stolbcov.Focus();
                return;
            }
            Close();
        }
        private void Setting_Loaded(object sender, RoutedEventArgs e)
        {
            strok.Text = data1.Strok.ToString();
            stolbcov.Text = data1.Stolbcov.ToString();
        }

    }
}
