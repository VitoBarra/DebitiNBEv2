using AppDebitiV2.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AppDebitiV2.ViewModels
{
    public class ViewModelMain : BaseViewModel
    {

        private UserDataViewModels _LoggedUserdata;

        public IUserData LoggedUserdata
        {
            get => _LoggedUserdata;
            set
            {
                _LoggedUserdata = (UserDataViewModels)value;
                Notify();
            }
        }


        private bool _buttonMenuVisibility;
        public bool ButtonMenuVisibility
        {
            get => _buttonMenuVisibility;
            set 
            { 
                _buttonMenuVisibility = value;
                Notify(); }

        }


        private bool _buttonArrowvisibility ;
        public bool ButtonArrowVisibility
        {
            get => _buttonArrowvisibility;
            set 
            {
                _buttonArrowvisibility = value; 
                Notify(); 
            }
        }



        private float _creditValue = 0f;

        public string CreditValue
        {
            get => _creditValue.ToString();

            set
            {
                _creditValue = float.Parse(value);
                Notify();
            }
        }


        private float _debitValue =0f;

        public string DebitValue
        {
            get => _debitValue.ToString();

            set
            {
                _debitValue = float.Parse( value);
                Notify();
            }
        }



        public RelayCommand OpenFirendList { get; private set; }
        public RelayCommand OpenLookRequest { get; private set; }
        public RelayCommand OpenAddRequest { get; private set; }
        public RelayCommand OpenPaga { get; private set; }

        public RelayCommand OpenMenu { get; private set; }



        public ViewModelMain() 
        {
            CreditValue = "0";
            DebitValue = "0";

            ButtonMenuVisibility = true;
            ButtonArrowVisibility = false;



            OpenMenu = new RelayCommand(para => { ButtonMenuVisibility = !ButtonMenuVisibility; ButtonArrowVisibility = !ButtonArrowVisibility; }); 
        }


        #region OpenWindows

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

        #endregion

    }
}
