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
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new circusEntities())
            {
                var data = db.HoloRoom.ToList();
                var responsibles = db.Responsible.ToList();

                var holoInfo = data.Select(room =>
                {
                    var resps = responsibles.Where(r => r.room_id == room.room_id)
                                            .Select(r => r.name);
                    return $"{room.name} - {string.Join(", ", resps)}";
                }).ToList();

                HoloList.ItemsSource = holoInfo;
            }
        }
    }
}
