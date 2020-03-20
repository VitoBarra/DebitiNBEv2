using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using debitiNBEService;
using Newtonsoft.Json;
using AppDebitiV2.ViewModels;
using System.Collections.ObjectModel;

namespace AppDebitiV2
{
    public class UserData
    {
        public int _ID;
        public string _Email;
        public string _Username;
        public string _Name;
        public string _Lastname;
    }

    public class UserDataViewModels : BaseViewModel
    {

        UserData userData = new UserData();

        [JsonProperty("ID")]
        public int ID { get => userData._ID; set { userData._ID = value; Notify(); } }

        [JsonProperty("Email")]
        public string Email { get => userData._Email; set { userData._Email = value; Notify(); } }

        [JsonProperty("username")]
        public string UserName { get => userData._Username; set { userData._Username = value; Notify(); } }


        [JsonProperty("Name")]
        public string _Name { get => userData._Name; set { userData._Name = value; Notify(); } }

        [JsonProperty("Lastname")]
        public string _Lastname { get => userData._Lastname; set { userData._Lastname = value; Notify(); } }



        private IList<RequestData> _WaitingRequest;
        public IList<RequestData> WaitingRequest { get => _WaitingRequest; set { _WaitingRequest = new ObservableCollection<RequestData>(value); Notify(); } }

        private IList<RequestData> _AcceptedRequest;
        public IList<RequestData> AcceptedRequest { get => _AcceptedRequest; set { _AcceptedRequest = new ObservableCollection<RequestData>(value); Notify(); } }

        private IList<RequestData> _DenniedRequest;
        public IList<RequestData> DenniedRequest { get => _DenniedRequest; set { _DenniedRequest = new ObservableCollection<RequestData>(value); Notify(); } }

        private IList<RequestData> _CompletedRequest;
        public IList<RequestData> CompletedRequest { get => _CompletedRequest; set { _CompletedRequest = new ObservableCollection<RequestData>(value); Notify(); } }


    }


    public class RequestData
    {
        public enum State { waiting, accepted, dennied, completed }

        [JsonProperty("ID_mandante")]
        public int ID_mandante { get; set; }


        [JsonProperty("ID_ricevente")]
        public int ID_ricevente { get; set; }

        [JsonProperty("Username_Interaction")]
        public string Username_Interaction { get; set; }


        [JsonProperty("Credito")]
        public double Credito { get; set; }

        [JsonProperty("Stato")]
        public State Stato { get; set; }
    }

}
