using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace MyTool.MySql
{
    static class MySqlTool 
    {
        public static string ColumnFormatter(string[] str)
        {
            string s = "";
            for (int i = 0; i < str.Length; i++)
            {
                if (i == str.Length - 1)
                    s += str[i];
                else
                    s += str[i] + ",";
            }

            return s;
        }

        static private string strConnection;
        public static string StrConnection { get => strConnection; set { strConnection = value; MySqlWriterTool.Connection = new MySqlConnection(value); } }

        static private MySqlCommand com;



        public class MySqlReaderTool : IDisposable
        {
            private MySqlConnection Connection;
            public bool StateOfConnection { get => Connection.State == ConnectionState.Open; }
            public MySqlDataReader reader;

           public  MySqlReaderTool()
            {
                Connection = new MySqlConnection(StrConnection);
            }



            private void OpenConnection()
            {
                try
                {
                    Connection.Open();
                }
                catch
                {
                }
            }

            public void ReadFrom(string colonne, string nomeTabella, string whereInfo = null)
            {
                if (Connection != null)
                {
                    OpenConnection();

                    if (StateOfConnection)
                    {
                        if (string.IsNullOrEmpty(whereInfo))
                            com = new MySqlCommand("SELECT " + colonne + "  FROM " + nomeTabella, Connection);
                        else
                            com = new MySqlCommand("SELECT " + colonne + "  FROM " + nomeTabella + " WHERE " + whereInfo, Connection);

                        reader = com.ExecuteReader();
                    }
                }
            }

            public void ReadFrom(string[] colonne, string nomeTabella, string whereInfo = null)
            {
                if (Connection != null)
                {
                    OpenConnection();

                    if (StateOfConnection)
                    {
                        if (string.IsNullOrEmpty(whereInfo))
                            com = new MySqlCommand("SELECT " + ColumnFormatter(colonne) + "  FROM " + nomeTabella, Connection);
                        else
                            com = new MySqlCommand("SELECT " + ColumnFormatter(colonne) + "  FROM " + nomeTabella + " WHERE " + whereInfo, Connection);

                        reader = com.ExecuteReader();
                    }
                }
            }

            public void Dispose()
            {
                reader.Close();
            }

            ~MySqlReaderTool()
            {
                Dispose();
            }
        }


        public static class MySqlWriterTool
        {
            static public MySqlConnection Connection;
            public static bool StateOfConnection { get => Connection.State == ConnectionState.Open; }

            static private void OpenConnection()
            {
                try
                {
                    Connection.Open();
                }
                catch
                {
                }
            }

            public static void ExecuteInsert(string TableName, string Columns, string Value)
            {
                if (Connection != null)
                {
                    OpenConnection();
                    if (StateOfConnection)
                    {
                        com = new MySqlCommand("INSERT INTO " + TableName + "( " + Columns + " ) " + "VALUES ( " + Value + ")", Connection);
                        com.ExecuteNonQuery();
                        Connection.Close();
                    }
                }

            }

            public static void ExecuteInsert(string TableName, string[] Columns, string Value)
            {
                if (Connection != null)
                {
                    OpenConnection();
                    if (StateOfConnection)
                    {
                        com = new MySqlCommand("INSERT INTO " + TableName + "( " + ColumnFormatter(Columns) + " ) " + "VALUES ( " + Value + ")", Connection);
                        com.ExecuteNonQuery();
                        Connection.Close();
                    }
                }

            }


            public static void ExecuteUpdate(string TableName, string Columnsvelues, string WhereInfo)
            {
                if (Connection != null)
                {
                    OpenConnection();
                    if (StateOfConnection)
                    {
                        com = new MySqlCommand("UPDATE " + TableName + " SET " + Columnsvelues + " WHERE( " + WhereInfo + " )", Connection);
                        com.ExecuteNonQuery();
                        Connection.Close();
                    }
                }
            }
  




            public static void ExecuteDelete(string TableName, string WhereInfo)
            {
                if (Connection != null)
                {
                    OpenConnection();
                    if (StateOfConnection)
                    {
                        com = new MySqlCommand("DELETE FROM " + TableName + " WHERE( " + WhereInfo + " )", Connection);
                        com.ExecuteNonQuery();
                        Connection.Close();
                    }
                }

            }



        }
    }
}
