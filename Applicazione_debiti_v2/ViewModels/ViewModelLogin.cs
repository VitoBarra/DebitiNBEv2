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
    public class ViewModelLogin : BaseViewModel
    {
        #region BindingProp
        private string _username;
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
        #endregion


        public  string UserDataStr = null;

        #region BindingComand
        public RelayCommand LoginCommand { get; private set; }
        public RelayCommand SingUpCommand { get; private set; }
        public RelayCommand ForgottenPasswordCommand { get; private set; }
        public RelayCommand CloseWind { get; private set; }
        #endregion



        public ViewModelLogin()
        {
            LoginCommand = new RelayCommand(
                          action: para =>
                          {
                              UserDataStr = HttpEmulator.GetLoginUser(Username, Password);
                              if (UserDataStr != null)
                              {
                                  Password = null;
                                  Global.loggedState = true;
                                  (para as Window).Close();
                              }
                              else
                                  MessageBox.Show("nome utente e/o password errate","ERRORE");

                          },
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
