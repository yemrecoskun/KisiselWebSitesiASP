using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using MySql.Data;
using MySqlX.XDevAPI;

namespace WebProje.Globals
{
    public class SQL
    {
        public static MySqlConnection con = new MySqlConnection("Server = sql7.freemysqlhosting.net; Database=sql7350454;user=sql7350454;pwd='9Fr5vapFdb';");
        public static MySqlCommand cmd;
        public SQL()
        {
            if (con.State == System.Data.ConnectionState.Open) con.Close();
        }
        public void Open()
        {
            if (con.State == System.Data.ConnectionState.Open) con.Close(); con.Open();
        }
        public void Close()
        {
            con.Close();
        }
        public MySqlDataReader CmdReader(string value)
        {
            cmd = new MySqlCommand(value,con);
            var rd = cmd.ExecuteReader();
            return rd;
        }
        public void CmdWriter(string value)
        {   
            con.Open();
            cmd = new MySqlCommand(value,con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
    public class Globals
    {

    }
}