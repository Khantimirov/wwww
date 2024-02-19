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
    /// Логика взаимодействия для UslugiRed.xaml
    /// </summary>
    public partial class UslugiRed : Window
    {
        Usluga Usluga;
        EntityModelContainer db = new EntityModelContainer();
        public UslugiRed(Usluga usluga)
        {
            InitializeComponent();
            this.Left = 0;
            this.Top = 0;
            Usluga = usluga;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;           
        }
    }
}
