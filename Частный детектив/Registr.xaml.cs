using MySqlConnector;
using System;
using System.Data;
using System.Windows;

namespace Частный_детектив
{
    /// <summary>
    /// Логика взаимодействия для Registr.xaml
    /// </summary>
    public partial class Registr : Window
    {
        
        public Registr()
        {
            InitializeComponent();
            this.Left = 0;
            this.Top = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection conn = new MySqlConnection("server=127.0.0.1;port=3306;database=mydb;user id=root;password=1234;charset=utf8;Pooling=false;SslMode=None;");
            string name = "";
            try
            {

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                    if (textBoxName.Text != "" && textBoxNumber.Text != "" && textBoxMail.Text != "" && textBoxLogin.Text != "" && textBoxPassword.Text != "")
                    {
                        MySqlCommand cmd1 = new MySqlCommand("select Client_Id from Client where login = @login or mail = @mail", conn);
                        cmd1.Parameters.AddWithValue("@login", textBoxLogin.Text);
                        cmd1.Parameters.AddWithValue("@mail", textBoxMail.Text);
                        if (cmd1.ExecuteScalar() != null) { name = cmd1.ExecuteScalar().ToString(); }

                        MySqlCommand cmd2 = new MySqlCommand("select Worker_Id from Worker where login = @login or mail = @mail", conn);
                        cmd2.Parameters.AddWithValue("@login", textBoxLogin.Text);
                        cmd2.Parameters.AddWithValue("@mail", textBoxMail.Text);
                        if (cmd2.ExecuteScalar() != null) { name = cmd2.ExecuteScalar().ToString(); }

                        MySqlCommand cmd3 = new MySqlCommand("select Adminis_Id from Adminis where login = @login", conn);
                        cmd3.Parameters.AddWithValue("@login", textBoxLogin.Text);
						if (cmd3.ExecuteScalar() != null) { name = cmd3.ExecuteScalar().ToString(); }

						MySqlCommand cmd4 = new MySqlCommand("select Manager_Id from Manager where login = @login or mail = @mail", conn);
						cmd4.Parameters.AddWithValue("@login", textBoxLogin.Text);
						cmd4.Parameters.AddWithValue("@mail", textBoxMail.Text);
						if (cmd4.ExecuteScalar() != null) { name = cmd4.ExecuteScalar().ToString(); }

						if (name == "")
                        {
                            MySqlCommand cmd = new MySqlCommand("insert into client (name, number, mail, login, password) values(@name, @number, @mail, @login, @password);", conn);
                            cmd.Parameters.AddWithValue("@name", textBoxName.Text);
                            cmd.Parameters.AddWithValue("@number", textBoxNumber.Text);
                            cmd.Parameters.AddWithValue("@mail", textBoxMail.Text);
                            cmd.Parameters.AddWithValue("@login", textBoxLogin.Text);
                            cmd.Parameters.AddWithValue("@password", textBoxPassword.Text);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Успешная регистрация!");
                            var form = new Vhod();
                            form.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Пользователь с таким логином уже существует");
                        }
                    }
                    else { MessageBox.Show("Заполните все поля!"); }
                }


            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
		public bool Reg(string Name, string Number, string Mail, string Login, string Password)
		{
			using (MySqlConnection conn = new MySqlConnection("server=127.0.0.1;port=3306;database=mydb;user id=root;password=1234;charset=utf8;Pooling=false;SslMode=None;"))
			{
				try
				{
					conn.Open();

					MySqlCommand cmd1 = new MySqlCommand("SELECT Client_Id FROM Client WHERE login = @login OR mail = @mail", conn);
					cmd1.Parameters.AddWithValue("@login", Login);
					cmd1.Parameters.AddWithValue("@mail", Mail);
					if (cmd1.ExecuteScalar() != null)
					{
						return false;
					}

					MySqlCommand cmd2 = new MySqlCommand("SELECT Worker_Id FROM Worker WHERE login = @login OR mail = @mail", conn);
					cmd2.Parameters.AddWithValue("@login", Login);
					cmd2.Parameters.AddWithValue("@mail", Mail);
					if (cmd2.ExecuteScalar() != null)
					{
						return false;
					}

					MySqlCommand cmd3 = new MySqlCommand("SELECT Adminis_Id FROM Adminis WHERE login = @login", conn);
					cmd3.Parameters.AddWithValue("@login", Login);
					if (cmd3.ExecuteScalar() != null)
					{
						return false;
					}

					MySqlCommand cmd4 = new MySqlCommand("SELECT Manager_Id FROM Manager WHERE login = @login OR mail = @mail", conn);
					cmd4.Parameters.AddWithValue("@login", Login);
					cmd4.Parameters.AddWithValue("@mail", Mail);
					if (cmd4.ExecuteScalar() != null)
					{
						return false;
					}

					MySqlCommand cmd = new MySqlCommand("INSERT INTO client (name, number, mail, login, password) VALUES (@name, @number, @mail, @login, @password);", conn);
					cmd.Parameters.AddWithValue("@name", Name);
					cmd.Parameters.AddWithValue("@number", Number);
					cmd.Parameters.AddWithValue("@mail", Mail);
					cmd.Parameters.AddWithValue("@login", Login);
					cmd.Parameters.AddWithValue("@password", Password);
					cmd.ExecuteNonQuery();
					return true;
				}
				catch (Exception ex)
				{
					return false;
				}
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
