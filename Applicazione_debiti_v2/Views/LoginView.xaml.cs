using AppDebitiV2.ViewModels;
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
using System.Windows.Shapes;


namespace AppDebitiV2
{
    /// <summary>
    /// Logica di interazione per Window1.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        ViewModelLoginForm vm;

        public LoginView(ViewModelLoginForm _vm)
        {
            InitializeComponent();
            this.DataContext = _vm;

            vm = _vm;
        }



        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (vm.LoginCommand.CanExecute(PasswordInput))
                vm.LoginCommand.Execute(PasswordInput);

            this.Close();

        }

        private void PasswordInput_PasswordChanged(object sender, RoutedEventArgs e)
        {
            vm.Password = PasswordInput.Password;
        }
    }
}
