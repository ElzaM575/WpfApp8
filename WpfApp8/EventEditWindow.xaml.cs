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
    /// Логика взаимодействия для EventEditWindow.xaml
    /// </summary>
    public partial class EventEditWindow : Window
    {
        public Event Event { get; private set; }
        public EventEditWindow()
        {
            InitializeComponent();
            Event = new Event { date = DateTime.Today };
            DataContext = this;
        }
        public EventEditWindow(Event existingEvent)
        {
            InitializeComponent();
            Event = new Event
            {
                event_id = existingEvent.event_id,
                name = existingEvent.name,
                date = existingEvent.date,
                type = existingEvent.type,
                prepayment = existingEvent.prepayment,
                company = existingEvent.company
            };
            DataContext = this;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
