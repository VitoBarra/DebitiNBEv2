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



        private IList<RequestData> _Request;
        public IList<RequestData> Request { get => _Request; set { _Request = new ObservableCollection<RequestData>(value); Notify(); } }




    }


    public class RequestData
    {
        [JsonProperty("ID_mandante")]
        int ID_mandante { get; set; }

        [JsonProperty("ID_ricevente")]
        int ID_ricevente { get; set; }

        [JsonProperty("Credito")]
        float Credito { get; set; }

        [JsonProperty("Stato")]
        int Stato { get; set; }
    }

}
