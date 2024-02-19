using System;
using System.Data;
using System.Windows;
using MySqlConnector;

namespace Частный_детектив
{
    /// <summary>
    /// Логика взаимодействия для Vhod.xaml
    /// </summary>
    public partial class Vhod : Window
    {
        public Vhod()
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
                    if (textBoxLogin.Text != "" && textBoxPassword.Text != "")
                    {
                        if (name == "")
                        {
                            MySqlCommand cmd1 = new MySqlCommand("select Client_Id from Client where login = @login and password = @password", conn);
                            cmd1.Parameters.AddWithValue("@login", textBoxLogin.Text);
							cmd1.Parameters.AddWithValue("@password", textBoxPassword.Text);
							if (cmd1.ExecuteScalar() != null)
                            {
                                name = cmd1.ExecuteScalar().ToString();
                                var form = new Klient(Convert.ToInt32(name));
                                form.Show();
                                this.Hide();
                            }
                        }
                        if (name == "")
                        {
                            MySqlCommand cmd2 = new MySqlCommand("select Worker_Id from Worker where login = @login and password = @password", conn);
                            cmd2.Parameters.AddWithValue("@login", textBoxLogin.Text);
							cmd2.Parameters.AddWithValue("@password", textBoxPassword.Text);
							if (cmd2.ExecuteScalar() != null)
                            {
                                name = cmd2.ExecuteScalar().ToString();
                                var form = new WorkerLK(Convert.ToInt32(name));
                                form.Show();
                                this.Hide();
                            }
                        }
                        if (name == "")
                        {
                            MySqlCommand cmd3 = new MySqlCommand("select Adminis_Id from Adminis where login = @login and password = @password", conn);
                            cmd3.Parameters.AddWithValue("@login", textBoxLogin.Text);
							cmd3.Parameters.AddWithValue("@password", textBoxPassword.Text);
							if (cmd3.ExecuteScalar() != null)
                            {
                                name = cmd3.ExecuteScalar().ToString();
                                var form = new Admin(Convert.ToInt32(name));
                                form.Show();
                                this.Hide();
                            }
                        }
						if (name == "")
						{
							MySqlCommand cmd3 = new MySqlCommand("select Manager_Id from Manager where login = @login and password = @password", conn);
							cmd3.Parameters.AddWithValue("@login", textBoxLogin.Text);
							cmd3.Parameters.AddWithValue("@password", textBoxPassword.Text);
							if (cmd3.ExecuteScalar() != null)
							{
								name = cmd3.ExecuteScalar().ToString();
								var form = new Manager(Convert.ToInt32(name));
								form.Show();
								this.Hide();
							}
						}
						if (name == "")
                        {
                            MessageBox.Show("пользователя не существует");
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

		public bool auth(string Login, string Password)
		{
			MySqlConnection conn = new MySqlConnection("server=127.0.0.1;port=3306;database=mydb;user id=root;password=1234;charset=utf8;Pooling=false;SslMode=None;");
			if (conn.State == ConnectionState.Closed)
			{
				conn.Open();
				MySqlCommand cmd1 = new MySqlCommand("select Client_Id from Client where login = @login and password = @password", conn);
				cmd1.Parameters.AddWithValue("@login", Login);
				cmd1.Parameters.AddWithValue("@password", Password);
				if (cmd1.ExecuteScalar() != null)
				{
					return true;

				}
				else
				{
					MySqlCommand cmd2 = new MySqlCommand("select Worker_Id from Worker where login = @login and password = @password", conn);
					cmd2.Parameters.AddWithValue("@login", Login);
					cmd2.Parameters.AddWithValue("@password", Password);
					if (cmd2.ExecuteScalar() != null)
					{
						return true;
					}
					else
					{
						MySqlCommand cmd3 = new MySqlCommand("select Adminis_Id from Adminis where login = @login and password = @password", conn);
						cmd3.Parameters.AddWithValue("@login", Login);
						cmd3.Parameters.AddWithValue("@password", Password);
						if (cmd3.ExecuteScalar() != null)
						{
							return true;
						}
						else
						{
							MySqlCommand cmd4 = new MySqlCommand("select Manager_Id from Manager where login = @login and password = @password", conn);
							cmd4.Parameters.AddWithValue("@login", Login);
							cmd4.Parameters.AddWithValue("@password", Password);
							if (cmd4.ExecuteScalar() != null)
							{
								return true;
							}
							else
							{
								return false;
							}
						}
					}
				}
			}
            else { return false; }
		}

		public bool db()
		{
			MySqlConnection conn = new MySqlConnection("server=127.0.0.1;port=3306;database=mydb;user id=root;password=1234;charset=utf8;Pooling=false;SslMode=None;");
			if (conn.State == ConnectionState.Closed)
			{
				conn.Open();
                return true;
			}
			else { return false; }
		}


		private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Vostan form = new Vostan();
            form.Show();
            this.Hide();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Registr form = new Registr();
            form.Show();
            this.Hide();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
