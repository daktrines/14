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
using System.Windows.Threading;

namespace _13
{
    /// <summary>
    /// Логика взаимодействия для Password.xaml
    /// </summary>
    public partial class Password : Window
    {
        public Password()
        {
            InitializeComponent();
        }

        private void Windows_Activated(object sender, EventArgs e)
        {
            PasswordB.Focus();
        }

        private void Войти_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordB.Password == "123") Close();
            else
            {
                MessageBox.Show("Неверный пароль. Повторите попытку ввода", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                PasswordB.Focus();
            }
        }

        private void Отмена_Click(object sender, RoutedEventArgs e)
        {
            Owner.Close();
        }
    }
}
