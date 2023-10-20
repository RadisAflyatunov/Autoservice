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
    /// Логика взаимодействия для ServicePage.xaml
    /// </summary>
    public partial class ServicePage : Page
    {
        public ServicePage()
        {
            InitializeComponent();

            var currentServices = AflyatunovAutoserviceEntities.GetContext().Service.ToList();

            ServiceListView.ItemsSource = currentServices;

            ComboType.SelectedIndex = 0;

            UpdatesServices();
        }

        private void UpdatesServices()
        {
            var currentServices = AflyatunovAutoserviceEntities.GetContext().Service.ToList();

            if(ComboType.SelectedIndex == 0)
            {
                currentServices = currentServices.Where(p => (p.Current_discount >= 0 && p.Current_discount <= 100)).ToList();
            }
            if (ComboType.SelectedIndex == 1)
            {
                currentServices = currentServices.Where(p => (p.Current_discount >= 0 && p.Current_discount < 5 )).ToList();
            }
            if (ComboType.SelectedIndex == 2)
            {
                currentServices = currentServices.Where(p => (p.Current_discount >= 5 && p.Current_discount < 15)).ToList();
            }
            if (ComboType.SelectedIndex == 3)
            {
                currentServices = currentServices.Where(p => (p.Current_discount >= 15 && p.Current_discount < 30)).ToList();
            }
            if (ComboType.SelectedIndex == 4)
            {
                currentServices = currentServices.Where(p => (p.Current_discount >= 30 && p.Current_discount < 70)).ToList();
            }
            if (ComboType.SelectedIndex == 5)
            {
                currentServices = currentServices.Where(p => (p.Current_discount >= 70 && p.Current_discount < 100)).ToList();
            }

            currentServices = currentServices.Where(p => p.Name_of_the_service.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            
            if(RButtonDown.IsChecked.Value)
            {
                currentServices = currentServices.OrderByDescending(p => p.Cost).ToList();
            }
            if(RButtonUp.IsChecked.Value)
            {
                currentServices = currentServices.OrderBy(p => p.Cost).ToList();
            }

            ServiceListView.ItemsSource = currentServices;

        }

        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage(null));
    
        }

        private void RButtonUp_Checked(object sender, RoutedEventArgs e)
        {
            UpdatesServices();
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdatesServices();
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdatesServices();
        }

        private void RButtonDown_Checked(object sender, RoutedEventArgs e)
        {
            UpdatesServices();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage(null));
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage((sender as Button).DataContext as Service));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(Visibility == Visibility.Visible)
            {
                AflyatunovAutoserviceEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                ServiceListView.ItemsSource = AflyatunovAutoserviceEntities.GetContext().Service.ToList();
            }
        }
    }
}
