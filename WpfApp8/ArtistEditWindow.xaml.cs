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
    /// Логика взаимодействия для ArtistEditWindow.xaml
    /// </summary>
    public partial class ArtistEditWindow : Window
    {
        public Artist Artist { get; private set; }
        public string ArtistRank
        {
            get
            {
                int count = Artist.success_count ?? 0;
                return count <= 10 ? "Начинающий" :
                       count <= 30 ? "Продвигающийся" : "VIP";
            }
        }
        public ArtistEditWindow()
        {
            InitializeComponent();
            Artist = new Artist();
            DataContext = this;
        }
        public ArtistEditWindow(Artist artist) : this()
        {
            Artist = new Artist
            {
                artist_id = artist.artist_id,
                name = artist.name,
                success_count = artist.success_count,
               
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
