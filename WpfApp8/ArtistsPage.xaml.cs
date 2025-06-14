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
    /// Логика взаимодействия для ArtistsPage.xaml
    /// </summary>
    public partial class ArtistsPage : Page
    {
        public ArtistsPage()
        {
            InitializeComponent();
            LoadArtists();
        }
        private void LoadArtists()
        {
            using (var db = new circusEntities())
            {
                ArtistList.ItemsSource = db.Artist.ToList().Select(a => new
                {
                    Artist = a,
                    DisplayInfo = $"{a.name} - {GetRank(a.success_count ?? 0)} {(a.success_count > 30 ? "(гримерка)" : "")}"
                }).ToList();
            }
        }
        private void Load_Click(object sender, RoutedEventArgs e)
        {
            LoadArtists();
        }


        private string GetRank(int count) =>
            count <= 10 ? "Начинающий" :
            count <= 30 ? "Продвигающийся" :
            "VIP";


        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (ArtistList.SelectedItem != null)
            {
                dynamic selected = ArtistList.SelectedItem;
                Artist selectedArtist = selected.Artist;

                var editWindow = new ArtistEditWindow(selectedArtist);
                if (editWindow.ShowDialog() == true)
                {
                    using (var db = new circusEntities())
                    {
                        var artist = db.Artist.Find(editWindow.Artist.artist_id);
                        if (artist != null)
                        {
                            artist.name = editWindow.Artist.name;
                            artist.success_count = editWindow.Artist.success_count;
                            db.SaveChanges();
                            LoadArtists();
                        }
                    }
                }
            }
        }

        

       
       
        

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (ArtistList.SelectedItem != null &&
                MessageBox.Show("Удалить этого артиста?", "Подтверждение",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                dynamic selected = ArtistList.SelectedItem;
                Artist selectedArtist = selected.Artist;

                using (var db = new circusEntities())
                {
                    var artist = db.Artist.Find(selectedArtist.artist_id);
                    if (artist != null)
                    {
                        db.Artist.Remove(artist);
                        db.SaveChanges();
                        LoadArtists();
                    }
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var editWindow = new ArtistEditWindow();
            if (editWindow.ShowDialog() == true)
            {
                using (var db = new circusEntities())
                {
                    db.Artist.Add(editWindow.Artist);
                    db.SaveChanges();
                    LoadArtists();
                }
            }
        }
    }
}
