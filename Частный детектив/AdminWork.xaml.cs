using MySqlConnector;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Частный_детектив
{
    /// <summary>
    /// Логика взаимодействия для AdminDetec.xaml
    /// </summary>
    public partial class AdminWork : Window
    {
        int Id;
        TextBlock label;
        Button ok;
        Border border;
        DockPanel panel;
        public AdminWork(int id)
        {
            InitializeComponent();
            Id = id;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            var form = new Admin(Id);
            form.Show();
            this.Hide();
        }


        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var form = new AdminWorker(Id, 1, 1);
            form.Show();
            this.Hide();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MySqlConnection conn = new MySqlConnection("server=127.0.0.1;port=3306;database=mydb;user id=root;password=1234;charset=utf8;Pooling=false;SslMode=None;");
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                MySqlCommand cmd1 = new MySqlCommand("select * from worker ORDER BY worker_id DESC", conn);
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
                    label.Text = reader["number"].ToString();
                    panel.Children.Add(label);

                    label = new TextBlock();
                    label.Style = (Style)Resources["labs"];
                    label.Text = reader["mail"].ToString();
                    panel.Children.Add(label);

                    label = new TextBlock();
                    label.Style = (Style)Resources["labs"];
                    label.Text = reader["login"].ToString(); ;
                    panel.Children.Add(label);

                    label = new TextBlock();
                    label.Style = (Style)Resources["labs"];
                    label.Text = reader["password"].ToString();
                    panel.Children.Add(label);

                    label = new TextBlock();
                    label.Style = (Style)Resources["labs"];
                    label.Text = reader["otdel"].ToString();
                    panel.Children.Add(label);

                    ok = new Button();
                    ok.Style = (Style)Resources["buts"];
                    ok.Content = "редактировать";
                    ok.Click += new RoutedEventHandler(ok_Click);
                    ok.Name = "But" + Convert.ToString(reader["worker_id"]);
                    panel.Children.Add(ok);

                    ok = new Button();
                    ok.Style = (Style)Resources["buts"];
                    ok.Content = "удалить";
                    ok.Click += new RoutedEventHandler(ok2_Click);
                    ok.Name = "But" + Convert.ToString(reader["worker_id"]);
                    panel.Children.Add(ok);

                    border.Child = panel;
                    stack.Children.Add(border);

                }
                reader.Close();
            }
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
                MessageBox.Show(get.ToString());
                var form = new AdminWorker(Id, 0, get);
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

                MySqlCommand cmd = new MySqlCommand("DELETE FROM worker where worker_id = @worker_id;", conn);
                cmd.Parameters.AddWithValue("@worker_id", get);
                cmd.ExecuteNonQuery();
                var form = new AdminWork(Id);
                form.Show();
                this.Hide();
                MessageBox.Show("Успешно удалено");
            }
        }
    }
}
