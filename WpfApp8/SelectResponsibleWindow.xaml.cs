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
    /// Логика взаимодействия для SelectResponsibleWindow.xaml
    /// </summary>
    public partial class SelectResponsibleWindow : Window
    {
        public Responsible SelectedResponsible { get; private set; }
        public SelectResponsibleWindow(IEnumerable<Responsible> availableResponsibles)
        {
            InitializeComponent();
            ResponsiblesList.ItemsSource = availableResponsibles.ToList();
        }

        private void Select_Click(object sender, RoutedEventArgs e)
        {
            SelectedResponsible = ResponsiblesList.SelectedItem as Responsible;
            DialogResult = true;
            Close();
        }
    }
}
