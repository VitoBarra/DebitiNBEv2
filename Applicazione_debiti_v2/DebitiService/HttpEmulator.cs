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
using d = debitiNBEService.HttpEmulator.Databaseinfo;


namespace debitiNBEService
{

    static class HttpEmulator
    {

        static public class Databaseinfo
        {
            static public readonly string databaseName = "debiti";

            static public readonly string CONNECTION_STR = $"server=localhost;database={databaseName};uid=root;pwd=toor";

            static public readonly string[] UserTable = { "ID", "Email", "Username", "Password", "Name", "Lastname" };
            static public readonly string[] FriendListTable = { "user_ID", "user_ID1", "Stato" };
            static public readonly string[] RequestTable = { "ID", "ID_mandante", "ID_ricevente", "Credito", "Stato" };
            static public readonly string[] PaymentRequestTable = { "ID", "Request_ID", "Credito", "Stato" };
        }


        public static void DatabaseConnection() => MySqlTool.StrConnection = Databaseinfo.CONNECTION_STR;



        public static void DebugFunction()
        {


        }



        public static string GetLoginUser(string username, string password)
        {
            MySqlReaderTool mySqlReader = new MySqlReaderTool();
            UserData ud = new UserData();


            string[] Column = new string[] { d.UserTable[0], d.UserTable[3] };
            mySqlReader.ReadFrom(Column, $"{d.databaseName}.user", $"{d.UserTable[2]} ='{username}' AND {d.UserTable[3]} = '{password}'");


            if (mySqlReader.reader.Read())
            {
                ud.ID = mySqlReader.reader.GetInt32(Column[0]);
                ud.Username = username;

                return JsonConvert.SerializeObject(ud, Formatting.Indented);
            }
            else
                return null;
        }



        public static void PostFriendRequest(int id, string username)
        {
            MySqlReaderTool mySqlReader = new MySqlReaderTool();


            string Column = d.UserTable[0];

            mySqlReader.ReadFrom(Column, $"{d.databaseName}.user", $"{d.UserTable[2]}={username}");

            if (mySqlReader.reader.Read())
                MySqlWriterTool.ExecuteInsert($"{d.databaseName}.user", "", $"{id},{mySqlReader.reader.GetInt32(Column)}");

        }


        public static string GetReqeust(int id)
        {
            MySqlReaderTool mySqlReader = new MySqlReaderTool();

            string[] Column = new string[] { d.RequestTable[1], d.RequestTable[2], d.RequestTable[3], d.RequestTable[4] };

            List<RequestData> requestDatas = new List<RequestData>();

            mySqlReader.ReadFrom(Column, $"{d.databaseName}.request", $"{d.RequestTable[1]} ='{id}' OR  {d.RequestTable[2]} ='{id}'");


            while (mySqlReader.reader.Read())
            {

                RequestData rd = new RequestData();



                rd.ID_mandante = mySqlReader.reader.GetInt32(Column[0]);
                rd.ID_ricevente = mySqlReader.reader.GetInt32(Column[1]);

                if (rd.ID_mandante == id)
                    rd.Username_Interaction = GetUserName(rd.ID_ricevente.ToString());
                else
                    rd.Username_Interaction = GetUserName(rd.ID_mandante.ToString());


                rd.Credito = mySqlReader.reader.GetDouble(Column[2]);

                rd.Stato = (RequestData.State)Enum.Parse(typeof(RequestData.State), mySqlReader.reader.GetString(Column[3]));


                requestDatas.Add(rd);
            }

            return JsonConvert.SerializeObject(requestDatas, Formatting.Indented);


        }

        #region Rest
        
        private static string GetUserName(string id)
        {
            MySqlReaderTool mySqlReaderTool = new MySqlReaderTool();

            string Column =  d.UserTable[2] ;

            mySqlReaderTool.ReadFrom(Column,$"{d.databaseName}.user",$"{d.UserTable[0]} ={id}");
            if (mySqlReaderTool.reader.Read())
                return mySqlReaderTool.reader.GetString(Column);
            else
                return null;
        }

        #endregion


    }
}
