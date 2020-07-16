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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.IO;

namespace WpfApp4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string host = "mysql11.hostland.ru";
        static string database = "host1323541_itstep22";
        static string port = "3306";
        static string username = "host1323541_itstep";
        static string pass = "269f43dc";
        static string ConnString = "Server=" + host + ";Database=" + database + ";port=" + port + ";User Id=" + username + ";password=" + pass;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var name = Name.Text;
            var phone = Phone.Text;

            if ((Name.Text == "")||(Phone.Text == ""))
            {
                MessageBox.Show("Одно или оба поля не заполнены.", "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MySqlConnection db = new MySqlConnection(ConnString);
                db.Open();
                string sql = $"INSERT INTO Phones (name, phone) VALUES ('{name}','{phone}');";
                MySqlCommand command = new MySqlCommand { Connection = db, CommandText = sql };
                MySqlDataReader result = command.ExecuteReader();
                MessageBox.Show("Абонент успешно добавлен", "УСПЕХ", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            //string[] line_inFile;
            //string msg = "";
            //MySqlConnection db = new MySqlConnection(ConnString);
            //db.Open();
            //string sql = $"SELECT name, phone FROM Phones";
            //MySqlCommand command = new MySqlCommand { Connection = db, CommandText = sql };
            //MySqlDataReader result = command.ExecuteReader();

            //using StreamWriter file = new StreamWriter("reg.txt", true);
            //file.WriteLine(result);

            ///////??????? Эта часть задания у меня не получилась???????/////////
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            string[] line_inFile;

            using (StreamReader sr = new StreamReader("reg.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    line_inFile = line.Split(new char[] { ' ' });
                    string name_inFile = line_inFile[0];
                    string phone_inFile = line_inFile[1];
                    MySqlConnection db = new MySqlConnection(ConnString);
                    db.Open();
                    string sql = $"INSERT INTO Phones (name, phone) VALUES ('{name_inFile}','{phone_inFile}');";
                    MySqlCommand command = new MySqlCommand { Connection = db, CommandText = sql };
                    MySqlDataReader result = command.ExecuteReader();
                    MessageBox.Show("Все абоненты из файла были добавлены в телефонную книгу", "УСПЕХ", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}
