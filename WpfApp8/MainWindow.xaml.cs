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

namespace WpfApp8
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }
        private void Animals_Click(object sender, RoutedEventArgs e) => MainFrame.Navigate(new AnimalsPage());
        private void Artists_Click(object sender, RoutedEventArgs e) => MainFrame.Navigate(new ArtistsPage());
        private void Holograms_Click(object sender, RoutedEventArgs e) => MainFrame.Navigate(new HologramsPage());
        private void Events_Click(object sender, RoutedEventArgs e) => MainFrame.Navigate(new EventsPage());
    }
}
