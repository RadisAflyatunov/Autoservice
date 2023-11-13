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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AflyatunovAutoservice
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {

        private Service _currentServices = new Service();

        public bool check = false;
        public AddEditPage(Service SelectedService)
        {
            InitializeComponent();

            if(SelectedService != null)
            {
                check = true;
                _currentServices = SelectedService;

            }

            DataContext = _currentServices;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentServices.Name_of_the_service))
                errors.AppendLine("Укажите название услуги");

            if (_currentServices.Cost <= 0)
                errors.AppendLine("Укажите стоимость улсуги");

            if (_currentServices.Current_discount < 0 || _currentServices.Current_discount > 100)
                errors.AppendLine("Не правильная скидка");

            if (_currentServices.Duration == 0 || string.IsNullOrWhiteSpace(_currentServices.Duration.ToString()))
                errors.AppendLine("Укажите длительность услуги");
            else
            {
                if (_currentServices.Duration > 240 || _currentServices.Duration < 1)
                    errors.AppendLine("Длительность не может быть больше 240 минут и меньше 1");
            }


            if (string.IsNullOrWhiteSpace(_currentServices.Current_discount.ToString()))
            {
                _currentServices.Current_discount = 0;
            }



            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            var allServices = AflyatunovAutoserviceEntities.GetContext().Service.ToList();
            allServices = allServices.Where(p => p.Name_of_the_service == _currentServices.Name_of_the_service).ToList();

            if (allServices.Count == 0 || check == true)
            {
                if (_currentServices.ID == 0)
                    AflyatunovAutoserviceEntities.GetContext().Service.Add(_currentServices);

                try
                {
                    AflyatunovAutoserviceEntities.GetContext().SaveChanges();
                    MessageBox.Show("Информация сохранена");
                    Manager.MainFrame.GoBack();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            else
                MessageBox.Show("Уже существует такая услуга");

        }
    }
}
