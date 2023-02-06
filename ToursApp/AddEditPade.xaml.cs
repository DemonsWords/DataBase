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

namespace ToursApp
{
    /// <summary>
    /// Логика взаимодействия для AddEditPade.xaml
    /// </summary>
    public partial class AddEditPade : Page
    {
        private readonly Hotel _currentHotel = new Hotel();
        public AddEditPade(Hotel selectedHotel)
        {
            InitializeComponent();

            if (selectedHotel != null)
                _currentHotel = selectedHotel;

            DataContext = _currentHotel;
            ComboCountries.ItemsSource = ToursBaseEntities1.GetContext().Country.ToList();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentHotel.Name))
                errors.AppendLine("Укажите имя преподавателя");
            if (_currentHotel.CountOfStars < 1 || _currentHotel.CountOfStars > 5)
                errors.AppendLine("Класс - число от 1 до 5");
            if (_currentHotel.Country == null)
                errors.AppendLine("Выберите предмет");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentHotel.id == 0)
                ToursBaseEntities1.GetContext().Hotel.Add(_currentHotel);
            try
            {
                ToursBaseEntities1.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена!");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }
    }
}
