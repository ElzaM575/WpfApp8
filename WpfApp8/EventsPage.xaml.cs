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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp8.DBConnection;

namespace WpfApp8
{
    /// <summary>
    /// Логика взаимодействия для EventsPage.xaml
    /// </summary>
    public partial class EventsPage : Page
    {
        public EventsPage()
        {
            InitializeComponent();
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new circusEntities())
            {
                var list = db.Event.ToList();
                EventList.ItemsSource = list.Select(ev =>
                {
                    string type = ev.type == "priezzhee"
                        ? $"Приезжее ({ev.company}, предоплата: {ev.prepayment})"
                        : "Частное (предоплата: 0)";
                    return $"{ev.name} | {FormatDate(ev.date)} | {type}";
                }).ToList();
            }
        }

        private string FormatDate(DateTime? date)
        {
            return date.HasValue ? date.Value.ToShortDateString() : "Не указано";
        }
    }
}
