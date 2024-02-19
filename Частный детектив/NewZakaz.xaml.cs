using MySqlConnector;
using System;
using System.Data;
using System.Windows;

namespace Частный_детектив
{
    /// <summary>
    /// Логика взаимодействия для NewZakaz.xaml
    /// </summary>
    public partial class NewZakaz : Window
    {
        int Id;
        MySqlConnection conn = new MySqlConnection("server=127.0.0.1;port=3306;database=mydb;user id=root;password=1234;charset=utf8;Pooling=false;SslMode=None;");
        public NewZakaz(int id)
        {
            InitializeComponent();
            Id = id;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxTextUslugi.Text != "" && listBox1.SelectedItem.ToString() != "")
            {
                string usluga = listBox1.SelectedItem.ToString();
                string[] words = usluga.Split(new char[] { '-' });
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                    MySqlCommand cmd1 = new MySqlCommand("select Usluga_id from Uslugi where name = @name", conn);
                    cmd1.Parameters.AddWithValue("@name", words[0]);
                    int usluga_id = Convert.ToInt32(cmd1.ExecuteScalar().ToString());

                    MySqlCommand cmd2 = new MySqlCommand("select otdel from Uslugi where name = @name", conn);
                    cmd2.Parameters.AddWithValue("@name", words[0]);
                    string otdel = cmd2.ExecuteScalar().ToString();

                    string x = "";
                    MySqlCommand cmd3 = new MySqlCommand("select * from worker where otdel = @otdel", conn);
                    cmd3.Parameters.AddWithValue("@otdel", otdel);
                    MySqlDataReader reader = cmd3.ExecuteReader();
                    while (reader.Read())
                    {
                        x = x + " " + Convert.ToString(reader["worker_id"]);
                    }
                    reader.Close();
                    x = x.Substring(1);
                    string[] ids = x.Split(new char[] { ' ' });
                    int worker_id = Convert.ToInt32(ids[new Random().Next(0, ids.Length)]);

                    MySqlCommand cmd = new MySqlCommand("insert into zakaz(wishes, client_id, worker_id, usluga_id, status) values (@wishes, @client_id, @worker_id, @usluga_id, @status);", conn);
                    cmd.Parameters.AddWithValue("@wishes", textBoxTextUslugi.Text);
                    cmd.Parameters.AddWithValue("@client_id", Id);
                    cmd.Parameters.AddWithValue("@worker_id", worker_id);
                    cmd.Parameters.AddWithValue("@usluga_id", usluga_id);
                    cmd.Parameters.AddWithValue("@status", "создан");
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Заказ создан! Теперь вы можете вести диалог с исполнителем.");
                    var form = new Klient(Id);
                    form.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("заполните все поля");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MySqlCommand cmd1 = new MySqlCommand("select * from uslugi", conn);
            MySqlDataReader reader;
            try
            {
                cmd1.Connection.Open();
                reader = cmd1.ExecuteReader();
                while (reader.Read())
                {
                    string x = Convert.ToString(reader["name"]) + "-" + Convert.ToString(reader["price"]);
                    listBox1.Items.Add(x);
                }
                reader.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: \r\n{0}", ex.ToString());
            }
            finally
            {
                cmd1.Connection.Close();
            }
            
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            var form = new Klient(Id);
            form.Show();
            this.Hide();
        }
    }
}
