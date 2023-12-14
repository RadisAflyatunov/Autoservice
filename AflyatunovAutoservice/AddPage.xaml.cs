using Microsoft.Win32;
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
    /// Логика взаимодействия для AddPage.xaml
    /// </summary>
    public partial class AddPage : Page
    {
        private Posishen _currentServices = new Posishen();

        public bool check = false;
        public AddPage(Posishen Selected)
        {
            InitializeComponent();


            DataContext = _currentServices;

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentServices.Name))
                errors.AppendLine("Укажите название услуги");

            if (_currentServices.Cost <= 0)
                errors.AppendLine("Укажите стоимость улсуги");


            if (string.IsNullOrWhiteSpace(_currentServices.WeightOrVolume.ToString()))
                errors.AppendLine("Укажите вес-объем");



            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            var allAgent = ShaurmaEntities.GetContext().Posishen.ToList();
            allAgent = allAgent.Where(p => p.Name == _currentServices.Name).ToList();

            var allServices = ShaurmaEntities.GetContext().Posishen.ToList();
            allServices = allServices.Where(p => p.Name == _currentServices.Name).ToList();

            if (allServices.Count == 0 || check == true)
            {
                if (_currentServices.ID == 0)
                    ShaurmaEntities.GetContext().Posishen.Add(_currentServices);

                try
                {
                    ShaurmaEntities.GetContext().SaveChanges();
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
        private void ChangePictureBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog myOpenFileDialog = new OpenFileDialog();
            if (myOpenFileDialog.ShowDialog() == true)
            {
                _currentServices.MainImagePath = myOpenFileDialog.FileName;
                LogoImage.Source = new BitmapImage(new Uri(myOpenFileDialog.FileName));
            }
        }
    }
}
