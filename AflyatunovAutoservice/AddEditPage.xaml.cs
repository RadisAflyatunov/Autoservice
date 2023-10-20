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

namespace AflyatunovAutoservice
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {

        private Service _currentServices = new Service();

        public AddEditPage(Service SelectedService)
        {
            InitializeComponent();

            if(SelectedService != null)
            {
                _currentServices = SelectedService;
            }

            DataContext = _currentServices;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if(string.IsNullOrWhiteSpace(_currentServices.Name_of_the_service))
            {
                errors.AppendLine("Укажите название услуги");
            }
            if(_currentServices.Cost==0)
            {
                errors.AppendLine("Укажите стоимость услуги");
            }

            if (string.IsNullOrWhiteSpace(_currentServices.Current_discount.ToString()))
            {
                _currentServices.Current_discount = 0;
            }

            if(_currentServices.Current_discount < 0 || _currentServices.Current_discount > 100)
            {
                errors.AppendLine("Укажите скидку");
            }
            if (string.IsNullOrWhiteSpace(_currentServices.Duration))
            {
                errors.AppendLine("Укажите длительность услуги");
            }
            if(errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if(_currentServices.ID == 0)
            {
                AflyatunovAutoserviceEntities.GetContext().Service.Add(_currentServices);
            }

            try
            {
                AflyatunovAutoserviceEntities.GetContext().SaveChanges();
                MessageBox.Show("информация сохранена");
                Manager.MainFrame.GoBack();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
