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
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new circusEntities())
            {
                var list = db.Artist.ToList();
                ArtistList.ItemsSource = list.Select(a =>
                {
                    int success = a.success_count ?? 0;
                    return $"{a.name} - {GetRank(success)}";
                }).ToList();
            }
        }

        private string GetRank(int count) =>
            count <= 10 ? "Начинающий" :
            count <= 30 ? "Продвигающийся" :
            "VIP";
    }
}
