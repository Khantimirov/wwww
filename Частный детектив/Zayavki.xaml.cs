using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
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


namespace Частный_детектив
{
    /// <summary>
    /// Логика взаимодействия для Zayavka.xaml
    /// </summary>
    public partial class Zayavki : Window
    {
        public Zayavki()
        {
            InitializeComponent();
            this.Left = 0;
            this.Top = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection conn = new MySqlConnection("server=127.0.0.1;port=3306;database=mydb;user id=root;password=1234;charset=utf8;Pooling=false;SslMode=None;");
            string name = "";
            int x = 0;
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                    if (textBoxName.Text != "" && textBoxNumber.Text != "" && textBoxTextUslugi.Text != "")
                    {
                        MySqlCommand cmd1 = new MySqlCommand("select count(Manager_Id) from Manager", conn);
                        if (cmd1.ExecuteScalar() != null)
                        {
                            x = Convert.ToInt32(cmd1.ExecuteScalar().ToString()) ;
                            Random rnd = new Random();
                            int id = rnd.Next(1, x + 1);
                            MySqlCommand cmd = new MySqlCommand("insert into zayavka (name, number, textzayavki, manager_id) values(@name, @number, @text, @id);", conn);
                            cmd.Parameters.AddWithValue("@name", textBoxName.Text);
                            cmd.Parameters.AddWithValue("@number", textBoxNumber.Text);
                            cmd.Parameters.AddWithValue("@text", textBoxTextUslugi.Text);
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Заявка отправлена, ожидайте звонка");
                            var form = new Vhod();
                            form.Show();
                            this.Hide();
                        }
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("заполните все поля");
                    }
                }

            }
            catch (MySqlException ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            var form = new Vhod();
            form.Show();
            this.Hide();
        }
    }
}
