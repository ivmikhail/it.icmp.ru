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
        
        static void Main(string[] args)
        {
            //string sourceConnString = "Data Source=127.0.0.1;Initial Catalog=IT;Persist Security Info=True;User ID=IT;Password=nhfv,kth;persist security info=False;Connection Timeout=30;";
            //string targetConnString = "Data Source=localhost;Initial Catalog=it2;Persist Security Info=True;User ID=it2;Password=dctbltngjgkfye;persist security info=False;Connection Timeout=30;";

            string sourceConnString = "Data Source=localhost;Initial Catalog=itc;Persist Security Info=True;User ID=wchk;Password=1234;persist security info=False;Connection Timeout=30;";
            string targetConnString = "Data Source=localhost;Initial Catalog=itcommunity;Persist Security Info=True;User ID=wchk;Password=1234;persist security info=False;Connection Timeout=30;";

            targetConn = OpenConnection(targetConnString);
            sourceConn = OpenConnection(sourceConnString);

            WriteToLog("INFO    Ready, steady, GO!!!1");

            string[] targetTables = { "Categories", "PostsCategories", "Users", "PostsCategories", "Comments", "Favorites", "Notes" }; // RatingLogs, Ratings
            CleanTargetTables(targetTables);

            MoveUsers();
            MoveCategories();
            MovePosts();
            MovePostCatsLinks();
            MoveComments();
            MoveFavorites();
            MoveRatings();

            writer.Flush();
            writer.Close();
        }

        private static string Formatting(string text)
        {
            string result = text;

            //result = Regex.Replace(result, "<b>(.*?)</b>", "[b]$1[/b]", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            //result = Regex.Replace(result, "<i>(.*?)</i>", "[i]$1[/i]", RegexOptions.Singleline|RegexOptions.IgnoreCase);
            //result = Regex.Replace(result, "<s>(.*?)</s>", "[s]$1[/s]", RegexOptions.Singleline|RegexOptions.IgnoreCase);
            //result = Regex.Replace(result, "<u>(.*?)</u>", "[u]$1[/u]", RegexOptions.Singleline|RegexOptions.IgnoreCase);
            //result = Regex.Replace(result, "<pre>(.*?)</pre>", "[code]$1[/code]", RegexOptions.Singleline|RegexOptions.IgnoreCase);
            //result = Regex.Replace(result, "<p>(.*?)</p>", "\n$1\n", RegexOptions.Singleline|RegexOptions.IgnoreCase);

            //result = Regex.Replace(result, "<img(.*?)src=(\"|')(.*?)(\"|')(.*?)>", "[img]$3[/img]", RegexOptions.Singleline|RegexOptions.IgnoreCase);
            //result = Regex.Replace(result, "<a(.*?)href=(\"|')(.*?)(\"|')(.*?)>(.*?)</a>", "[url=$3]$6[/url]", RegexOptions.Singleline|RegexOptions.IgnoreCase);

            //result = Regex.Replace(result, "<ul>(.*?)</ul>", "[list]$1[/list]", RegexOptions.Singleline|RegexOptions.IgnoreCase);
            //result = Regex.Replace(result, "<li>(.*?)</li>", "[*]$1", RegexOptions.Singleline|RegexOptions.IgnoreCase);
            //result = Regex.Replace(result, @"<li>(.*?)(?=(<li>)|(\[/list]))", "[*]$1", RegexOptions.Singleline|RegexOptions.IgnoreCase);

            //result = Regex.Replace(result, "&quot;", "\"", RegexOptions.Singleline | RegexOptions.IgnoreCase);

            //result = Regex.Replace(result, @"<br\s*/?>", "\n", RegexOptions.Singleline|RegexOptions.IgnoreCase);
            //result = Regex.Replace(result, "\"", "'", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            
            return result;
        }
        private static void MoveNotepad()
        {

            WriteToLog("INFO    data migrating 'notes -> Notes' start...");

            DataTable sourceTable = GetSourceTable("notes");

            for (int i = 0; i < sourceTable.Rows.Count; i++)
            {
                    SqlCommand cmd = new SqlCommand(
                    "SET IDENTITY_INSERT Notes on;insert into Notes(Id,Title,Text,CreateDate,UserId) values(@id,@title,@text,@cdate,@user_id)", targetConn);

                    string label = HttpUtility.HtmlEncode(sourceTable.Rows[i]["label"].ToString());
                    string body = HttpUtility.HtmlEncode(sourceTable.Rows[i]["body"].ToString());

                    SqlParameter id = cmd.Parameters.Add("@id", SqlDbType.Int);
                    id.Value = Convert.ToInt32(sourceTable.Rows[i]["id"].ToString());

                    SqlParameter title = cmd.Parameters.Add("@title", SqlDbType.NVarChar);
                    title.Value = sourceTable.Rows[i]["title"].ToString();

                    SqlParameter text = cmd.Parameters.Add("@text", SqlDbType.NVarChar);
                    text.Value = sourceTable.Rows[i]["text"].ToString();

                    SqlParameter cdate = cmd.Parameters.Add("@cdate", SqlDbType.DateTime);
                    cdate.Value = Convert.ToDateTime(sourceTable.Rows[i]["cdate"].ToString());

                    SqlParameter user_id = cmd.Parameters.Add("@user_id", SqlDbType.Int);
                    user_id.Value = Convert.ToInt32(sourceTable.Rows[i]["user_id"].ToString());

                    cmd.ExecuteNonQuery();
            }
            ExecuteQuery("SET IDENTITY_INSERT Notes off", targetConn);
            ResetIdentitySeed("Notes");
            WriteToLog("INFO    data migrating 'notes -> Notes' end");
        }

        private static void MoveCategories()
        {
            WriteToLog("INFO    data migrating 'categories -> Categories' start...");

            DataTable sourceTable = GetSourceTable("categories");
            for (int i = 0; i < sourceTable.Rows.Count; i++ )
            {
                ExecuteQuery(@"SET IDENTITY_INSERT Categories on; 
                               insert into Categories(Id, Name, Sort) values(" 
                               + sourceTable.Rows[i]["id"] + ", '" 
                               + sourceTable.Rows[i]["name"] + "' ," 
                               + sourceTable.Rows[i]["sort"] + ")", targetConn);
            }
            ExecuteQuery("SET IDENTITY_INSERT Categories off", targetConn);
            ResetIdentitySeed("Categories");

            WriteToLog("INFO    data migrating 'categories -> Categories' end");

           //WriteToLog("INFO    data migrating 'post_cat' clearing...");
          // ExecuteQuery("delete from post_cat", targetConn);
          // ResetIdentitySeed("post_cat");
          // WriteToLog("INFO    data migrating 'post_cat' clearing end");
        }

        private static void MovePostCatsLinks() {
            WriteToLog("INFO    data migrating 'post_cat -> PostsCategories' start...");

            DataTable sourceTable = GetSourceTable("post_cat");
            for (int i = 0; i < sourceTable.Rows.Count; i++) {
                ExecuteQuery(@"SET IDENTITY_INSERT PostsCategories on; 
                               insert into PostsCategories(PostId, CategoryID) values("
                               + sourceTable.Rows[i]["post_id"] + "," +
                               sourceTable.Rows[i]["cat_id"] + ")", targetConn);
            }
            ExecuteQuery("SET IDENTITY_INSERT PostsCategories off", targetConn);
            ResetIdentitySeed("PostsCategories");

            WriteToLog("INFO    data migrating 'post_cat -> PostsCategories' end");
        }
        private static void MoveRatings() {
            WriteToLog("INFO    data migrating 'ratings -> Ratings' start...");
            DataTable sourceTable = GetSourceTable("ratings");
            for (int i = 0; i < sourceTable.Rows.Count; i++) {
                ExecuteQuery(@"SET IDENTITY_INSERT Ratings on; 
                               insert into Ratings(Id,EntityId,EntityType,Value) values("
                               + sourceTable.Rows[i]["id"] + ","
                               +sourceTable.Rows[i]["entity_id"] + ","
                               +sourceTable.Rows[i]["entity_type"] + "," +
                               sourceTable.Rows[i]["value"] + ")", targetConn);
            }
            ExecuteQuery("SET IDENTITY_INSERT PostsCategories off", targetConn);
            ResetIdentitySeed("Ratings");
            WriteToLog("INFO    data migrating 'ratings -> Ratings' end");



            WriteToLog("INFO    data migrating 'rating_logs -> RatingLogs' start...");
            DataTable sourceTable2 = GetSourceTable("post_cat");
            for (int i = 0; i < sourceTable2.Rows.Count; i++) {
                ExecuteQuery(@"SET IDENTITY_INSERT RatingLogs on; 
                               insert into RatingLogs(Id, EntityId,EntityType,UserId,Value,CreateDate) values("
                               + sourceTable.Rows[i]["id"] + "," +
                               sourceTable.Rows[i]["entity_id"] + "," +
                               sourceTable.Rows[i]["entity_type"] + "," +
                               sourceTable.Rows[i]["user_id"] + "," +
                               sourceTable.Rows[i]["value"] + "," +
                               "CONVERT(datetime, '" + sourceTable.Rows[i]["cdate"].ToString() + "', 104)" 
                               + ")", targetConn);
            }
            ExecuteQuery("SET IDENTITY_INSERT RatingLogs off", targetConn);
            ResetIdentitySeed("RatingLogs");

            WriteToLog("INFO    data migrating 'rating_logs -> RatingLogs' end");
        }

        private static void MoveFavorites()
        {
            WriteToLog("INFO    data migrating 'favorites -> Favorites' start...");

            DataTable sourceTable = GetSourceTable("favorites");

            for (int i = 0; i < sourceTable.Rows.Count; i++)
            {
                    SqlCommand cmd = new SqlCommand(
                    "SET IDENTITY_INSERT Favorites on;insert into favorites(Id,UserId,PostId,CreateDate) values(@id,@user_id,@post_id,@cdate)", targetConn);


                    SqlParameter id = cmd.Parameters.Add("@id", SqlDbType.Int);
                    id.Value = Convert.ToInt32(sourceTable.Rows[i]["id"].ToString());

                    SqlParameter user_id = cmd.Parameters.Add("@user_id", SqlDbType.Int);
                    user_id.Value = Convert.ToInt32(sourceTable.Rows[i]["user_id"].ToString());

                    SqlParameter post_id = cmd.Parameters.Add("@post_id", SqlDbType.Int);
                    post_id.Value = Convert.ToInt32(sourceTable.Rows[i]["_id"].ToString());

                    SqlParameter cdate = cmd.Parameters.Add("@cdate", SqlDbType.DateTime);
                    cdate.Value = Convert.ToDateTime(sourceTable.Rows[i]["cdate"].ToString());

                    cmd.ExecuteNonQuery();
            }
            ExecuteQuery("SET IDENTITY_INSERT Favorites off", targetConn);
            ResetIdentitySeed("Favorites");
            WriteToLog("INFO    data migrating 'favorites -> Favorites' end");
        }

        private static void MoveUsers()
        {

            WriteToLog("INFO    data migrating 'users -> Users' start...");

            DataTable sourceTable = GetSourceTable("users");

            for (int i = 0; i < sourceTable.Rows.Count; i++)
            {

                ExecuteQuery(@"SET IDENTITY_INSERT Users on; 
                        insert into users(Id, Nick, Password, CreateDate, Role, Email, HeadersCounter, PostsCount, CommentsCount) values(" 
                        + sourceTable.Rows[i]["id"] + "," +
                        "'" + sourceTable.Rows[i]["nick"] + "'," +
                        "'" + sourceTable.Rows[i]["pass"] + "'," +
                        "CONVERT(datetime, '" + sourceTable.Rows[i]["cdate"].ToString() + "', 104)," + 
                        sourceTable.Rows[i]["role"] +
                        "'" + sourceTable.Rows[i]["email"] + "')" +
                        sourceTable.Rows[i]["header_text_counter"] +
                        sourceTable.Rows[i]["posts_count"] + 
                        sourceTable.Rows[i]["comments_count"] + ")", targetConn);
            }

            ExecuteQuery("SET IDENTITY_INSERT Users off", targetConn);
            ResetIdentitySeed("Users");
            WriteToLog("INFO    data migrating 'users -> Users' end");
        }
        private static void MovePosts()
        {

            WriteToLog("INFO    data migrating 'posts -> Posts' start...");

            DataTable sourceTable = GetSourceTable("posts");

            for (int i = 0; i < sourceTable.Rows.Count; i++)
            {

                    SqlCommand cmd = new SqlCommand(
                    "SET IDENTITY_INSERT Posts on;insert into Posts(Id,Title,Description,Text,CreateDate,AuthorId,IsAttached,ViewsCount,Source,CommentsCount,EntityType, EntityId,IsCommentable) values(@id,@title,@description,@text,@cdate,@user_id,@attached,@views,@source,@comments_count,null,null,@isComm)", targetConn);


                    SqlParameter id = cmd.Parameters.Add("@id", SqlDbType.Int);
                    id.Value = Convert.ToInt32(sourceTable.Rows[i]["id"].ToString());

                    SqlParameter title = cmd.Parameters.Add("@title", SqlDbType.NVarChar);
                    title.Value = sourceTable.Rows[i]["title"].ToString();

                    SqlParameter desc = cmd.Parameters.Add("@description", SqlDbType.NVarChar);
                    desc.Value = Formatting(sourceTable.Rows[i]["description"].ToString());

                    SqlParameter text = cmd.Parameters.Add("@text", SqlDbType.NVarChar);
                    text.Value = Formatting(sourceTable.Rows[i]["text"].ToString());

                    SqlParameter date = cmd.Parameters.Add("@cdate", SqlDbType.DateTime);
                    date.Value = Convert.ToDateTime(sourceTable.Rows[i]["cdate"].ToString());

                    SqlParameter user_id = cmd.Parameters.Add("@user_id", SqlDbType.Int);
                    user_id.Value = Convert.ToInt32(sourceTable.Rows[i]["user_id"].ToString());

                    SqlParameter attached = cmd.Parameters.Add("@attached", SqlDbType.TinyInt);
                    attached.Value = Convert.ToInt32(sourceTable.Rows[i]["attached"].ToString());

                    SqlParameter views = cmd.Parameters.Add("@views", SqlDbType.Int);
                    views.Value = Convert.ToInt32(sourceTable.Rows[i]["views"].ToString());

                    SqlParameter source = cmd.Parameters.Add("@source", SqlDbType.NVarChar);
                    source.Value =  sourceTable.Rows[i]["source"].ToString();

                    SqlParameter comm_count = cmd.Parameters.Add("@comments_count", SqlDbType.Int);
                    comm_count.Value = Convert.ToInt32(sourceTable.Rows[i]["comments_count"].ToString());

                    SqlParameter isComm = cmd.Parameters.Add("@isComm", SqlDbType.Int);
                    comm_count.Value = 1;

                    cmd.ExecuteNonQuery();
            }
            ExecuteQuery("SET IDENTITY_INSERT Posts off", targetConn);
            ResetIdentitySeed("Posts");
            WriteToLog("INFO    data migrating 'posts -> Posts' end");
        }
        private static void MoveComments()
        {

            WriteToLog("INFO    data migrating 'comments -> Comments' start...");

            DataTable sourceTable = GetSourceTable("comments");

            for (int i = 0; i < sourceTable.Rows.Count; i++)
            {
                int author_id = Convert.ToInt32(sourceTable.Rows[i]["user_id"]);
                int news_id = Convert.ToInt32(sourceTable.Rows[i]["post_id"]);
                int comment_id = Convert.ToInt32(sourceTable.Rows[i]["id"]);

                    SqlCommand cmd = new SqlCommand(
                    "SET IDENTITY_INSERT Comments on;insert into Comments(Id,postId,AuthorId,CreateDate,Ip,Text) values(@id,@post_id,@user_id,@cdate,@ip,@text)", targetConn);


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
                    text.Value = Formatting(sourceTable.Rows[i]["text"].ToString()); 

                    cmd.ExecuteNonQuery();
            }
            ExecuteQuery("SET IDENTITY_INSERT Comments off", targetConn);
            ResetIdentitySeed("Comments");
            WriteToLog("INFO    data migrating 'comments -> Comments' end");
        }

        #region Всякая низкоуровневая хрень

        private static void CleanTargetTable(string tablename) {
                WriteToLog("INFO    Clearing '" + tablename + "' start...");
                ClearTargetTable(tablename);
                ResetIdentitySeed(tablename);
                WriteToLog("INFO    Clearing '" + tablename + "' end");
        }
        private static void CleanTargetTables(string[] tables)
        {
            foreach (string tablename in tables)
            {
                CleanTargetTable(tablename);
            }
        }

        private static void ClearTargetTable(string tablename)
        {
            ExecuteQuery("delete from " + tablename, targetConn);
        }


        private static void WriteToLog(string data)
        {
            writer.WriteLine(DateTime.Now.ToString("HH:mm:ss:ffff") + "  " + data);
            writer.Flush();
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
            int maxId = ExecuteScalar("select max(Id) from " + tablename, targetConn);
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
        #endregion        
    }
}
