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
        public int Row1 { get; set; }
        public int Column1 { get; set; }

        private void Сохранить_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter writer = new StreamWriter("config.ini");
            writer.WriteLine(Row1);
            writer.WriteLine(Column1);
            writer.Close();
        }

        private void Вых_Click(object sender, RoutedEventArgs e)
        {
            Owner.Close();
        }

        private void Setting_Activated(object sender, EventArgs e)
        {
            strok.Focus();
          
        }
        
        private void Ввод_Click(object sender, RoutedEventArgs e)
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
    }
}
