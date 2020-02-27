using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using MyTool.MySql;
using MySqlReaderTool = MyTool.MySql.MySqlTool.MySqlReaderTool;
using MySqlWriterTool = MyTool.MySql.MySqlTool.MySqlWriterTool;



namespace debitiNBEService
{

    static class Global
    {
        static public readonly string CONNECTION_STR = "server=localhost;database=debiti;uid=root;pwd=toor";
    }

    static class HttpEmulator
    {
        public static void DatabaseConnection() => MySqlTool.StrConnection = Global.CONNECTION_STR;



        public static void DebugFunction()
        {


        }



        public static string GetUserList()
        {
            List<UserData> usl = new List<UserData>();

            MySqlReaderTool mySqlReader = new MySqlReaderTool();

            mySqlReader.ReadFrom("ID_user,user", "debiti.user");
            while (mySqlReader.reader.Read())
            {
                UserData us = new UserData()
                {
                    ID_User = (int)mySqlReader.reader[0],
                    UserName = mySqlReader.reader[1].ToString()
                };
                usl.Add(us);
            }

            if (usl.Count > 0)
                return JsonConvert.SerializeObject(usl, Formatting.Indented);
            else
                return null;

        }



        public static string GetUserData(string UserName, string CodePassword)
        {
            UserData us = new UserData();

            MySqlReaderTool mySqlReader = new MySqlReaderTool();

            string[] str = new string[3] { "ID", "Username", "Password " };

            mySqlReader.ReadFrom(Stringnator(str), "debiti.user", "Username = '" + UserName + "' and password = '" + CodePassword + "'");

            if (mySqlReader.reader.Read())
            {
                us = new UserData()
                {
                    ID_User = mySqlReader.reader.GetInt32(str[0]),
                    UserName = mySqlReader.reader.GetString(str[1])
                };
            }


            return JsonConvert.SerializeObject(us, Formatting.Indented);
        }




        private static string GetUserNamedebit(int ID)
        {
            MySqlReaderTool mySqlReader = new MySqlReaderTool();

            mySqlReader.ReadFrom("user", "debiti.user", "ID_user =" + ID);

            if (mySqlReader.reader.Read())
                return mySqlReader.reader[0].ToString();
            else
                return null;
        }

        public static string GetRequestList(string ID_user)
        {
            List<RequestData> request = new List<RequestData>();

            MySqlReaderTool mySqlReader = new MySqlReaderTool();

            mySqlReader.ReadFrom("*", "debiti.request", "KS_ID_user_ricevente=" + ID_user + " and accepted = 0");


            while (mySqlReader.reader.Read())
            {



                RequestData rd = new RequestData() //instanzio un oggetto requestdata
                {
                    ID_Request = (int)mySqlReader.reader[0],
                    ID_Mandante = (int)mySqlReader.reader[1],
                    User = GetUserNamedebit((int)mySqlReader.reader[1]),
                    Importo = (double.Parse(mySqlReader.reader[3].ToString()))
                };

                request.Add(rd);//aggiungo l oggetto alla lista
            }


            if (request.Count > 0)
                return JsonConvert.SerializeObject(request, Formatting.Indented);
            else
                return null;
        }

        public static string GetReqestSaldoList(string ID_user)
        {

            List<RequestData> request = new List<RequestData>();


            MySqlReaderTool PayRequest = new MySqlReaderTool();
            PayRequest.ReadFrom("*", "debiti.payrequest");

            while (PayRequest.reader.Read())
            {

                MySqlReaderTool Request = new MySqlReaderTool();
                Request.ReadFrom("credito,KS_ID_user_ricevente", "debiti.request", "ID_Request =" + PayRequest.reader[1] + " AND KS_ID_user_mandante = " + ID_user);
                while (Request.reader.Read())
                {

                    RequestData srd = new RequestData()
                    {
                        ID_SaldoRequest = (int)PayRequest.reader[0],
                        ID_Mandante = int.Parse(ID_user),
                        User = GetUserNamedebit((int)Request.reader[1]),
                        ID_Request = (int)PayRequest.reader[1],
                        Importo = double.Parse(PayRequest.reader[2].ToString()),

                    };

                    request.Add(srd);
                }


            }
            if (request.Count > 0)
                return JsonConvert.SerializeObject(request, Formatting.Indented);
            else
                return null;
        }

        public static string GetAccepted(string ID_User)
        {
            List<RequestData> AcceptedRequest = new List<RequestData>();


            MySqlReaderTool Request = new MySqlReaderTool();
            Request.ReadFrom("*", "debiti.request", "KS_ID_user_ricevente=" + ID_User + " and accepted = 1");


            while (Request.reader.Read())
            {
                RequestData rd = new RequestData() //instanzio un oggetto requestdata
                {
                    ID_Mandante = (int)Request.reader[1],
                    User = GetUserNamedebit((int)Request.reader[1]),
                    ID_Request = (int)Request.reader[0],
                    Importo = float.Parse(Request.reader[3].ToString())
                };

                AcceptedRequest.Add(rd);

            }

            if (AcceptedRequest.Count > 0)
                return JsonConvert.SerializeObject(AcceptedRequest, Formatting.Indented);
            else
                return null;

        }

        public static string GetSaldiRequest(string ID_request)
        {
            double SaldatoParzialeR = 0;
            MySqlReaderTool sqlReader = new MySqlReaderTool();
            sqlReader.ReadFrom("credito", "debiti.payrequest", "ID_Request = " + ID_request);
            while (sqlReader.reader.Read())
                SaldatoParzialeR += double.Parse(sqlReader.reader[0].ToString());
            sqlReader.reader.Close();

            return SaldatoParzialeR.ToString();
        }
        public static bool PostRequest(string Ricevente_userName, string ID_mandante, string isChecked, string Importo)
        {
            int ID_rivcevente = -1;
            MySqlReaderTool sqlReader = new MySqlReaderTool();
            sqlReader.ReadFrom("ID_user", "debiti.user", "user = '" + Ricevente_userName + "'");
            if (sqlReader.reader.Read())
                ID_rivcevente = (int)sqlReader.reader[0];

            if (ID_rivcevente >= 0)
            {
                MySqlWriterTool.ExecuteInsert(" debiti.request",  "KS_ID_user_mandante, KS_ID_user_ricevente, credito ",  ID_mandante + "," + ID_rivcevente + "," + ((bool.Parse(isChecked) ? Importo : "-" + Importo)));
                return true;
            }
            else
                return false;
        }
        public static void PostSaldo(string ID_request, string Saldo)
        {

            double SaldatoParzialeR = double.Parse(GetSaldiRequest(ID_request));
            double debito = 0;
            MySqlReaderTool sqlReader = new MySqlReaderTool();



            sqlReader.ReadFrom("credito", "debiti.request", "ID_Request = " + ID_request);
            if (sqlReader.reader.Read())
                debito = double.Parse(sqlReader.reader[0].ToString());
            sqlReader.reader.Close();



            if ((SaldatoParzialeR + double.Parse(Saldo)) <= debito)
                MySqlWriterTool.ExecuteInsert("debiti.payrequest","ID_request, credito",  ID_request + " , " + Saldo);



        }

        public static void PutRequest(string ID_PayRequest)
        {
            MySqlWriterTool.ExecuteUpdate(" debiti.request", " accepted = 1", " ID_Request = " + ID_PayRequest);
        }

        public static void PutSaldoRequest(string ID_PayRequest)
        {
            MySqlReaderTool sqlreadepay = new MySqlReaderTool();
            MySqlReaderTool sqlreadeRequest = new MySqlReaderTool();

            sqlreadepay.ReadFrom("ID_request,credito", "debiti.payrequest", "ID_PayRequest =" + ID_PayRequest);
            if (sqlreadepay.reader.Read())
            {
                sqlreadeRequest.ReadFrom("credito", "debiti.request", "ID_Request = " + sqlreadepay.reader[0].ToString());

                if (sqlreadeRequest.reader.Read())
                {
                    MySqlWriterTool.ExecuteUpdate(" debiti.request", " credito =" + (double.Parse(sqlreadeRequest.reader[0].ToString()) - double.Parse(sqlreadepay.reader[1].ToString())) , "ID_Request = " + (int)sqlreadepay.reader[0]);


                    DeleteSaldoRequest(ID_PayRequest);
                    if ((double.Parse(sqlreadeRequest.reader[0].ToString()) - double.Parse(sqlreadepay.reader[1].ToString())) <= 0)
                        DeleteRequest(sqlreadepay.reader[0].ToString());
                }
            }
        }
        public static void DeleteRequest(string ID_Request)
        {
            MySqlWriterTool.ExecuteDelete(" debiti.request", " ID_Request = " + ID_Request);
        }

        public static void DeleteSaldoRequest(string ID_PayRequest)
        {
            MySqlWriterTool.ExecuteDelete("debiti.payrequest"," ID_PayRequest = " + ID_PayRequest);
        }



        public static string Stringnator(string[] str)
        {
            string s="";
            for (int i = 0; i < str.Length; i++)
            {
                if (i == str.Length - 1)
                    s += str[i];
                else
                    s += str[i] + ",";
            }

            return s;
        }



    }
}
