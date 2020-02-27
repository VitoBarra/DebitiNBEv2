using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using debitiNBEService;
using Newtonsoft.Json;

namespace AppDebitiV2
{
    public interface IUserData
    {
        [JsonProperty("username")]
        string UserName { get; set; }

        [JsonProperty("ID_user")]
        int ID_User { get; set; }
    }

    public class UserData : IUserData
    {
        string IUserData.UserName { get; set; }
        int IUserData.ID_User { get; set; }

       public IList<IRequestData> AcceptedCrediti ;
       public IList<IRequestData> AcceptedDebiti;


        public UserData(string UserName, string Password)
        {
           // JsonConvert.DeserializeObject<UserData>(HttpEmulator.GetUserData(UserName, Password));
        }
    }


    public interface IRequestData
    {
        [JsonProperty("ID_Request")]
        int ID_Request { get; set; }
        [JsonProperty("ID_Mandante")]
        int ID_Mandante { get; set; }
        [JsonProperty("User")]
        string User { get; set; }
        [JsonProperty("Importo")]
        float Importo { get; set; }

        [JsonProperty("ID_SaldoRequest")]
        int ID_SaldoRequest { get; set; }
        [JsonProperty("CreditoRichistaSaldo")]
        float CreditoRichistaSaldo { get; set; }

    }

}
