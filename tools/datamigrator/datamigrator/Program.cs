using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace datamigrator
{
    class Program
    {
        private static SqlConnection target;
        private static SqlConnection source;
        private static TextWriter messages; 
        
        static void Main(string[] args)
        {
            target = OpenConnection("Data Source=localhost;Initial Catalog=itcommunity;Persist Security Info=True;User ID=wchk;Password=1234;persist security info=False;Connection Timeout=30;");
            source = OpenConnection("Data Source=localhost;Initial Catalog=itc;Persist Security Info=True;User ID=wchk;Password=1234;persist security info=False;Connection Timeout=30;");
            messages = new StreamWriter("messages.txt");
           // MoveCategories();
        }


        private static void MoveCategories()
        {
            DataTable sourceCat = GetDataTable("select * from tblNewsType", source);
            for (int i = 0; i < sourceCat.Rows.Count; i++ )
            {
                InsertData("SET IDENTITY_INSERT categories on; insert into categories(id, name, sort) values(" + sourceCat.Rows[i]["NewsType_Id"] + ", '" + sourceCat.Rows[i]["NewsType"] + "' ," + sourceCat.Rows[i]["NewsType_Id"] + ")", target);
            }
        }
        private static void MoveUsers()
        {
            DataTable sourceCat = GetDataTable("select * from tblUsers", source);
            for (int i = 0; i < sourceCat.Rows.Count; i++)
            {
                InsertData(@"SET IDENTITY_INSERT users on; 
                        insert into users(id, nick, pass, cdate, role, email) values(" 
                        + sourceCat.Rows[i]["user_Id"] + ", '"
                        + sourceCat.Rows[i]["nick"] + "' ," 
                        + sourceCat.Rows[i]["role"] + ", '" 
                        + sourceCat.Rows[i]["email"] + "', '" 
                        + sourceCat.Rows[i]["regdate"] + "' )", target);
            }
        }
        private static SqlConnection OpenConnection(string connString)
        {
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            return connection;
        }

        private static DataTable GetDataTable(string query, SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.CommandType = CommandType.Text;

            SqlDataReader reader = cmd.ExecuteReader();

            DataTable table = new DataTable();

            for (int i = 0; (i < reader.FieldCount); i++)
            {
                Type type;
                string name;
                type = reader.GetFieldType(i);
                name = reader.GetName(i);
                table.Columns.Add(name, type);
            }

            while (reader.Read())
            {
                DataRow row = table.NewRow();
                object[] rowdata = new object[reader.FieldCount];
                reader.GetValues(rowdata);
                row.ItemArray = rowdata;
                table.Rows.Add(row);
            }
            reader.Close();
            DataTable result = table;
            return result;
        }
        private static void InsertData(string query, SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
        }

        public static string HashUserPass(string pass, string login)
        {
           // string preparedPass = login + pass;
            //string hashedPass = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(preparedPass.ToUpper(), "SHA1");
            //return hashedPass;
        }
    }
}
