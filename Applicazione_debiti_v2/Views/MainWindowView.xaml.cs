using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using AppDebitiV2.ViewModels;
using debitiNBEService;
using Newtonsoft.Json;

namespace AppDebitiV2.Views
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
       public static ViewModelMain vm;


        public MainWindowView(ViewModelMain _vm)
        {
            InitializeComponent();
            vm = _vm;
            this.DataContext = vm;
            LoginView loginView= new LoginView(new ViewModelLogin());

            if(loginView.ShowDialog() != true)
            {
                vm.LoginUser((loginView.DataContext as ViewModelLogin).UserDataStr);
            }
        }

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


        private void ListViewItem_AddFriend(object sender, MouseButtonEventArgs e)
        {
            AddFriend request = new AddFriend(new ViewModelAddFriend());
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
