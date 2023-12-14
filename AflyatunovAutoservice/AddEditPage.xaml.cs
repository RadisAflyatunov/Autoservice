using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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

        private Posishen _currentServices = new Posishen();
        public AddEditPage(Posishen SelectedService)
        {
            InitializeComponent();
            if (SelectedService != null)
            {
                this._currentServices = SelectedService;
            }
            var _currentPos = ShaurmaEntities.GetContext().Posishen.ToList();

            ComboClient.ItemsSource = _currentPos;
        }


        private void AddSale_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage(_currentServices as Posishen));
        }

        private void BackSale_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage((sender as Button).DataContext as Posishen));
        }

        private void AddProdHistoryBtn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();


            if (string.IsNullOrWhiteSpace(ProdCount.Text))
            {
                errors.AppendLine("Укажите количество");
            }
            else
            {
                int c = Convert.ToInt32(ProdCount.Text);
                if (c < 1)
                    errors.AppendLine("Укажите количество");
            }

            if (string.IsNullOrWhiteSpace(Countе.Text))
            {
                errors.AppendLine("Укажите номер");
            }
            else
            {
                int c = Convert.ToInt32(Countе.Text);
                if (c < 1)
                    errors.AppendLine("Укажите номер");
            }
           
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (errors.Length == 0)
            {
                try
                {
                    ShaurmaEntities.GetContext().SaveChanges();
                    MessageBox.Show("информация сохранена");
                    Manager.MainFrame.GoBack();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void ComboProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

