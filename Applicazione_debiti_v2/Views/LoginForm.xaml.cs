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
using debitiNBEService;
using Newtonsoft.Json;

namespace AppDebitiV2
{
    /// <summary>
    /// Logica di interazione per Window1.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        public LoginForm()
        {
            InitializeComponent();
            HttpEmulator.DatabaseConnection();
        }

        private void LoginButton(object sender, RoutedEventArgs e)
        {
            HttpEmulator.DebugFunction();

            //Global.UserData = JsonConvert.DeserializeObject<IUserData>(HttpEmulator.GetUserData(UserInput.Text, PasswordInput.Password));


            if (Global.UserData.ID_User != -1)
            {
                //Global.UserData.AcceptedCrediti = new List<IRequestData>();
                //Global.UserData.AcceptedDebiti = new List<IRequestData>();
                //Global.loggedState = true;
                Close();
            }
            else
            {
                MessageBox.Show("la combinaione di utente e password non corisponde a nessun utente");
            }


        }

        private void SignUpButton(object sender, RoutedEventArgs e)
        {

        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {

        }
    }
}
