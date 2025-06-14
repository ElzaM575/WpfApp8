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
            LoadEvents();
        }

        private void LoadEvents()
        {
            using (var db = new circusEntities())
            {
                EventList.ItemsSource = db.Event.ToList().Select(e => new
                {
                    Event = e,
                    DisplayInfo = $"{e.name} | {FormatDate(e.date)} | " +
                                 $"{(e.type == "priezzhee" ? $"Приезжее ({e.company}, предоплата: {e.prepayment})" : "Частное")}"
                }).ToList();
            }
        }
        private void Load_Click(object sender, RoutedEventArgs e)
        {
            LoadEvents();
        }

        private string FormatDate(DateTime? date)
        {
            return date.HasValue ? date.Value.ToShortDateString() : "Не указано";
        }

        

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (EventList.SelectedItem != null)
            {
                dynamic selected = EventList.SelectedItem;
                Event selectedEvent = selected.Event;

                var editWindow = new EventEditWindow(selectedEvent);
                if (editWindow.ShowDialog() == true)
                {
                    using (var db = new circusEntities())
                    {
                        var ev = db.Event.Find(editWindow.Event.event_id);
                        if (ev != null)
                        {
                            ev.name = editWindow.Event.name;
                            ev.date = editWindow.Event.date;
                            ev.type = editWindow.Event.type;
                            ev.prepayment = editWindow.Event.prepayment;
                            ev.company = editWindow.Event.company;
                            db.SaveChanges();
                            LoadEvents();
                        }
                    }
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (EventList.SelectedItem != null &&
            MessageBox.Show("Удалить это мероприятие?", "Подтверждение",
            MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                dynamic selected = EventList.SelectedItem;
                Event selectedEvent = selected.Event;

                using (var db = new circusEntities())
                {
                    var ev = db.Event.Find(selectedEvent.event_id);
                    if (ev != null)
                    {
                        db.Event.Remove(ev);
                        db.SaveChanges();
                        LoadEvents();
                    }
                }
            }
        }

        private void CompleteEvent_Click(object sender, RoutedEventArgs e)
        {
            if (EventList.SelectedItem != null)
            {
                dynamic selected = EventList.SelectedItem;
                Event selectedEvent = selected.Event;

                var completeWindow = new CompleteEventWindow(selectedEvent);
                if (completeWindow.ShowDialog() == true)
                {
                    using (var db = new circusEntities())
                    {
                        var ev = db.Event.Find(selectedEvent.event_id);
                        if (ev != null)
                        {
                            ev.prepayment = completeWindow.Profit;
                            
                            db.SaveChanges();
                            LoadEvents();
                        }
                    }
                }
            }
        }

    

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var editWindow = new EventEditWindow();
            if (editWindow.ShowDialog() == true)
            {
                using (var db = new circusEntities())
                {
                    db.Event.Add(editWindow.Event);
                    db.SaveChanges();
                    LoadEvents();
                }
            }
        }
    }
}
