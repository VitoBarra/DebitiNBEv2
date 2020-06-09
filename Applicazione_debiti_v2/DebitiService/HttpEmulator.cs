using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
//using MySql.Data.MySqlClient;
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
            public struct Table
            {
                public string TableName;
                public string[] Columns;

                public Table(string t, string[] vs)
                {
                    TableName = t;
                    Columns = vs;
                }

            }

            static public readonly string databaseName = "debiti";

            static public readonly string CONNECTION_STR = $"server=localhost;database={databaseName};uid=root;pwd=toor";



            static public readonly Table UserTable = new Table("user",
                new string[] { "ID", "Email", "Username", "Password", "Name", "Lastname" });
            static public readonly Table FriendListTable = new Table("friendlist",
                new string[] { "user_ID", "user_ID1", "Stato" });
            static public readonly Table RequestTable = new Table("Request",
                new string[] { "ID", "ID_mandante", "ID_ricevente", "Credito", "Stato" });
            static public readonly Table PaymentRequestTable = new Table("paymentrequest",
                new string[] { "ID", "Request_ID", "Credito", "Stato" });
        }


        public static void DatabaseConnection() => MySqlTool.StrConnection = Databaseinfo.CONNECTION_STR;


        
        public static void DebugFunction()
        {


        }



        public static string GetLoginUser(string username, string password)
        {
            MySqlReaderTool mySqlReader = new MySqlReaderTool();
            UserData ud = new UserData();


            string[] Column = new string[] { d.UserTable.Columns[0], d.UserTable.Columns[3] };
            mySqlReader.ReadFrom(Column, $"{d.databaseName}.{d.UserTable.TableName}", $"{d.UserTable.Columns[2]} ='{username}' AND {d.UserTable.Columns[3]} = '{password}'");


            if (mySqlReader.reader.Read())
            {
                ud.ID = mySqlReader.reader.GetInt32(Column[0]);
                ud.Username = username;

                return JsonConvert.SerializeObject(ud, Formatting.Indented);
            }
            else
                return null;
        }



        public static string PostFriendRequest(int idMandante, int idRicevente)
        {
            MySqlReaderTool mySqlReader = new MySqlReaderTool();


            string Column = d.UserTable.Columns[0];


            try
            {
                if (!FriendListcheck(idMandante, idRicevente))
                {
                    MySqlWriterTool.ExecuteInsert($"{d.databaseName}.{d.FriendListTable.TableName}", "", $"{idMandante},{idRicevente},1");

                    return JsonConvert.SerializeObject(0);
                }
                else
                    return JsonConvert.SerializeObject(1);
            }
            catch
            {
                return JsonConvert.SerializeObject(2);
            }


        }


        public static string GetReqeust(int id)
        {
            MySqlReaderTool mySqlReader = new MySqlReaderTool();

            string[] Column = new string[] { d.RequestTable.Columns[1], d.RequestTable.Columns[2], d.RequestTable.Columns[3], d.RequestTable.Columns[4] };

            List<RequestData> requestDatas = new List<RequestData>();

            mySqlReader.ReadFrom(Column, $"{d.databaseName}.{d.RequestTable.TableName}", $"{d.RequestTable.Columns[1]} ='{id}' OR  {d.RequestTable.Columns[2]} ='{id}'");


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

            string Column = d.UserTable.Columns[2];

            mySqlReaderTool.ReadFrom(Column, $"{d.databaseName}.{d.UserTable.TableName}", $"{d.UserTable.Columns[0]} ={id}");
            if (mySqlReaderTool.reader.Read())
                return mySqlReaderTool.reader.GetString(Column);
            else
                return null;
        }

        private static bool FriendListcheck(int id1, int id2)
        {

            MySqlReaderTool mySqlReaderTool = new MySqlReaderTool();


            string[] Column = d.FriendListTable.Columns;

            mySqlReaderTool.ReadFrom("*", $"{d.databaseName}.{d.FriendListTable.TableName}", $"({Column[0]}='{id1}' AND {Column[1]}='{id2}') or ({Column[0]}='{id2}' AND {Column[1]}='{id1}')");
            if (mySqlReaderTool.reader.Read())
                return true;
            else
                return false;

        }


        #endregion


    }
}
