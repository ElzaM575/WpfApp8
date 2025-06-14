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
    /// Логика взаимодействия для HologramsPage.xaml
    /// </summary>
    public partial class HologramsPage : Page
    {
        public HologramsPage()
        {
            InitializeComponent();
            LoadHoloRooms();
        }
        private void LoadHoloRooms()
        {
            using (var db = new circusEntities())
            {
                var rooms = db.HoloRoom.ToList();
                var responsibles = db.Responsible.ToList();

                HoloList.ItemsSource = rooms.Select(room => new
                {
                    Room = room,
                    RoomName = room.name,
                    Responsibles = string.Join(", ", responsibles
                        .Where(r => r.room_id == room.room_id)
                        .Select(r => r.name))
                }).ToList();
            }
        }
        private void Load_Click(object sender, RoutedEventArgs e)
        {
            LoadHoloRooms();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var editWindow = new HoloRoomEditWindow();
            if (editWindow.ShowDialog() == true)
            {
                using (var db = new circusEntities())
                {
                    db.HoloRoom.Add(editWindow.Room);
                    db.SaveChanges();
                    LoadHoloRooms();
                }
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {

            if (HoloList.SelectedItem != null)
            {
                dynamic selected = HoloList.SelectedItem;
                var editWindow = new HoloRoomEditWindow(selected.Room);
                if (editWindow.ShowDialog() == true)
                {
                    using (var db = new circusEntities())
                    {
                        var room = db.HoloRoom.Find(editWindow.Room.room_id);
                        if (room != null)
                        {
                            room.name = editWindow.Room.name;
                            db.SaveChanges();
                            LoadHoloRooms();
                        }
                    }
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (HoloList.SelectedItem != null &&
                MessageBox.Show("Удалить этот кабинет?", "Подтверждение",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                dynamic selected = HoloList.SelectedItem;
                using (var db = new circusEntities())
                {
                    var room = db.HoloRoom.Find(selected.Room.room_id);
                    if (room != null)
                    {
                        db.HoloRoom.Remove(room);
                        db.SaveChanges();
                        LoadHoloRooms();
                    }
                }
            }
        }

        private void AssignRespo_Click(object sender, RoutedEventArgs e)
        {
            if (HoloList.SelectedItem != null)
            {
                dynamic selected = HoloList.SelectedItem;
                var assignWindow = new AssignResponsibleWindow(selected.Room.room_id);
                assignWindow.ShowDialog();
                LoadHoloRooms();
            }
        }
    }
}
