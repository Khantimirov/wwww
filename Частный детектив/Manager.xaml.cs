using MySqlConnector;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Частный_детектив
{
	/// <summary>
	/// Логика взаимодействия для Manager.xaml
	/// </summary>
	public partial class Manager : Window
	{
		int Id;
		TextBlock label;
		Button ok;
		Border border;
		DockPanel panel;
		public Manager(int id)
		{
			InitializeComponent();
			Id = id;
		}

		private void Window_Closed_1(object sender, EventArgs e)
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
				MySqlCommand cmd1 = new MySqlCommand("select * from zayavka where status=@kop and Manager_Id=@id ORDER BY zayavka_id DESC", conn);
				cmd1.Parameters.AddWithValue("@kop", "ожидание звонка");
				cmd1.Parameters.AddWithValue("@id", Id);
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
					label.Text = reader["time"].ToString();
					panel.Children.Add(label);
					label = new TextBlock();
					label.Style = (Style)Resources["labs"];
					label.Text = reader["timenow"].ToString();
					panel.Children.Add(label);

					label = new TextBlock();
					label.Style = (Style)Resources["labs"];
					label.Text = reader["status"].ToString();
					panel.Children.Add(label);

					ok = new Button();
					ok.Style = (Style)Resources["buts"];
					ok.Click += new RoutedEventHandler(ok_Click);
					ok.Name = "But" + Convert.ToString(reader["zayavka_id"]);
					panel.Children.Add(ok);

					border.Child = panel;
					stack.Children.Add(border);

				}
				reader.Close();
				cmd1 = new MySqlCommand("select * from zayavka where status=@kop and Manager_Id=@id ORDER BY zayavka_id DESC", conn);
				cmd1.Parameters.AddWithValue("@kop", "вам позвонили");
				cmd1.Parameters.AddWithValue("@id", Id);
				reader = cmd1.ExecuteReader();
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
					label.Text = reader["time"].ToString();
					panel.Children.Add(label);
					label = new TextBlock();
					label.Style = (Style)Resources["labs"];
					label.Text = reader["timenow"].ToString();
					panel.Children.Add(label);

					label = new TextBlock();
					label.Style = (Style)Resources["labs"];
					label.Text = reader["status"].ToString();
					panel.Children.Add(label);

					ok = new Button();
					ok.Style = (Style)Resources["buts2"];

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
				MySqlCommand cmd = new MySqlCommand("update zayavka set status=@new where zayavka_id = @zayavka_id;", conn);
				cmd.Parameters.AddWithValue("@new", "вам позвонили");
				cmd.Parameters.AddWithValue("@zayavka_id", get);
				cmd.ExecuteNonQuery();

				(sender as FrameworkElement).Style = (Style)Resources["buts2"];
				var form = new Manager(Id);
				form.Show();
				this.Hide();
			}
		}
		public bool redzayavka(string Name)
		{
			MySqlConnection conn = new MySqlConnection("server=127.0.0.1;port=3306;database=mydb;user id=root;password=1234;charset=utf8;Pooling=false;SslMode=None;");
			if (conn.State == ConnectionState.Closed)
			{
				conn.Open();
				try
				{
					string text = Name;
					text = text.Replace("But", "");
					int get = Convert.ToInt32(text);
					MySqlCommand cmd = new MySqlCommand("update zayavka set status=@new where zayavka_id = @zayavka_id;", conn);
					cmd.Parameters.AddWithValue("@new", "вам позвонили");
					cmd.Parameters.AddWithValue("@zayavka_id", get);
					cmd.ExecuteNonQuery();
					return true;
				}
				catch { return false; }
			}
			else { return false; }
		}
	}
}
