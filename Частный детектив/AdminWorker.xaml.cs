using MySqlConnector;
using System;
using System.Data;
using System.Windows;

namespace Частный_детектив
{
    /// <summary>
    /// Логика взаимодействия для AdminWorker.xaml
    /// </summary>
    public partial class AdminWorker : Window
    {
        int Id;
        int Workerid;
        int Del;
        public AdminWorker(int id, int del, int workerid)
        {
            InitializeComponent();
            Id = id;
            Del = del;
            Workerid = workerid;
            
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            var form = new AdminWork(Id);
            form.Show();
            this.Hide();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MySqlConnection conn = new MySqlConnection("server=127.0.0.1;port=3306;database=mydb;user id=root;password=1234;charset=utf8;Pooling=false;SslMode=None;");
            if (Del == 0)
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                    MySqlCommand cmd1 = new MySqlCommand("select * from worker where worker_id = @worker_id", conn);
                    cmd1.Parameters.AddWithValue("@worker_id", Workerid);
                    MySqlDataReader reader = cmd1.ExecuteReader();
                    while (reader.Read())
                    {
                        textBoxName.Text = reader["name"].ToString();
                        textBoxNumber.Text = reader["number"].ToString();
                        textBoxMail.Text = reader["mail"].ToString();
                        textBoxLogin.Text = reader["login"].ToString();
                        textBoxPassword.Text = reader["password"].ToString();
                        textBoxOtdel.Text = reader["otdel"].ToString();
                    }
                    reader.Close();
                }
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
			string name = "";
			MySqlConnection conn = new MySqlConnection("server=127.0.0.1;port=3306;database=mydb;user id=root;password=1234;charset=utf8;Pooling=false;SslMode=None;");
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                if (Del == 1)
                {
					MySqlCommand cmd1 = new MySqlCommand("select Client_Id from Client where login = @login or mail = @mail", conn);
					cmd1.Parameters.AddWithValue("@login", textBoxLogin.Text);
					cmd1.Parameters.AddWithValue("@mail", textBoxMail.Text);
					if (cmd1.ExecuteScalar() != null) { name = cmd1.ExecuteScalar().ToString(); MessageBox.Show("Пользователь с таким логином уже существует"); }
					else
					{
						MySqlCommand cmd2 = new MySqlCommand("select Worker_Id from Worker where login = @login or mail = @mail", conn);
						cmd2.Parameters.AddWithValue("@login", textBoxLogin.Text);
						cmd2.Parameters.AddWithValue("@mail", textBoxMail.Text);
						if (cmd2.ExecuteScalar() != null) { name = cmd2.ExecuteScalar().ToString(); MessageBox.Show("Пользователь с таким логином уже существует"); }
						else
						{
							MySqlCommand cmd3 = new MySqlCommand("select Adminis_Id from Adminis where login = @login", conn);
							cmd3.Parameters.AddWithValue("@login", textBoxLogin.Text);
							cmd3.Parameters.AddWithValue("@mail", textBoxMail.Text);
							if (cmd3.ExecuteScalar() != null) { name = cmd3.ExecuteScalar().ToString(); MessageBox.Show("Пользователь с таким логином уже существует"); }
							else
							{
								MySqlCommand cmd4 = new MySqlCommand("select Manager_Id from Manager where login = @login", conn);
								cmd4.Parameters.AddWithValue("@login", textBoxLogin.Text);
								cmd4.Parameters.AddWithValue("@mail", textBoxMail.Text);
								if (cmd4.ExecuteScalar() != null) { name = cmd4.ExecuteScalar().ToString(); MessageBox.Show("Пользователь с таким логином уже существует"); }
								else
								{
									MySqlCommand cmd = new MySqlCommand("insert into worker (name, number, mail, login, password, otdel) values(@name, @number, @mail, @login, @password, @otdel);", conn);
									cmd.Parameters.AddWithValue("@name", textBoxName.Text);
									cmd.Parameters.AddWithValue("@number", textBoxNumber.Text);
									cmd.Parameters.AddWithValue("@mail", textBoxMail.Text);
									cmd.Parameters.AddWithValue("@login", textBoxLogin.Text);
									cmd.Parameters.AddWithValue("@password", textBoxPassword.Text);
									cmd.Parameters.AddWithValue("@otdel", textBoxOtdel.Text);
									cmd.ExecuteNonQuery();
									MessageBox.Show("Сотрудник добавлен");
								}
							}
						}
					}
                }
                if (Del == 0)
                {
                    MySqlCommand cmd = new MySqlCommand("UPDATE worker SET name = @name, number = @number, mail = @mail, login = @login, password = @password, otdel = @otdel where worker_id = @worker_id;", conn);
                    cmd.Parameters.AddWithValue("@name", textBoxName.Text);
                    cmd.Parameters.AddWithValue("@number", textBoxNumber.Text);
                    cmd.Parameters.AddWithValue("@mail", textBoxMail.Text);
                    cmd.Parameters.AddWithValue("@login", textBoxLogin.Text);
                    cmd.Parameters.AddWithValue("@password", textBoxPassword.Text);
                    cmd.Parameters.AddWithValue("@otdel", textBoxOtdel.Text);
                    cmd.Parameters.AddWithValue("@worker_id", Workerid);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Успешно изменено");
                }
                var form = new AdminWork(Id);
                form.Show();
                this.Hide();

            }
        }
        public bool addworker(string Name, string Number, string Mail, string Login, string Password, string Otdel)
        {
			string name = "";
			MySqlConnection conn = new MySqlConnection("server=127.0.0.1;port=3306;database=mydb;user id=root;password=1234;charset=utf8;Pooling=false;SslMode=None;");
			if (conn.State == ConnectionState.Closed)
			{
				conn.Open();
				MySqlCommand cmd1 = new MySqlCommand("select Client_Id from Client where login = @login or mail = @mail", conn);
				cmd1.Parameters.AddWithValue("@login", textBoxLogin.Text);
				cmd1.Parameters.AddWithValue("@mail", textBoxMail.Text);
				if (cmd1.ExecuteScalar() != null) { name = cmd1.ExecuteScalar().ToString(); return false; }
				else
				{
					MySqlCommand cmd2 = new MySqlCommand("select Worker_Id from Worker where login = @login or mail = @mail", conn);
					cmd2.Parameters.AddWithValue("@login", textBoxLogin.Text);
					cmd2.Parameters.AddWithValue("@mail", textBoxMail.Text);
					if (cmd2.ExecuteScalar() != null) { name = cmd2.ExecuteScalar().ToString(); return false; }
					else
					{
						MySqlCommand cmd3 = new MySqlCommand("select Adminis_Id from Adminis where login = @login or mail = @mail", conn);
						cmd3.Parameters.AddWithValue("@login", textBoxLogin.Text);
						cmd3.Parameters.AddWithValue("@mail", textBoxMail.Text);
						if (cmd3.ExecuteScalar() != null) { name = cmd3.ExecuteScalar().ToString(); return false; }
						else
						{
							MySqlCommand cmd4 = new MySqlCommand("select Manager_Id from Manager where login = @login or mail = @mail", conn);
							cmd4.Parameters.AddWithValue("@login", textBoxLogin.Text);
							cmd4.Parameters.AddWithValue("@mail", textBoxMail.Text);
							if (cmd4.ExecuteScalar() != null) { name = cmd4.ExecuteScalar().ToString(); return false; }
							else
							{
								MySqlCommand cmd = new MySqlCommand("insert into worker (name, number, mail, login, password, otdel) values(@name, @number, @mail, @login, @password, @otdel);", conn);
								cmd.Parameters.AddWithValue("@name", Name);
								cmd.Parameters.AddWithValue("@number", Number);
								cmd.Parameters.AddWithValue("@mail", Mail);
								cmd.Parameters.AddWithValue("@login", Login);
								cmd.Parameters.AddWithValue("@password", Password);
								cmd.Parameters.AddWithValue("@otdel", Otdel);
								cmd.ExecuteNonQuery();
								return true;
							}
						}
					}

				}
			}
			
			else
			{
				return false;
			}
		}
	}
}
