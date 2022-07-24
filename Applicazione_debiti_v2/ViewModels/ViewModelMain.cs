using AppDebitiV2.Models;
using AppDebitiV2.Views;
using debitiNBEService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using RequestData = AppDebitiV2.Models.RequestData;

namespace AppDebitiV2.ViewModels
{
    public class ViewModelMain : BaseViewModel
    {
        #region Databinding

        private UserDataViewModels _LoggedUserdata;

        public UserDataViewModels LoggedUserdata
        {
            get => _LoggedUserdata;
            set
            {
                _LoggedUserdata = value;
                Notify();
            }
        }

        private double _CreditVale = 0;
        public double CreditValue
        {
            get => _CreditVale;

            set { _CreditVale = value; Notify(); }
        }
        private double _DebitVale = 0;
        public double DebitValue
        {
            get => _DebitVale;
  
            set { _DebitVale = value; Notify(); }
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

        #endregion


        #region Command
        public RelayCommand OpenFirendList { get; private set; }
        public RelayCommand OpenLookRequest { get; private set; }
        public RelayCommand OpenAddRequest { get; private set; }
        public RelayCommand OpenPaga { get; private set; }

        public RelayCommand OpenMenu { get; private set; }

        #endregion

        public ViewModelMain() 
        {


            ButtonMenuVisibility = true;
            ButtonArrowVisibility = false;



            OpenMenu = new RelayCommand(para => { ButtonMenuVisibility = !ButtonMenuVisibility; ButtonArrowVisibility = !ButtonArrowVisibility; }); 
        }


        #region OpenWindows

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
            DebitInfo paga = new DebitInfo();
            paga.ShowDialog();
        }

        #endregion


        private void DebCredValeu()
        {
            double credit = 0;
            double debit = 0;
            foreach (RequestData rq in _LoggedUserdata.AcceptedRequest)
            {
                if (rq.Credito > 0)
                    credit += rq.Credito;
                else
                    debit += rq.Credito;
            }

            CreditValue = credit;
            DebitValue = debit;

   
        }

        public void LoginUser(string LoginStr)
        {
            LoggedUserdata = JsonConvert.DeserializeObject<UserDataViewModels>(LoginStr);
            RequestData[] tempReq = JsonConvert.DeserializeObject<RequestData[]>(HttpEmulator.GetReqeust(LoggedUserdata.ID));


            List<RequestData> waiting = new List<RequestData>();
            List<RequestData> accepted = new List<RequestData>();
            List<RequestData> dennied = new List<RequestData>();
            List<RequestData> completed = new List<RequestData>();

            foreach (RequestData rd in tempReq)
            {
                if (LoggedUserdata.ID == rd.ID_ricevente)
                    rd.Credito = -rd.Credito;

                switch (rd.Stato)
                {
                    case RequestData.State.waiting:
                      
                        waiting.Add(rd);
                        break;
                    case RequestData.State.accepted:
                        accepted.Add(rd);
                        break;
                    case RequestData.State.dennied:
                        dennied.Add(rd);
                        break;
                    case RequestData.State.completed:
                        completed.Add(rd);
                        break;
                }
            }

            LoggedUserdata.WaitingRequest = waiting;
            LoggedUserdata.AcceptedRequest = accepted;
            LoggedUserdata.DenniedRequest = dennied;
            LoggedUserdata.CompletedRequest = completed;

            DebCredValeu();
        }
    }
}
