using System;
using System.Windows;
using System.Net.Mail;
using System.Net;
using MySqlConnector;
using System.Data;

namespace Частный_детектив
{
    /// <summary>
    /// Логика взаимодействия для Vostan.xaml
    /// </summary>
    public partial class Vostan : Window
    {
        public Vostan()
        {
            InitializeComponent();
            this.Left = 0;
            this.Top = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MailAddress from = new MailAddress("38_g@inbox.ru", "Восстановление данных");
                MailAddress to = new MailAddress(textBoxMail.Text);
                MailMessage m = new MailMessage(from, to);
                m.Subject = "Маркетинговое агентство";
                MySqlConnection conn = new MySqlConnection("server=127.0.0.1;port=3306;database=mydb;user id=root;password=1234;charset=utf8;Pooling=false;SslMode=None;");
                string log = "";
                string pas = "";
                try
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                        if (textBoxMail.Text != "")
                        {
                            if (log == "")
                            {
                                MySqlCommand cmd1 = new MySqlCommand("select Client_Id from Client where mail = @mail", conn);
                                cmd1.Parameters.AddWithValue("@mail", textBoxMail.Text);
                                if (cmd1.ExecuteScalar() != null)
                                {
                                    MySqlCommand cmd2 = new MySqlCommand("select login from Client where mail = @mail", conn);
                                    cmd2.Parameters.AddWithValue("@mail", textBoxMail.Text);

                                    MySqlCommand cmd3 = new MySqlCommand("select password from Client where mail = @mail", conn);
                                    cmd3.Parameters.AddWithValue("@mail", textBoxMail.Text);

                                    log = cmd2.ExecuteScalar().ToString();
                                    pas = cmd3.ExecuteScalar().ToString();
                                    m.Body = "<h1>Логин: " + log + "</h1>" + "<h1>Пароль: " + pas + "</h1>";
                                }
                            }
                            else if (log == "")
                            {
                                MySqlCommand cmd1 = new MySqlCommand("select Worker_Id from worker where mail = @mail", conn);
                                cmd1.Parameters.AddWithValue("@mail", textBoxMail.Text);
                                if (cmd1.ExecuteScalar() != null)
                                {
                                    MySqlCommand cmd2 = new MySqlCommand("select login from worker where mail = @mail", conn);
                                    cmd2.Parameters.AddWithValue("@mail", textBoxMail.Text);

                                    MySqlCommand cmd3 = new MySqlCommand("select password from worker where mail = @mail", conn);
                                    cmd3.Parameters.AddWithValue("@mail", textBoxMail.Text);

                                    log = cmd2.ExecuteScalar().ToString();
                                    pas = cmd3.ExecuteScalar().ToString();
                                    m.Body = "<h1>Логин: " + log + "</h1>" + "<h1>Пароль: " + pas + "</h1>";
                                }
                            }
                            else if (log == "")
                            {
                                MySqlCommand cmd1 = new MySqlCommand("select Manager_Id from manager where mail = @mail", conn);
                                cmd1.Parameters.AddWithValue("@mail", textBoxMail.Text);
                                if (cmd1.ExecuteScalar() != null)
                                {
                                    MySqlCommand cmd2 = new MySqlCommand("select login from manager where mail = @mail", conn);
                                    cmd2.Parameters.AddWithValue("@mail", textBoxMail.Text);

                                    MySqlCommand cmd3 = new MySqlCommand("select password from manager where mail = @mail", conn);
                                    cmd3.Parameters.AddWithValue("@mail", textBoxMail.Text);

                                    log = cmd2.ExecuteScalar().ToString();
                                    pas = cmd3.ExecuteScalar().ToString();
                                    m.Body = "<h1>Логин: " + log + "</h1>" + "<h1>Пароль: " + pas + "</h1>";
                                }
                            }
                            if (log == "")
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
                m.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
                smtp.Credentials = new NetworkCredential("38_g@inbox.ru", "pPydF1U0ePfCmTXXTaum");
                smtp.EnableSsl = true;
                smtp.Send(m);
                MessageBox.Show("Логин и пароль отправлены на почту");
                Vhod form = new Vhod();
                form.Show();
                this.Hide();
            }
            catch
            {
                MessageBox.Show("Пользователя с такой почтой не существует");
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
