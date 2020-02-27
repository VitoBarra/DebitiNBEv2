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
using AppDebitiV2.Views;

namespace AppDebitiV2.Views
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        ViewModels.ViewModelMainWindow vm;


        public MainWindowView( )
        {
            InitializeComponent();



            //this.Hide();
            //LoginForm login = new LoginForm();
            //login.ShowDialog();
            //this.Show();

            GetViewModel();
        }

        public void GetViewModel() => vm = (ViewModels.ViewModelMainWindow)DataContext;

        private void AddRequest(object sender, RoutedEventArgs e)
        {

        }

        private void PagaRequest(object sender, RoutedEventArgs e)
        {

        }

        private void AccettaRequest(object sender, RoutedEventArgs e)
        {

        }

        private void CreditDetails(object sender, RoutedEventArgs e)
        {

        }

        private void DetailDebt(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            vm.MenuVisibility = true;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            vm.MenuVisibility = false;
        }

        private void ListViewItem_AddFriend(object sender, MouseButtonEventArgs e)
        {
            AddFriend request = new AddFriend();
            request.ShowDialog();
        }

        private void ListViewItem_LookRequest(object sender, MouseButtonEventArgs e)
        {

            Richieste richieste = new Richieste();
            richieste.ShowDialog();
        }
        private void ListViewItem_AddRequest(object sender, MouseButtonEventArgs e)
        {
            AddRichiesta request = new AddRichiesta();
            request.ShowDialog();
        }


        private void ListViewItem_Paga(object sender, MouseButtonEventArgs e)
        {
            Paga paga = new Paga();
            paga.ShowDialog();
        }
    }
}
