using System;
using System.Collections.Generic;
using System.Web.Security;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Text.RegularExpressions;

namespace datamigrator
{
    class Program
    {
        private static StreamWriter writer = new StreamWriter("itcommunity-migrate.log", false);
        private static SqlConnection targetConn;
        private static SqlConnection sourceConn;
        
        static void Main(string[] args)
        {
            targetConn = OpenConnection("Data Source=localhost;Initial Catalog=itcommunity;Persist Security Info=True;User ID=wchk;Password=1234;persist security info=False;Connection Timeout=30;");
            sourceConn = OpenConnection("Data Source=localhost;Initial Catalog=itc;Persist Security Info=True;User ID=wchk;Password=1234;persist security info=False;Connection Timeout=30;");

            WriteToLog("Ready, steady, GO!!!1");
            MoveCategories();
            MoveUsers();
            MovePosts();

            writer.Flush();
            writer.Close();
        }


        private static void MoveCategories()
        {
            WriteToLog("data migrating 'tblNewsType -> categories' start...");
            ClearTargetTable("categories");

            DataTable sourceTable = GetSourceTable("tblNewsType");
            for (int i = 0; i < sourceTable.Rows.Count; i++ )
            {
                ExecuteQuery(@"SET IDENTITY_INSERT categories on; 
                               insert into categories(id, name, sort) values(" 
                               + sourceTable.Rows[i]["NewsType_Id"] + ", '" 
                               + sourceTable.Rows[i]["NewsType"] + "' ," 
                               + sourceTable.Rows[i]["NewsType_Id"] + ")", targetConn);
            }
            ExecuteQuery("SET IDENTITY_INSERT categories off", targetConn);
            ResetIdentitySeed("categories");

            WriteToLog("data migrating 'tblNewsType -> categories' end");

            WriteToLog("data migrating 'post_cat' clearing...");
            ExecuteQuery("delete from post_cat", targetConn);
            ResetIdentitySeed("post_cat");
            WriteToLog("data migrating 'post_cat' clearing end");
        }
        private static void MoveUsers()
        {

            WriteToLog("data migrating 'tblUsers -> users' start...");
            ClearTargetTable("users");

            DataTable sourceTable = GetSourceTable("tblUsers");

            string currLogin = "";
            string currEmail = "";

            string validLogin = "";
            string validEmail = "";

            bool isCurrLoginValid = false;
            bool isCurrEmailValid = false;

            Random rand = new Random();
            //sourceTable.Rows.Count
            for (int i = 0; i < sourceTable.Rows.Count; i++)
            {
                currLogin = sourceTable.Rows[i]["nick"].ToString();
                currEmail = sourceTable.Rows[i]["email"].ToString();

                isCurrLoginValid = IsLoginValid(currLogin);
                isCurrEmailValid = (currEmail != "");//IsEmailExist(currEmail);

                if (isCurrLoginValid)
                {
                    validLogin = currLogin;
                } else
                {
                    validLogin = "user" + rand.Next(99999);
                }

                if (isCurrEmailValid)
                {
                    validEmail = currEmail;
                } else
                {
                    validEmail = "email" + rand.Next(99999) + "@notvalid.com";
                }

                ExecuteQuery(@"SET IDENTITY_INSERT users on; 
                        insert into users(id, nick, pass, cdate, role, email) values(" 
                        + sourceTable.Rows[i]["user_Id"] + "," +
                        "'" + validLogin + "'," +
                        "'" + HashUserPass(sourceTable.Rows[i]["password"].ToString(), validLogin) + "'," +
                        "CONVERT(datetime, '" + sourceTable.Rows[i]["regdate"].ToString() + "', 104)," + 
                        "'" + sourceTable.Rows[i]["role"] + "'," +
                        "'" + validEmail + "')", targetConn);

                if (currLogin != validLogin)
                {
                    WriteToLog("WARNING логин пользователя '" + currLogin + "' не валидный, теперь его логин - '" + validLogin + "'");
                }
                if (currEmail != validEmail)
                {
                    if (currEmail == "")
                    {
                        WriteToLog("WARNING емейл пользователя '" + currLogin + "' не задан(пустой), теперь его eмейл - '" + validEmail + "'");
                    } else
                    {
                        WriteToLog("WARNING емейл пользователя '" + currLogin + "' уже существует в базе('" + currEmail +"'), теперь его eмейл - '" + validEmail + "'");
                    }
                  }
            }

            ExecuteQuery("SET IDENTITY_INSERT users off", targetConn);
            ExecuteQuery("update users set role = 2 where role = 3", targetConn);
            ResetIdentitySeed("users");
            WriteToLog("data migrating 'tblUsers -> users' end");
        }
        private static void MovePosts()
        {

            WriteToLog("data migrating 'tblNews -> posts' start...");
            ClearTargetTable("posts");

            DataTable sourceTable = GetSourceTable("tblNews");

            for (int i = 0; i < sourceTable.Rows.Count; i++)
            {
                SqlCommand cmd = new SqlCommand(
                "SET IDENTITY_INSERT posts on;insert into posts(id,title,description,text,cdate,user_id,attached,views,source,comments_count) values(@id,@title,@description,@text,@cdate,@user_id,@attached,@views,@source,@comments_count)", targetConn);
                
 
                SqlParameter id = cmd.Parameters.Add("@id", SqlDbType.Int);
                id.Value = Convert.ToInt32(sourceTable.Rows[i]["news_id"]);
                
                SqlParameter title = cmd.Parameters.Add("@title", SqlDbType.NVarChar);
                title.Value = sourceTable.Rows[i]["title"].ToString();
                
                SqlParameter desc = cmd.Parameters.Add("@description", SqlDbType.NVarChar);
                desc.Value = sourceTable.Rows[i]["intro"].ToString();
                
                SqlParameter text = cmd.Parameters.Add("@text", SqlDbType.NVarChar);
                text.Value = sourceTable.Rows[i]["main"].ToString();
                
                SqlParameter date = cmd.Parameters.Add("@cdate", SqlDbType.DateTime);
                date.Value = Convert.ToDateTime(sourceTable.Rows[i]["ndate"].ToString());
                
                SqlParameter user_id = cmd.Parameters.Add("@user_id", SqlDbType.Int);
                user_id.Value = ExecuteScalar("select id from users where UPPER(nick) = UPPER('" + sourceTable.Rows[i]["author"] + "')", targetConn);
                
                SqlParameter attached = cmd.Parameters.Add("@attached", SqlDbType.Int);
                attached.Value = Convert.ToInt32(sourceTable.Rows[i]["ontop"].ToString());
                
                SqlParameter views = cmd.Parameters.Add("@views", SqlDbType.Int);
                views.Value = Convert.ToInt32(sourceTable.Rows[i]["views"].ToString());
                
                SqlParameter source = cmd.Parameters.Add("@source", SqlDbType.NVarChar);
                source.Value = sourceTable.Rows[i]["source"].ToString();

                SqlParameter comm_count = cmd.Parameters.Add("@comments_count", SqlDbType.Int);
                comm_count.Value = ExecuteScalar("select count(*) from tblComment where cnews_id = '" + sourceTable.Rows[i]["news_id"] + "'", sourceConn);

                cmd.ExecuteNonQuery();
                /*
                ExecuteQuery(@"SET IDENTITY_INSERT posts on; 
                        insert into posts(id, 
                                          title, 
                                          description, 
                                          text, 
                                          cdate, 
                                          user_id, 
                                          attached, 
                                          views, 
                                          source,
                                          comments_count) values("
                        + sourceTable.Rows[i]["news_id"] + "," +
                        "'" + sourceTable.Rows[i]["title"] + "'," +
                        "'" + sourceTable.Rows[i]["intro"] + "'," +
                        "'" + sourceTable.Rows[i]["main"] + "'," +
                        "CONVERT(datetime, '" + sourceTable.Rows[i]["ndate"].ToString() + "', 104)," +
                        ExecuteScalar("select id from users where UPPER(nick) = UPPER('" + sourceTable.Rows[i]["author"] + "')", targetConn) + "," +
                        sourceTable.Rows[i]["ontop"] + "," +
                        sourceTable.Rows[i]["views"] + "," +
                        "'" + sourceTable.Rows[i]["source"] + "'," +
                        +ExecuteScalar("select count(*) from tblComment where cnews_id = '" + sourceTable.Rows[i]["news_id"] + "'", sourceConn) + ")"
                        , targetConn);
                */
                ExecuteQuery("insert into post_cat(post_id, cat_id) values(" + sourceTable.Rows[i]["news_id"] + ", " + sourceTable.Rows[i]["newstype_id"] + " )", targetConn);
            }
            ExecuteQuery("SET IDENTITY_INSERT posts off", targetConn);
            ResetIdentitySeed("posts");
            WriteToLog("data migrating 'tblNews -> posts' end");
        }




        #region Всякая низкоуровневая хрень

        private static bool IsLoginValid(string login)
        {
            Regex regexp = new Regex("^[A-Za-z0-9_\\-\\.]{2,20}$");
            return regexp.IsMatch(login);
        }
        private static bool IsEmailExist(string email)
        {
            return ExecuteScalar("select count(id) from users where UPPER(email) = UPPER('" + email+ "')", targetConn) == 0 ? true : false;
        }

        private static void WriteToLog(string data)
        {
            writer.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "  " + data);
        }

        private static SqlConnection OpenConnection(string connString)
        {
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            return connection;
        }
        private static DataTable GetSourceTable(string tablename)
        {
            return GetDataTable("select * from " + tablename, sourceConn);
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
        private static void ResetIdentitySeed(string tablename)
        {
            int maxId = ExecuteScalar("select max(id) from " + tablename, targetConn);
            ExecuteQuery("DBCC CHECKIDENT ('" + tablename + "', RESEED, " + maxId + ") WITH NO_INFOMSGS;", targetConn);

        }
        private static void ClearTargetTable(string tablename)
        {
            ExecuteQuery("delete from " + tablename, targetConn);
        }
        private static void ExecuteQuery(string query, SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
        }
        private static int ExecuteScalar(string query, SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.CommandType = CommandType.Text;
            object obj = cmd.ExecuteScalar();

            return (obj == null || obj.ToString() == "") ? 0 : (int)obj;
        }

        public static string HashUserPass(string pass, string login)
        {
            string preparedPass = login.ToUpper() + pass;
            string hashedPass = FormsAuthentication.HashPasswordForStoringInConfigFile(preparedPass, "SHA1");
            return hashedPass;
        }

        #endregion        
    }
}
