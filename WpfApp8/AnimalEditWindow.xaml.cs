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
    /// Логика взаимодействия для AnimalEditWindow.xaml
    /// </summary>
    public partial class AnimalEditWindow : Window
    {
        public Animal Animal { get; private set; }
        public List<Trainer> Trainers { get; }
        public List<string> Genders { get; } = new List<string> { "Мужской", "Женский" };

        public AnimalEditWindow() 
        {
            InitializeComponent();
            Animal = new Animal();
            using (var db = new circusEntities())
            {
                Trainers = db.Trainer.ToList();
            }
            DataContext = this;
        }
        public AnimalEditWindow(Animal animal) : this()
        {
            Animal = new Animal
            {
                animal_id = animal.animal_id,
                name = animal.name,
                age = animal.age,
                gender = animal.gender,
                weight = animal.weight,
                food = animal.food,
                care = animal.care,
                trainer_id = animal.trainer_id
            };
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
