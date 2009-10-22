using System;
using System.Collections.Generic;
using System.Web.Security;
using System.Web;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Sql;
using System.Data.SqlTypes;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Imaging;

namespace datamigrator
{
    class Program
    {
        private static StreamWriter writer = new StreamWriter("itcommunity-migrate.log", false);
        private static SqlConnection targetConn;
        private static SqlConnection sourceConn;
        private static List<string> notValidUsers = new List<string>();
        
        static void Main(string[] args)
        {
            string targetConnString = "Data Source=localhost;Initial Catalog=itcommunity;Persist Security Info=True;User ID=wchk;Password=1234;persist security info=False;Connection Timeout=30;";
            string sourceConnString = "Data Source=localhost;Initial Catalog=itc;Persist Security Info=True;User ID=wchk;Password=1234;persist security info=False;Connection Timeout=30;";

            targetConn = OpenConnection(targetConnString);
            sourceConn = OpenConnection(sourceConnString);

            WriteToLog("INFO    Ready, steady, GO!!!1");

            ClearTargetDB();
           MoveCategories();
           // MoveUsers();
           // MovePosts();
           // MovePostComments();
           // UpdateNotValidLogins();

            writer.Flush();
            writer.Close();
        }

        private static string Formatting(string text)
        {
            string result = text;

            result = Regex.Replace(result, "<b>(.*?)</b>", "[b]$1[/b]");
            result = Regex.Replace(result, "<i>(.*?)</i>", "[i]$1[/i]");
            result = Regex.Replace(result, "<s>(.*?)</s>", "[s]$1[/s]");
            result = Regex.Replace(result, "<u>(.*?)</u>", "[u]$1[/u]");            
            result = Regex.Replace(result, "<p>(.*?)</p>", "\n$1\n");

            result = Regex.Replace(result, "<img(.*?)src=(\"|')(.*?)(\"|')(.*?)>", "[img]$3[/img]");
            result = Regex.Replace(result, "<a(.*?)href=(\"|')(.*?)(\"|')(.*?)>(.*?)</a>", "[url=$3]$6[/url]");

            result = Regex.Replace(result, "<ul>(.*?)</ul>", "[list]$2[/list]");
            result = Regex.Replace(result, "<li>(.*?)</li>", "[*]$2");


            result = Regex.Replace(result, "<br />", "\n");
            result = Regex.Replace(result, "\"", "'");
            result = HttpUtility.HtmlEncode(result);
            return result;
        }
        private static void ClearTargetDB()
        {
            WriteToLog("INFO    Clearing target tables start...");
            ClearTargetTables(new string[] { "categories", "users", "posts", "comments" });
            WriteToLog("INFO    Clearing target tables end");
        }

        private static void MoveCategories()
        {
            WriteToLog("INFO    data migrating 'tblNewsType -> categories' start...");

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

            WriteToLog("INFO    data migrating 'tblNewsType -> categories' end");

            WriteToLog("INFO    data migrating 'post_cat' clearing...");
            ExecuteQuery("delete from post_cat", targetConn);
            ResetIdentitySeed("post_cat");
            WriteToLog("INFO    data migrating 'post_cat' clearing end");
        }
        private static void MoveUsers()
        {

            WriteToLog("INFO    data migrating 'tblUsers -> users' start...");

            DataTable sourceTable = GetSourceTable("tblUsers");

            string currLogin = "";
            string currEmail = "";

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

                if (!isCurrLoginValid)
                {
                    notValidUsers.Add(currLogin);
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
                        "'" + currLogin + "'," +
                        "'" + HashUserPass(sourceTable.Rows[i]["password"].ToString(), currLogin) + "'," +
                        "CONVERT(datetime, '" + sourceTable.Rows[i]["regdate"].ToString() + "', 104)," + 
                        "'" + sourceTable.Rows[i]["role"] + "'," +
                        "'" + validEmail + "')", targetConn);

                if (currEmail != validEmail)
                {
                    if (currEmail == "")
                    {
                        WriteToLog("WARNING user('" + currLogin + "') email not set(empty), reset email to - '" + validEmail + "'");
                    } else
                    {
                        WriteToLog("WARNING user('" + currLogin + "') email('" + currEmail + "') already in db, reset email to - '" + validEmail + "'");
                    }
                  }
            }

            ExecuteQuery("SET IDENTITY_INSERT users off", targetConn);
            ExecuteQuery("update users set role = 2 where role = 3", targetConn);
            ExecuteQuery("update users set role = 1 where nick = 'wchk'", targetConn); // НЕ СТИРАТЬ!!!!1
            ResetIdentitySeed("users");
            WriteToLog("INFO    data migrating 'tblUsers -> users' end");
        }
        private static void MovePosts()
        {

            WriteToLog("INFO    data migrating 'tblNews -> posts' start...");

            DataTable sourceTable = GetSourceTable("tblNews");

            for (int i = 0; i < sourceTable.Rows.Count; i++)
            {
                string author_login = sourceTable.Rows[i]["author"].ToString();
                int author_id = ExecuteScalar("select id from users where UPPER(nick) = UPPER('" + author_login + "')", targetConn);
                int post_id = Convert.ToInt32(sourceTable.Rows[i]["news_id"]);
                string introImgLink = "";
                string mainImgLink = "";

                if (author_id > 0)
                {
                    introImgLink = SaveIntroImage(post_id, author_id);
                    mainImgLink = SaveMainImage(post_id, author_id);

                    SqlCommand cmd = new SqlCommand(
                    "SET IDENTITY_INSERT posts on;insert into posts(id,title,description,text,cdate,user_id,attached,views,source,comments_count) values(@id,@title,@description,@text,@cdate,@user_id,@attached,@views,@source,@comments_count)", targetConn);


                    SqlParameter id = cmd.Parameters.Add("@id", SqlDbType.Int);
                    id.Value = post_id;

                    SqlParameter title = cmd.Parameters.Add("@title", SqlDbType.NVarChar);
                    //обрезаем слижком длинные тайтлы
                    string title_original = sourceTable.Rows[i]["title"].ToString();
                    title.Value = Formatting(title_original.Length > 100 ? title_original.Substring(0, 100) : title_original);

                    SqlParameter desc = cmd.Parameters.Add("@description", SqlDbType.NVarChar);
                    desc.Value = introImgLink + Formatting(sourceTable.Rows[i]["intro"].ToString());

                    SqlParameter text = cmd.Parameters.Add("@text", SqlDbType.NVarChar);
                    text.Value = mainImgLink + Formatting(sourceTable.Rows[i]["main"].ToString());

                    SqlParameter date = cmd.Parameters.Add("@cdate", SqlDbType.DateTime);
                    date.Value = Convert.ToDateTime(sourceTable.Rows[i]["ndate"].ToString());

                    SqlParameter user_id = cmd.Parameters.Add("@user_id", SqlDbType.Int);
                    user_id.Value = author_id;

                    SqlParameter attached = cmd.Parameters.Add("@attached", SqlDbType.Int);
                    attached.Value = Convert.ToInt32(sourceTable.Rows[i]["ontop"].ToString());

                    SqlParameter views = cmd.Parameters.Add("@views", SqlDbType.Int);
                    views.Value = Convert.ToInt32(sourceTable.Rows[i]["views"].ToString());

                    SqlParameter source = cmd.Parameters.Add("@source", SqlDbType.NVarChar);
                    //обрезаем слижком длинные источники новостей
                    string source_original = sourceTable.Rows[i]["source"].ToString();
                    source.Value = source_original.Length > 900 ? source_original.Substring(0, 900) : source_original;

                    SqlParameter comm_count = cmd.Parameters.Add("@comments_count", SqlDbType.Int);
                    comm_count.Value = ExecuteScalar("select count(*) from tblComment where cnews_id = '" + sourceTable.Rows[i]["news_id"] + "'", sourceConn);

                    cmd.ExecuteNonQuery();



                    ExecuteQuery("insert into post_cat(post_id, cat_id) values(" + sourceTable.Rows[i]["news_id"] + ", " + sourceTable.Rows[i]["newstype_id"] + " )", targetConn);
                } else
                {
                    WriteToLog("WARNING cannot move post with id = '" + post_id + "': FK error, author(user) with nick = '" + author_login + "' not exist");
                }
            }
            ExecuteQuery("SET IDENTITY_INSERT posts off", targetConn);
            ResetIdentitySeed("posts");
            WriteToLog("INFO    data migrating 'tblNews -> posts' end");
        }
        private static void MovePostComments()
        {

            WriteToLog("INFO    data migrating 'tblComment -> comments' start...");

            DataTable sourceTable = GetSourceTable("tblComment");

            for (int i = 0; i < sourceTable.Rows.Count; i++)
            {
                string author_login = sourceTable.Rows[i]["nick"].ToString();
                int author_id = ExecuteScalar("select id from users where UPPER(nick) = UPPER('" + author_login + "')", targetConn);
                int news_id = Convert.ToInt32(sourceTable.Rows[i]["cnews_id"]);
                int comment_id = Convert.ToInt32(sourceTable.Rows[i]["comment_id"]);
                bool is_post_exist = ExecuteScalar("select id from posts where id = " + news_id + " ", targetConn) > 0;
                if (author_id > 0 && is_post_exist)
                {
                    SqlCommand cmd = new SqlCommand(
                    "SET IDENTITY_INSERT comments on;insert into comments(id,post_id,user_id,cdate,ip,text) values(@id,@post_id,@user_id,@cdate,@ip,@text)", targetConn);


                    SqlParameter id = cmd.Parameters.Add("@id", SqlDbType.Int);
                    id.Value = comment_id;

                    SqlParameter post_id = cmd.Parameters.Add("@post_id", SqlDbType.Int);
                    post_id.Value = news_id;

                    SqlParameter user_id = cmd.Parameters.Add("@user_id", SqlDbType.Int);
                    user_id.Value = author_id;

                    SqlParameter cdate = cmd.Parameters.Add("@cdate", SqlDbType.DateTime);
                    cdate.Value = Convert.ToDateTime(sourceTable.Rows[i]["cdate"].ToString());

                    SqlParameter ip = cmd.Parameters.Add("@ip", SqlDbType.NVarChar);
                    ip.Value = sourceTable.Rows[i]["ip"].ToString();

                    SqlParameter text = cmd.Parameters.Add("@text", SqlDbType.NVarChar);
                    string comment_text = Formatting(sourceTable.Rows[i]["comment"].ToString()); 
                    text.Value = comment_text.Length > 512 ? comment_text.Substring(0, 512) : comment_text;

                    cmd.ExecuteNonQuery();
                } else
                {
                    if (is_post_exist)
                    {
                        WriteToLog("WARNING cannot move comment with id = '" + comment_id + "': FK error, post with id = '" + news_id + "' not exist");
                    } else
                    {
                        WriteToLog("WARNING cannot move comment with id = '" + comment_id + "'(post id = '" + news_id + "'): FK error, comment author with login = '" + author_login + "' not exist");
                    }
                }
            }
            ExecuteQuery("SET IDENTITY_INSERT comments off", targetConn);
            ResetIdentitySeed("comments");
            WriteToLog("INFO    data migrating 'tblComments -> comments' end");
        }
        private static void UpdateNotValidLogins()
        {
            WriteToLog("INFO    reset not valid logins...");
            string validLogin = "";
            string newpass = "";
            Random rand = new Random();
            foreach (string login in notValidUsers)
            {
                validLogin = "user" + rand.Next();
                newpass = "" + rand.Next();
                ExecuteQuery("update users set nick='" + validLogin + "', pass = '" + HashUserPass(newpass, validLogin) + "' where nick='" + login+ "'", targetConn);
                WriteToLog("WARNING user login - '" + login + "' is not valid, reset login to - '" + validLogin + "' and reset pass to - " + newpass);
      
            }
            notValidUsers.Clear();
            WriteToLog("INFO    reset not valid logins end");
        }
        private static string SaveIntroImage(int post_id, int author_id)
        {
            string res = "";
            try
            {

                MemoryStream stream = new MemoryStream();
                SqlCommand command = new SqlCommand("select introImage from tblNews where news_id = " + post_id, sourceConn);
                byte[] image = (byte[])command.ExecuteScalar();
                stream.Write(image, 0, image.Length);
                Bitmap bitmap = new Bitmap(stream);
                Random rand = new Random();
                if (bitmap != null)
                {
                    string path = CreateFolder(author_id, post_id, "full", "postimages");
                    string real_path = path + rand.Next(0, 999999) + ".jpg";
                    FileStream fs = File.OpenWrite(real_path);
                    bitmap.Save(fs, ImageFormat.Jpeg);
                    fs.Close();
                    fs.Dispose();
                    res = "[float=left][img]" + real_path + "[/img][/float]";

                }

                bitmap.Dispose();
                stream.Close();
                stream.Dispose();
            } catch
            {
                res = "";
            }
            return res;
        }
        private static string SaveMainImage(int post_id, int author_id)
        {
            string res = "";
            try
            {

                MemoryStream stream = new MemoryStream();
                SqlCommand command = new SqlCommand("select mainImage from tblNews where news_id = " + post_id, sourceConn);
                byte[] image = (byte[])command.ExecuteScalar();
                stream.Write(image, 0, image.Length);
                Bitmap bitmap = new Bitmap(stream);
                Random rand = new Random();
                if (bitmap != null)
                {
                    string path = CreateFolder(author_id, post_id, "full", "postimages");
                    string real_path = path + rand.Next(0, 999999) + ".jpg";
                    FileStream fs = File.OpenWrite(real_path);
                    bitmap.Save(fs, ImageFormat.Jpeg);
                    fs.Close();
                    fs.Dispose();
                    res = "[img]" + real_path + "[/img]\n\n";

                }

                bitmap.Dispose();
                stream.Close();
                stream.Dispose();
            } catch
            {
                res = "";
            }
            return res;
        }

        #region Всякая низкоуровневая хрень

        public static string CreateFolder(int user_id, int post_id, string folder, string imagesfolder)
        {
            string current_folder =  imagesfolder + "/" + user_id + "/" + post_id + "/" + folder + "/";
            if (!Directory.Exists(current_folder))
            {
                Directory.CreateDirectory(current_folder);
            }
            return current_folder;
        }

        private static void ClearTargetTables(string[] tables)
        {
            foreach (string tablename in tables)
            {
                WriteToLog("INFO    Clearing '" + tablename + "' start...");
                ClearTargetTable(tablename);
                WriteToLog("INFO    Clearing '" + tablename + "' end");
            }
        }

        private static void ClearTargetTable(string tablename)
        {
            ExecuteQuery("delete from " + tablename, targetConn);
        }

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
            writer.WriteLine(DateTime.Now.ToString("HH:mm:ss:ffff") + "  " + data);
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
