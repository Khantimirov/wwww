using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Частный_детектив
{
    /// <summary>
    /// Логика взаимодействия для DetectivZayav.xaml
    /// </summary>
    public partial class DetectivZayav : Window
    {
        StackPanel formaZayavki;
        TextBox info;
        Button ok;
        EntityModelContainer db = new EntityModelContainer();
        Detective Detective;
        string text;
        Zayavka zayavka1;
        public DetectivZayav(Detective detective)
        {
            InitializeComponent();
            this.Left = 0;
            this.Top = 0;
            Detective = detective;
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Detectiv form = new Detectiv(Detective);
            form.textblock.Text = "Добро пожаловать в Личный кабинет, " + Detective.Name + "\n" + "Здесь можете просматривать входящие заявки на консультацию, писать и отвечать на сообщения.";
            form.Show();
            this.Hide();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Detectiv form = new Detectiv(Detective);
            form.textblock.Text = "Добро пожаловать в Личный кабинет, " + Detective.Name + "\n" + "Здесь можете просматривать входящие заявки на консультацию, писать и отвечать на сообщения.";
            form.Show();
            this.Hide();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = Detective.Id;
                var query = from n in db.Zayavkas
                            join m in db.Detectives
                            on n.Detective equals m
                            where m.Id == id && n.Status == "На рассмотрении"
                            orderby n.Id
                            select new { n.Id };

                foreach (var w in query)
                    text = text + "," + $"{w}";

                if (text.Contains("Id"))
                {
                    text = text.Replace("Id", "");
                    text = text.Replace("=", "");
                    text = text.Replace(" ", "");
                    text = text.Replace("{", "");
                    text = text.Replace("}", "");
                    string[] zayavk = text.Split(new char[] { ',' });
                    int dl = zayavk.Length;
                    dl = dl - 1;
                    int sch = 1;

                    while (dl != 0)
                    {
                        id = Convert.ToInt32(zayavk[sch]);
                        zayavka1 = db.Zayavkas.Find(id);
                        formaZayavki = new StackPanel();
                        formaZayavki.Style = (Style)Resources["Panel"];
                        stack.Children.Add(formaZayavki);

                        info = new TextBox();
                        info.Style = (Style)Resources["Text"];
                        info.Text = "Имя: " + zayavka1.Name + "\n" + "Номер: " + zayavka1.Number + "\n" + "Информация: " + zayavka1.TextZayavki;
                        formaZayavki.Children.Add(info);

                        ok = new Button();
                        ok.Style = (Style)Resources["NotRead"];
                        ok.Click += new RoutedEventHandler(ok_Click);
                        ok.Name = "But" + Convert.ToString(zayavka1.Id);

                        formaZayavki.Children.Add(ok);

                        dl = dl - 1;
                        sch = sch + 1;
                    }
                }
            }
            catch { }
            try
            {
                text = "";
                int id = Detective.Id;
                var query = from n in db.Zayavkas
                            join m in db.Detectives
                            on n.Detective equals m
                            where m.Id == id && n.Status == "Прочитано"
                            orderby n.Id
                            select new { n.Id };

                foreach (var w in query)
                    text = text + "," + $"{w}";

                if (text.Contains("Id"))
                {
                    text = text.Replace("Id", "");
                    text = text.Replace("=", "");
                    text = text.Replace(" ", "");
                    text = text.Replace("{", "");
                    text = text.Replace("}", "");
                    string[] zayavk = text.Split(new char[] { ',' });
                    int dl = zayavk.Length;
                    dl = dl - 1;
                    int sch = 1;

                    while (dl != 0)
                    {
                        id = Convert.ToInt32(zayavk[sch]);
                        zayavka1 = db.Zayavkas.Find(id);
                        formaZayavki = new StackPanel();
                        formaZayavki.Style = (Style)Resources["Panel"];
                        stack.Children.Add(formaZayavki);

                        info = new TextBox();
                        info.Style = (Style)Resources["Text"];
                        info.Text = "Имя: " + zayavka1.Name + "\n" + "Номер: " + zayavka1.Number + "\n" + "Информация: " + zayavka1.TextZayavki;
                        formaZayavki.Children.Add(info);

                        ok = new Button();
                        ok.Style = (Style)Resources["Read"];
                        ok.Name = "But" + Convert.ToString(zayavka1.Id);
                        formaZayavki.Children.Add(ok);

                        dl = dl - 1;
                        sch = sch + 1;
                    }
                }
            }
            catch { }

        }

        void ok_Click(object sender, RoutedEventArgs e)
        {
            (sender as FrameworkElement).Style = (Style)Resources["Read"];
            string text = (sender as FrameworkElement).Name;
            text = text.Replace("But", "");
            int get = Convert.ToInt32(text);
            zayavka1 = db.Zayavkas.Find(get);
            zayavka1.Status = "Прочитано";
            db.SaveChanges();
        }

    }
}
