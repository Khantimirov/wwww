using MySqlConnector;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Частный_детектив
{
    /// <summary>
    /// Логика взаимодействия для DetectivSms.xaml
    /// </summary>
    public partial class WorkerSms : Window
    {
        int ZakazId;
        int WorkerId;
        TextBox body;

        public WorkerSms(int zakazid, int workerId)
        {
            InitializeComponent();
            ZakazId = zakazid;
            WorkerId = workerId;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            var form = new WorkerLK(WorkerId);
            form.Show();
            this.Hide();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MySqlConnection conn = new MySqlConnection("server=127.0.0.1;port=3306;database=mydb;user id=root;password=1234;charset=utf8;Pooling=false;SslMode=None;");
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select client_id from zakaz where zakaz_id = @zakaz_id LIMIT 1", conn);
                cmd.Parameters.AddWithValue("@zakaz_id", ZakazId);
                int clientid = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                MySqlCommand cmd2 = new MySqlCommand("select name from client where client_id = @client_id", conn);
                cmd2.Parameters.AddWithValue("@client_id", clientid);
                label.Content = cmd2.ExecuteScalar().ToString();

                MySqlCommand cmd1 = new MySqlCommand("select * from sms where zakaz_id = @zakaz_id", conn);
                cmd1.Parameters.AddWithValue("@zakaz_id", ZakazId);
                MySqlDataReader reader = cmd1.ExecuteReader();
                while (reader.Read())
                {
                    body = new TextBox();
                    body.Style = (Style)Resources["Text"];
                    body.Text = Convert.ToString(reader["sender"]) + "    |    " + Convert.ToString(reader["text"]);
                    stack.Children.Add(body);
                    scroll.ScrollToEnd();
                }
                reader.Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (textBoxMessage.Text != "")
                {
                    if (textBoxMessage.Text.Length < 150)
                    {
                        MySqlConnection conn = new MySqlConnection("server=127.0.0.1;port=3306;database=mydb;user id=root;password=1234;charset=utf8;Pooling=false;SslMode=None;");
                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();

                            MySqlCommand cmd2 = new MySqlCommand("select name from worker where worker_id = @worker_id", conn);
                            cmd2.Parameters.AddWithValue("@worker_id", WorkerId);
                            string nameworker = cmd2.ExecuteScalar().ToString();

                            MySqlCommand cmd = new MySqlCommand("insert into sms (text, sender, zakaz_id) values (@text, @sender, @zakaz_id);", conn);
                            cmd.Parameters.AddWithValue("@text", textBoxMessage.Text);
                            cmd.Parameters.AddWithValue("@sender", nameworker);
                            cmd.Parameters.AddWithValue("@zakaz_id", ZakazId);
                            cmd.ExecuteNonQuery();

                            body = new TextBox();
                            body.Style = (Style)Resources["Text"];
                            body.Text = nameworker + "    |    " + textBoxMessage.Text;
                            stack.Children.Add(body);
                            scroll.ScrollToEnd();
                            textBoxMessage.Text = "";
                        }
                    }
                    else { MessageBox.Show("Сообщение длинное, ограничение - 150 символов"); }
                }
                else { MessageBox.Show("Сообщение пустое!"); }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var form = new WorkerChats(WorkerId);
            form.Show();
            this.Hide();
        }
    }
}
