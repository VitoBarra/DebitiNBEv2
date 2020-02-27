using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace AppDebitiV2
{
    static class Global
    {
        public static IUserData UserData { get; set; } = new UserData ( "90" ,"asda");
        public static bool loggedState = false;
    }
}
