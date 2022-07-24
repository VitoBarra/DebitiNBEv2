using AppDebitiV2.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppDebitiV2.DebitiService
{
    class RestService
    {
        public static HttpClient client = new HttpClient();



        public static async Task<UserData> Service()
        {
            UserData Data = null;
            string EndPoint = "MY_ENDPOINT";
            var parameters = new Dictionary<string, string>() { };

            try
            {
                HttpResponseMessage response = await client.PostAsync(EndPoint, new FormUrlEncodedContent(parameters));
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<UserData>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("\tERROR {0}", ex.Message);
            }

            return Data;
        }


    }
}
