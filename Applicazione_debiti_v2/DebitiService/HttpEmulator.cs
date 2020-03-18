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
        public static string databaseName = "debiti";

        static public readonly string CONNECTION_STR = $"server=localhost;database={databaseName};uid=root;pwd=toor";
    }

    static class HttpEmulator
    {
        private static string[] UserTable = {"ID", "Email", "Username", "Password", "Name", "Lastname" };
        private static string[] FriendListTable = { "user_ID", "user_ID1", "Stato"};

        private static string[] RequestTable = {"ID", "ID_mandante", "ID_ricevente", "Credito", "Stato" };
        private static string[] PaymentRequestTable = {"ID", "Request_ID", "Credito", "Stato"};


        public static void DatabaseConnection() => MySqlTool.StrConnection = Global.CONNECTION_STR;



        public static void DebugFunction()
        {


        }



        public static string GetLoginUser(string username, string password)
        {
            MySqlReaderTool mySqlReader = new MySqlReaderTool();
            UserData ud = new UserData();


            string[] Column = new string[] { UserTable[0], UserTable[3] };
            mySqlReader.ReadFrom(Column, $"{Global.databaseName}.user", $"Username ='{username}' AND Password = '{password}'");


            if (mySqlReader.reader.Read())
            {
                ud.ID = mySqlReader.reader.GetInt32(Column[0]);
                ud.UserName = username;

                return JsonConvert.SerializeObject(ud, Formatting.Indented);
            }
            else
                return null;
        }



        public static void PostFriendRequest(int id, string username)
        {
            MySqlReaderTool mySqlReader = new MySqlReaderTool();


            string Column = UserTable[0];

            mySqlReader.ReadFrom(Column, $"{Global.databaseName}.user", $"Username={username}");

            if (mySqlReader.reader.Read())
                MySqlWriterTool.ExecuteInsert($"{Global.databaseName}.user", "", $"{id},{mySqlReader.reader.GetInt32(Column)}");

        }




    }
}
