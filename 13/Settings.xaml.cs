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
        public Settings(int row = 0, int column = 0)
        {
            InitializeComponent();
            Row1 = row;
            Column1 = column;
            strok.Text = row.ToString();
            stolbcov.Text = column.ToString();
        }

        public static class data1
        {
            public static int Strok;
            public static int Stolbcov;
          
        }
        public int Row1 { get; set; }
        public int Column1 { get; set; }

        private void Вых_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Setting_Activated(object sender, EventArgs e)
        {
            strok.Focus();
        }
        
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
            StreamWriter writer = new StreamWriter("config.ini");
            Row1 = Convert.ToInt32(strok.Text);
            Column1 = Convert.ToInt32(stolbcov.Text);
            writer.Close();
        }
    }
}
