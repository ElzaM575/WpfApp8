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
    /// Логика взаимодействия для AssignResponsibleWindow.xaml
    /// </summary>
    public partial class AssignResponsibleWindow : Window
    {
        private int _roomId;
        public AssignResponsibleWindow(int roomId)
        {
            InitializeComponent();
            _roomId = roomId;
            LoadData();
        }
        private void LoadData()
        {
            using (var db = new circusEntities())
            {
                var room = db.HoloRoom.Find(_roomId);
                RoomName = room?.name;

                var allResponsibles = db.Responsible.ToList();
                CurrentResponsibles = allResponsibles.Where(r => r.room_id == _roomId).ToList();
                AvailableResponsibles = allResponsibles.Except(CurrentResponsibles).ToList();

                ResponsiblesList.ItemsSource = CurrentResponsibles;
            }
        }
        public string RoomName { get; set; }
        public List<Responsible> CurrentResponsibles { get; set; }
        public List<Responsible> AvailableResponsibles { get; set; }

        private void AddResponsible_Click(object sender, RoutedEventArgs e)
        {
            var selectWindow = new SelectResponsibleWindow(AvailableResponsibles);
            if (selectWindow.ShowDialog() == true && selectWindow.SelectedResponsible != null)
            {
                using (var db = new circusEntities())
                {
                    var responsible = db.Responsible.Find(selectWindow.SelectedResponsible.responsible_id);
                    if (responsible != null)
                    {
                        responsible.room_id = _roomId;
                        db.SaveChanges();
                        LoadData();
                    }
                }
            }
        }

        private void RemoveResponsible_Click(object sender, RoutedEventArgs e)
        {
            if (ResponsiblesList.SelectedItem is Responsible selected)
            {
                using (var db = new circusEntities())
                {
                    var responsible = db.Responsible.Find(selected.responsible_id);
                    if (responsible != null)
                    {
                        responsible.room_id = null;
                        db.SaveChanges();
                        LoadData();
                    }
                }
            }
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
