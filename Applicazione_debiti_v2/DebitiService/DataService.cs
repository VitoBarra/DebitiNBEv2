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
        [JsonProperty("username")]
        public string UserName { get; set; } = "not legged";

        [JsonProperty("ID_user")]
        public int ID_User { get; set; } = -1;
    }


    public class RequestData
    {
        [JsonProperty("ID_Request")]
        public int ID_Request { get; set; }
        [JsonProperty("ID_Mandante")]
        public int ID_Mandante { get; set; }
        [JsonProperty("User")]
        public string User { get; set; }
        [JsonProperty("Importo")]
        public double Importo { get; set; }
        [JsonProperty("ID_SaldoRequest")]
        public int ID_SaldoRequest { get; set; }
    }

}
