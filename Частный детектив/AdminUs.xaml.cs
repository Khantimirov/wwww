using MySqlConnector;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Частный_детектив
{
    /// <summary>
    /// Логика взаимодействия для AdminUs.xaml
    /// </summary>
    public partial class AdminUs : Window
    {
        int Id;
        int UslugaId;
        int Del;
        public AdminUs(int id, int del, int uslugaId)
        {
            InitializeComponent();
            Id = id;
            Del = del;
            UslugaId = uslugaId;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            var form = new Admin(Id);
            form.Show();
            this.Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection conn = new MySqlConnection("server=127.0.0.1;port=3306;database=mydb;user id=root;password=1234;charset=utf8;Pooling=false;SslMode=None;");
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                if (Del == 1)
                {
                    MySqlCommand cmd = new MySqlCommand("insert into uslugi (name, price, otdel) values(@name, @price, @otdel);", conn);
                    cmd.Parameters.AddWithValue("@name", textBoxName.Text);
                    cmd.Parameters.AddWithValue("@price", textBoxPrice.Text);
                    cmd.Parameters.AddWithValue("@otdel", textBoxOtdel.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Услуга добавлена");
                }
                if(Del == 0)
                {
                    MySqlCommand cmd = new MySqlCommand("UPDATE uslugi SET name = @name, price = @price, otdel = @otdel where usluga_id = @usluga_id;", conn);
                    cmd.Parameters.AddWithValue("@name", textBoxName.Text);
                    cmd.Parameters.AddWithValue("@price", textBoxPrice.Text);
                    cmd.Parameters.AddWithValue("@otdel", textBoxOtdel.Text);
                    cmd.Parameters.AddWithValue("@usluga_id", UslugaId);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Успешно изменено");
                }
                var form = new Admin(Id);
                form.Show();
                this.Hide();

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MySqlConnection conn = new MySqlConnection("server=127.0.0.1;port=3306;database=mydb;user id=root;password=1234;charset=utf8;Pooling=false;SslMode=None;");
            if (Del == 0)
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                    MySqlCommand cmd1 = new MySqlCommand("select * from uslugi where usluga_id = @usluga_id", conn);
                    cmd1.Parameters.AddWithValue("@usluga_id", UslugaId);
                    MySqlDataReader reader = cmd1.ExecuteReader();
                    while (reader.Read())
                    {
                        textBoxName.Text = reader["name"].ToString();
                        textBoxPrice.Text = reader["price"].ToString();
                        textBoxOtdel.Text = reader["otdel"].ToString();
                    }
                    reader.Close();
                }
            }
        }
        public bool uslugiload()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("server=127.0.0.1;port=3306;database=mydb;user id=root;password=1234;charset=utf8;Pooling=false;SslMode=None;");
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                    MySqlCommand cmd1 = new MySqlCommand("select * from uslugi where usluga_id = @usluga_id", conn);
                    cmd1.Parameters.AddWithValue("@usluga_id", UslugaId);
                    MySqlDataReader reader = cmd1.ExecuteReader();
                    while (reader.Read())
                    {
                        textBoxName.Text = reader["name"].ToString();
                        textBoxPrice.Text = reader["price"].ToString();
                        textBoxOtdel.Text = reader["otdel"].ToString();
                    }
                    reader.Close();
                }
                return true;
            }
            catch
            {
                return false;
            }
		}
	}
}
