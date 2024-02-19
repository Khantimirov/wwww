using MySqlConnector;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Частный_детектив
{
    /// <summary>
    /// Логика взаимодействия для Detectiv.xaml
    /// </summary>
    public partial class WorkerLK : Window
    {
        int Id;
        TextBlock label;
        Button ok;
        Border border;
        DockPanel panel;
        public WorkerLK(int id)
        {
            InitializeComponent();
            this.Left = 0;
            this.Top = 0;
            Id = id;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Vhod form = new Vhod();
            form.Show();
            this.Hide();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var form = new WorkerChats(Id);
            form.Show();
            this.Hide();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MySqlConnection conn = new MySqlConnection("server=127.0.0.1;port=3306;database=mydb;user id=root;password=1234;charset=utf8;Pooling=false;SslMode=None;");
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                MySqlCommand cmd1 = new MySqlCommand("select * from zakaz where worker_id = @worker_id ORDER BY zakaz_id DESC", conn);
                cmd1.Parameters.AddWithValue("@worker_id", Id);
                MySqlDataReader reader = cmd1.ExecuteReader();
                while (reader.Read())
                {
                    int usluga_id = Convert.ToInt32(reader["usluga_id"]);
                    MySqlConnection conn2 = new MySqlConnection("server=127.0.0.1;port=3306;database=mydb;user id=root;password=1234;charset=utf8;Pooling=false;SslMode=None;");
                    if (conn2.State == ConnectionState.Closed)
                    {
                        conn2.Open();
                        MySqlCommand cmd2 = new MySqlCommand("select name from Uslugi where usluga_id = @usluga_id", conn2);
                        cmd2.Parameters.AddWithValue("@usluga_id", usluga_id);
                        string name_usluga = cmd2.ExecuteScalar().ToString();

                        border = new Border();
                        border.Style = (Style)Resources["bord"];
                        panel = new DockPanel();

                        label = new TextBlock();
                        label.Style = (Style)Resources["labs"];
                        label.Text = name_usluga;
                        panel.Children.Add(label);

                        label = new TextBlock();
                        label.Style = (Style)Resources["labs"];
                        label.Text = Convert.ToString(reader["wishes"]);
                        panel.Children.Add(label);

                        label = new TextBlock();
                        label.Style = (Style)Resources["labs"];
                        if (Convert.ToString(reader["status"]) == "завершено") { label.Width = 270; }
                        label.Text = Convert.ToString(reader["status"]);
                        panel.Children.Add(label);

                        if (Convert.ToString(reader["status"]) == "в работе")
                        {
                            ok = new Button();
                            ok.Style = (Style)Resources["buts"];
                            ok.Content = "завершено";
                            ok.Click += new RoutedEventHandler(ok_Click);
                            ok.Name = "But" + Convert.ToString(reader["zakaz_id"]);
                            panel.Children.Add(ok);
                        }
                        if (Convert.ToString(reader["status"]) == "создан")
                        {
                            ok = new Button();
                            ok.Style = (Style)Resources["buts"];
                            ok.Content = "начать заказ";
                            ok.Click += new RoutedEventHandler(ok2_Click);
                            ok.Name = "But" + Convert.ToString(reader["zakaz_id"]);
                            panel.Children.Add(ok);
                        }


                        border.Child = panel;
                        stack.Children.Add(border);
                    }
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

                MySqlCommand cmd = new MySqlCommand("UPDATE zakaz SET status = @status where zakaz_id = @zakaz_id;", conn);
                cmd.Parameters.AddWithValue("@status", "завершено");
                cmd.Parameters.AddWithValue("@zakaz_id", get);
                cmd.ExecuteNonQuery();
                var form = new WorkerLK(Id);
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

                MySqlCommand cmd = new MySqlCommand("UPDATE zakaz SET status = @status where zakaz_id = @zakaz_id;", conn);
                cmd.Parameters.AddWithValue("@status", "в работе");
                cmd.Parameters.AddWithValue("@zakaz_id", get);
                cmd.ExecuteNonQuery();
                var form = new WorkerLK(Id);
                form.Show();
                this.Hide();
            }
        }
    }
}
