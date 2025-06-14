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
using WpfApp8.DBConnection;

namespace WpfApp8
{
    /// <summary>
    /// Логика взаимодействия для CompleteEventWindow.xaml
    /// </summary>
    public partial class CompleteEventWindow : Window
    {
        public decimal Profit { get; set; }
        public decimal Expenses { get; set; }
        public CompleteEventWindow(Event eventToComplete)
        {
            InitializeComponent();
            Title = $"Завершение мероприятия: {eventToComplete.name}";
            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
