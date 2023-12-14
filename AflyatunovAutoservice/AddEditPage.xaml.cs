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

        private Posishen _currentServices = new Posishen();

        public AddEditPage(Posishen SelectedService)
        {
            InitializeComponent();

            if (SelectedService != null)
            {
                this._currentServices = SelectedService;
            }


            DataContext = _currentServices;

            var _currentPosishen = shavaEntities.GetContext().Posishen.ToList();

            ComboProd.ItemsSource = _currentPosishen;
        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (ComboProd.SelectedItem == null)
                errors.AppendLine("Укажите продукт");

            if(errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            


        }
    }
}
