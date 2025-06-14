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
    /// Логика взаимодействия для HoloRoomEditWindow.xaml
    /// </summary>
    public partial class HoloRoomEditWindow : Window
    {
        public HoloRoom Room { get; private set; }

        public HoloRoomEditWindow()
        {
            InitializeComponent();
            Room = new HoloRoom();
            DataContext = this;
        }
        public HoloRoomEditWindow(HoloRoom room) : this()
        {
            Room = new HoloRoom
            {
                room_id = room.room_id,
                name = room.name
            };
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
