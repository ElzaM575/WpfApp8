using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp8.DBConnection;

namespace WpfApp8
{
    /// <summary>
    /// Логика взаимодействия для AnimalsPage.xaml
    /// </summary>
    public partial class AnimalsPage : Page
    {

        public AnimalsPage()
        {
            InitializeComponent();
            LoadGenders();
            LoadAnimals();
        }
        private void LoadGenders()
        {
            GenderFilter.ItemsSource = new Dictionary<string, string>
            {
                { "Мужской", "Мужской" },
                { "Женский", "Женский" }
            };
            GenderFilter.SelectedIndex = -1;
        }

        private void LoadAnimals()
        {
            using (var db = new circusEntities())
            {
                AnimalList.ItemsSource = db.Animal.Include("Trainer").ToList();
            }
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new circusEntities())
            {
                var animals = db.Animal.Include("Trainer").ToList();
                AnimalList.ItemsSource = animals;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new circusEntities())
            {
                var query = db.Animal.Include("Trainer").AsQueryable();

                if (GenderFilter.SelectedValue != null)
                {
                    query = query.Where(a => a.gender == GenderFilter.SelectedValue.ToString());
                }

                if (int.TryParse(AgeFilter.Text, out int age))
                {
                    query = query.Where(a => a.age == age);
                }

                AnimalList.ItemsSource = query.ToList();
            }
        }




        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (AnimalList.SelectedItem is Animal selectedAnimal)
            {
                var editWindow = new AnimalEditWindow(selectedAnimal);
                if (editWindow.ShowDialog() == true)
                {
                    using (var db = new circusEntities())
                    {
                        var animal = db.Animal.Find(editWindow.Animal.animal_id);
                        if (animal != null)
                        {
                            animal.name = editWindow.Animal.name;
                            animal.age = editWindow.Animal.age;
                            animal.gender = editWindow.Animal.gender;
                            animal.weight = editWindow.Animal.weight;
                            animal.food = editWindow.Animal.food;
                            animal.care = editWindow.Animal.care;
                            animal.trainer_id = editWindow.Animal.trainer_id;
                            db.SaveChanges();
                        }
                    }
                    LoadAnimals();
                }
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (AnimalList.SelectedItem is Animal selectedAnimal &&
                MessageBox.Show("Удалить это животное?", "Подтверждение",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                using (var db = new circusEntities())
                {
                    var animal = db.Animal.Find(selectedAnimal.animal_id);
                    if (animal != null)
                    {
                        db.Animal.Remove(animal);
                        db.SaveChanges();
                    }
                }
                LoadAnimals();
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            using (var db = new circusEntities())
            {
                var trainersWindow = new TrainersWindow(db.Trainer.ToList());
                trainersWindow.ShowDialog();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var editWindow = new AnimalEditWindow();
            if (editWindow.ShowDialog() == true)
            {
                using (var db = new circusEntities())
                {
                    db.Animal.Add(editWindow.Animal);
                    db.SaveChanges();
                }
                LoadAnimals();
            }
        }
    }
}
