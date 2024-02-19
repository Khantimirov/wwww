using MySqlConnector;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Частный_детектив
{
    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        int Id;
        TextBlock label;
        Button ok;
        Border border;
        DockPanel panel;
        public Admin(int id)
        {
            InitializeComponent();
            Id = id;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            var form = new Vhod();
            form.Show();
            this.Hide();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MySqlConnection conn = new MySqlConnection("server=127.0.0.1;port=3306;database=mydb;user id=root;password=1234;charset=utf8;Pooling=false;SslMode=None;");
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                MySqlCommand cmd1 = new MySqlCommand("select * from uslugi ORDER BY usluga_id DESC", conn);
                MySqlDataReader reader = cmd1.ExecuteReader();
                while (reader.Read())
                {
                    border = new Border();
                    border.Style = (Style)Resources["bord"];
                    panel = new DockPanel();

                    label = new TextBlock();
                    label.Style = (Style)Resources["labs"];
                    label.Text = reader["name"].ToString(); ;
                    panel.Children.Add(label);

                    label = new TextBlock();
                    label.Style = (Style)Resources["labs"];
                    label.Text = reader["price"].ToString();
                    panel.Children.Add(label);

                    label = new TextBlock();
                    label.Style = (Style)Resources["labs"];
                    label.Text = reader["otdel"].ToString();
                    panel.Children.Add(label);

                    ok = new Button();
                    ok.Style = (Style)Resources["buts"];
                    ok.Content = "редактировать";
                    ok.Click += new RoutedEventHandler(ok_Click);
                    ok.Name = "But" + Convert.ToString(reader["usluga_id"]);
                    panel.Children.Add(ok);

                    ok = new Button();
                    ok.Style = (Style)Resources["buts"];
                    ok.Content = "удалить";
                    ok.Click += new RoutedEventHandler(ok2_Click);
                    ok.Name = "But" + Convert.ToString(reader["usluga_id"]);
                    panel.Children.Add(ok);

                    border.Child = panel;
                    stack.Children.Add(border);

                }
                reader.Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var form = new AdminWork(Id);
            form.Show();
            this.Hide();
        }

        void ok_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection conn = new MySqlConnection("server=127.0.0.1;port=3306;database=mydb;user id=root;password=1234;charset=utf8;Pooling=false;SslMode=None;");
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                string text = (sender as FrameworkElement).Name;
                text = text.Replace("But", "");
                int get = Convert.ToInt32(text);

                var form = new AdminUs(Id, 0, get);
                form.Show();
                this.Hide();
                
            }
        }
        void ok2_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection conn = new MySqlConnection("server=127.0.0.1;port=3306;database=mydb;user id=root;password=1234;charset=utf8;Pooling=false;SslMode=None;");
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                string text = (sender as FrameworkElement).Name;
                text = text.Replace("But", "");
                int get = Convert.ToInt32(text);

                MySqlCommand cmd = new MySqlCommand("DELETE FROM uslugi where usluga_id = @usluga_id;", conn);
                cmd.Parameters.AddWithValue("@usluga_id", get);
                cmd.ExecuteNonQuery();
                var form = new Admin(Id);
                form.Show();
                this.Hide();
                MessageBox.Show("Успешно удалено");
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var form = new AdminUs(Id, 1, 1);
            form.Show();
            this.Hide();
        }
    }
}
