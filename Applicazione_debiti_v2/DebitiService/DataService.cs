using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace debitiNBEService
{

    public class UserData
    {

        [JsonProperty("ID")]
        public int ID { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Lastname")]
        public string Lastname { get; set; }

    }


    public class RequestData
    {
        public enum State {active, accepted, dennied }

        [JsonProperty("ID_mandante")]
        public int ID_mandante { get; set; }

        [JsonProperty("ID_ricevente")]
        public int ID_ricevente { get; set; }

        [JsonProperty("Credito")]
        public double Credito { get; set; }

        [JsonProperty("Stato")]
        public State Stato { get; set; }
    }



    enum FriendRequest { active, accepted, ignored, blocked }
 


}
