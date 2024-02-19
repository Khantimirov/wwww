using MySqlConnector;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Частный_детектив
{
    /// <summary>
    /// Логика взаимодействия для ClientSmses.xaml
    /// </summary>
    public partial class KlientChats : Window
    {
        int Id;
        MySqlConnection conn = new MySqlConnection("server=127.0.0.1;port=3306;database=mydb;user id=root;password=1234;charset=utf8;Pooling=false;SslMode=None;");
        Button ok;
        public KlientChats(int id)
        {
            InitializeComponent();
            Id = id;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                MySqlCommand cmd1 = new MySqlCommand("select * from zakaz where client_id = @client_id  ORDER BY zakaz_id DESC", conn);
                cmd1.Parameters.AddWithValue("@client_id", Id);
                MySqlDataReader reader = cmd1.ExecuteReader();
                while (reader.Read())
                {
                    if (Convert.ToString(reader["status"]) != "отменен")
                    {
                        int usluga_id = Convert.ToInt32(reader["usluga_id"]);
                        MySqlConnection conn2 = new MySqlConnection("server=127.0.0.1;port=3306;database=mydb;user id=root;password=1234;charset=utf8;Pooling=false;SslMode=None;");
                        if (conn2.State == ConnectionState.Closed)
                        {
                            conn2.Open();
                            MySqlCommand cmd2 = new MySqlCommand("select name from Uslugi where usluga_id = @usluga_id", conn2);
                            cmd2.Parameters.AddWithValue("@usluga_id", usluga_id);
                            string name_usluga = cmd2.ExecuteScalar().ToString();

                            ok = new Button();
                            ok.Style = (Style)Resources["People"];
                            ok.Content = name_usluga;
                            ok.Click += new RoutedEventHandler(ok_Click);
                            ok.Name = "But" + Convert.ToString(reader["zakaz_id"]);
                            stack.Children.Add(ok);
                        }
                    }
                }
                reader.Close();
            }
        }
        void ok_Click(object sender, RoutedEventArgs e)
        {
            string text = (sender as FrameworkElement).Name;
            text = text.Replace("But", "");
            int get = Convert.ToInt32(text);
            var form = new KlientSms(get, Id);
            form.Show();
            this.Hide();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            var form = new Klient(Id);
            form.Show();
            this.Hide();
        }
    }
}
