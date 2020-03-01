using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using debitiNBEService;
using Newtonsoft;
using Newtonsoft.Json;

namespace AppDebitiV2.ViewModels
{
    public class ViewModelLoginForm: BaseViewModel
    {
        public RelayCommand LoginCommand { get; private set; }
        public RelayCommand SingUpCommand { get; private set; }
        public RelayCommand ForgottenPasswordCommand { get; private set; }
        public RelayCommand CloseWind { get; private set; }


        private string  _username;
        public string Username 
        {
            get 
            {
                return _username;
            }
            set 
            {
                _username = value;
                Notify();
                LoginCommand.RaiseCanExecuteChanged();
            } 
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                Notify();
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        public ViewModelLoginForm()
        {
            LoginCommand = new RelayCommand(
                          action: para => { Global.UserData = JsonConvert.DeserializeObject<UserData>(HttpEmulator.GetLoginUser(Username, Password)); Password = null; (para as Window).Close(); },
                          predicate: para => { return !(string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password)); });



            SingUpCommand = new RelayCommand(para => scasciat());
            ForgottenPasswordCommand = new RelayCommand(para => scasciat());

            ForgottenPasswordCommand.Execute(null);
        }






        void scasciat()
        {
            Debug.WriteLine("linea scassat");
        }


    }
}
