using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
using System.Web;
namespace ITCommunity {
    public class Database {
        private static SqlConnection OpenConnection() {
            string connectionString;
            if (ConfigurationManager.ConnectionStrings[Environment.MachineName] != null) {
                connectionString = ConfigurationManager.ConnectionStrings[Environment.MachineName].ConnectionString;
            } else {
                connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            }
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
        public static DataTable CaptchaAnswerAdd(Int32 question_id) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("CaptchaAnswerAdd", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@question_id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@question_id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@question_id"].Value = question_id;
            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            System.Data.DataTable table = new DataTable();

            for (int i = 0; (i < reader.FieldCount); i++) {
                System.Type __type;
                string __name;
                __type = reader.GetFieldType(i);
                __name = reader.GetName(i);
                table.Columns.Add(__name, __type);
            }

            while (reader.Read()) {
                System.Data.DataRow row = table.NewRow();
                object[] rowdata = new object[reader.FieldCount];
                reader.GetValues(rowdata);
                row.ItemArray = rowdata;
                table.Rows.Add(row);
            }
            reader.Close();
            DataTable result = table;
            connection.Close();
            return result;
        }
        public static int CaptchaAnswerDelete(Int32 id) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("CaptchaAnswerDelete", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@id"].Value = id;
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            return result;
        }
        public static DataTable CaptchaAnswersList(Int32 id) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("CaptchaAnswersList", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@id"].Value = id;
            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            System.Data.DataTable table = new DataTable();

            for (int i = 0; (i < reader.FieldCount); i++) {
                System.Type __type;
                string __name;
                __type = reader.GetFieldType(i);
                __name = reader.GetName(i);
                table.Columns.Add(__name, __type);
            }

            while (reader.Read()) {
                System.Data.DataRow row = table.NewRow();
                object[] rowdata = new object[reader.FieldCount];
                reader.GetValues(rowdata);
                row.ItemArray = rowdata;
                table.Rows.Add(row);
            }
            reader.Close();
            DataTable result = table;
            connection.Close();
            return result;
        }
        public static int CaptchaAnswerUpdate(Int32 id, String text, Byte isRight) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("CaptchaAnswerUpdate", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@id"].Value = id;
            cmd.Parameters.Add("@text", System.Data.SqlDbType.NVarChar, 50);
            cmd.Parameters["@text"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@text"].Value = text;
            cmd.Parameters.Add("@isRight", System.Data.SqlDbType.TinyInt, 0);
            cmd.Parameters["@isRight"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@isRight"].Value = isRight;
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            return result;
        }
        public static int CaptchaDelete(Int32 id) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("CaptchaDelete", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@id"].Value = id;
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            return result;
        }
        public static DataTable CaptchaGet() {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("CaptchaGet", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            System.Data.DataTable table = new DataTable();

            for (int i = 0; (i < reader.FieldCount); i++) {
                System.Type __type;
                string __name;
                __type = reader.GetFieldType(i);
                __name = reader.GetName(i);
                table.Columns.Add(__name, __type);
            }

            while (reader.Read()) {
                System.Data.DataRow row = table.NewRow();
                object[] rowdata = new object[reader.FieldCount];
                reader.GetValues(rowdata);
                row.ItemArray = rowdata;
                table.Rows.Add(row);
            }
            reader.Close();
            DataTable result = table;
            connection.Close();
            return result;
        }
        public static object CaptchaQuestionAdd() {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("CaptchaQuestionAdd", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            object result = cmd.ExecuteScalar();
            connection.Close();
            return result;
        }
        public static DataRow CaptchaQuestionGet(Int32 id) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("CaptchaQuestionGet", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@id"].Value = id;
            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            System.Data.DataTable table = new DataTable();

            for (int i = 0; (i < reader.FieldCount); i++) {
                System.Type __type;
                string __name;
                __type = reader.GetFieldType(i);
                __name = reader.GetName(i);
                table.Columns.Add(__name, __type);
            }

            DataRow result;
            if (reader.Read()) {
                System.Data.DataRow row = table.NewRow();
                object[] rowdata = new object[reader.FieldCount];
                reader.GetValues(rowdata);
                row.ItemArray = rowdata;
                result = row;
            } else {
                result = null;
            }
            reader.Close();
            connection.Close();
            return result;
        }
        public static DataTable CaptchaQuestionsList() {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("CaptchaQuestionsList", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            System.Data.DataTable table = new DataTable();

            for (int i = 0; (i < reader.FieldCount); i++) {
                System.Type __type;
                string __name;
                __type = reader.GetFieldType(i);
                __name = reader.GetName(i);
                table.Columns.Add(__name, __type);
            }

            while (reader.Read()) {
                System.Data.DataRow row = table.NewRow();
                object[] rowdata = new object[reader.FieldCount];
                reader.GetValues(rowdata);
                row.ItemArray = rowdata;
                table.Rows.Add(row);
            }
            reader.Close();
            DataTable result = table;
            connection.Close();
            return result;
        }
        public static int CaptchaQuestionUpdate(Int32 id, String text) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("CaptchaQuestionUpdate", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@id"].Value = id;
            cmd.Parameters.Add("@text", System.Data.SqlDbType.NVarChar, 200);
            cmd.Parameters["@text"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@text"].Value = text;
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            return result;
        }
        public static DataRow CategoryAdd(String name, Int32 sort) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("CategoryAdd", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@name", System.Data.SqlDbType.NVarChar, 32);
            cmd.Parameters["@name"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@name"].Value = name;
            cmd.Parameters.Add("@sort", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@sort"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@sort"].Value = sort;
            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            System.Data.DataTable table = new DataTable();

            for (int i = 0; (i < reader.FieldCount); i++) {
                System.Type __type;
                string __name;
                __type = reader.GetFieldType(i);
                __name = reader.GetName(i);
                table.Columns.Add(__name, __type);
            }

            DataRow result;
            if (reader.Read()) {
                System.Data.DataRow row = table.NewRow();
                object[] rowdata = new object[reader.FieldCount];
                reader.GetValues(rowdata);
                row.ItemArray = rowdata;
                result = row;
            } else {
                result = null;
            }
            reader.Close();
            connection.Close();
            return result;
        }
        public static int CategoryDel(Int32 id) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("CategoryDel", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@id"].Value = id;
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            return result;
        }
        public static DataTable CategoryGetAll() {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("CategoryGetAll", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            System.Data.DataTable table = new DataTable();

            for (int i = 0; (i < reader.FieldCount); i++) {
                System.Type __type;
                string __name;
                __type = reader.GetFieldType(i);
                __name = reader.GetName(i);
                table.Columns.Add(__name, __type);
            }

            while (reader.Read()) {
                System.Data.DataRow row = table.NewRow();
                object[] rowdata = new object[reader.FieldCount];
                reader.GetValues(rowdata);
                row.ItemArray = rowdata;
                table.Rows.Add(row);
            }
            reader.Close();
            DataTable result = table;
            connection.Close();
            return result;
        }
        public static DataRow CategoryGetById(Int32 id) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("CategoryGetById", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@id"].Value = id;
            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            System.Data.DataTable table = new DataTable();

            for (int i = 0; (i < reader.FieldCount); i++) {
                System.Type __type;
                string __name;
                __type = reader.GetFieldType(i);
                __name = reader.GetName(i);
                table.Columns.Add(__name, __type);
            }

            DataRow result;
            if (reader.Read()) {
                System.Data.DataRow row = table.NewRow();
                object[] rowdata = new object[reader.FieldCount];
                reader.GetValues(rowdata);
                row.ItemArray = rowdata;
                result = row;
            } else {
                result = null;
            }
            reader.Close();
            connection.Close();
            return result;
        }
        public static int CategoryUpdate(Int32 id, Int32 sort, String name) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("CategoryUpdate", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@id"].Value = id;
            cmd.Parameters.Add("@sort", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@sort"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@sort"].Value = sort;
            cmd.Parameters.Add("@name", System.Data.SqlDbType.NVarChar, 32);
            cmd.Parameters["@name"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@name"].Value = name;
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            return result;
        }
        public static DataRow CommentAdd(Int32 post_id, Int32 user_id, String ip, String text) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("CommentAdd", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@post_id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@post_id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@post_id"].Value = post_id;
            cmd.Parameters.Add("@user_id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@user_id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@user_id"].Value = user_id;
            cmd.Parameters.Add("@ip", System.Data.SqlDbType.NVarChar, 50);
            cmd.Parameters["@ip"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@ip"].Value = ip;
            cmd.Parameters.Add("@text", System.Data.SqlDbType.NVarChar, 512);
            cmd.Parameters["@text"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@text"].Value = text;
            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            System.Data.DataTable table = new DataTable();

            for (int i = 0; (i < reader.FieldCount); i++) {
                System.Type __type;
                string __name;
                __type = reader.GetFieldType(i);
                __name = reader.GetName(i);
                table.Columns.Add(__name, __type);
            }

            DataRow result;
            if (reader.Read()) {
                System.Data.DataRow row = table.NewRow();
                object[] rowdata = new object[reader.FieldCount];
                reader.GetValues(rowdata);
                row.ItemArray = rowdata;
                result = row;
            } else {
                result = null;
            }
            reader.Close();
            connection.Close();
            return result;
        }
        public static int CommentDel(Int32 id) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("CommentDel", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@id"].Value = id;
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            return result;
        }
        public static DataTable CommentGetByPost(Int32 id) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("CommentGetByPost", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@id"].Value = id;
            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            System.Data.DataTable table = new DataTable();

            for (int i = 0; (i < reader.FieldCount); i++) {
                System.Type __type;
                string __name;
                __type = reader.GetFieldType(i);
                __name = reader.GetName(i);
                table.Columns.Add(__name, __type);
            }

            while (reader.Read()) {
                System.Data.DataRow row = table.NewRow();
                object[] rowdata = new object[reader.FieldCount];
                reader.GetValues(rowdata);
                row.ItemArray = rowdata;
                table.Rows.Add(row);
            }
            reader.Close();
            DataTable result = table;
            connection.Close();
            return result;
        }
        public static DataTable CommentGetLasts(Int32 count) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("CommentGetLasts", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@count", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@count"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@count"].Value = count;
            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            System.Data.DataTable table = new DataTable();

            for (int i = 0; (i < reader.FieldCount); i++) {
                System.Type __type;
                string __name;
                __type = reader.GetFieldType(i);
                __name = reader.GetName(i);
                table.Columns.Add(__name, __type);
            }

            while (reader.Read()) {
                System.Data.DataRow row = table.NewRow();
                object[] rowdata = new object[reader.FieldCount];
                reader.GetValues(rowdata);
                row.ItemArray = rowdata;
                table.Rows.Add(row);
            }
            reader.Close();
            DataTable result = table;
            connection.Close();
            return result;
        }
        public static DataRow FavoriteAdd(Int32 user_id, Int32 post_id) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("FavoriteAdd", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@user_id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@user_id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@user_id"].Value = user_id;
            cmd.Parameters.Add("@post_id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@post_id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@post_id"].Value = post_id;
            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            System.Data.DataTable table = new DataTable();

            for (int i = 0; (i < reader.FieldCount); i++) {
                System.Type __type;
                string __name;
                __type = reader.GetFieldType(i);
                __name = reader.GetName(i);
                table.Columns.Add(__name, __type);
            }

            DataRow result;
            if (reader.Read()) {
                System.Data.DataRow row = table.NewRow();
                object[] rowdata = new object[reader.FieldCount];
                reader.GetValues(rowdata);
                row.ItemArray = rowdata;
                result = row;
            } else {
                result = null;
            }
            reader.Close();
            connection.Close();
            return result;
        }
        public static int FavoriteDel(Int32 post_id, Int32 user_id) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("FavoriteDel", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@post_id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@post_id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@post_id"].Value = post_id;
            cmd.Parameters.Add("@user_id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@user_id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@user_id"].Value = user_id;
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            return result;
        }
        public static DataTable FavoriteGetByUser(Int32 user_id, Int32 page, Int32 count, ref Int32 posts_count) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("FavoriteGetByUser", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@user_id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@user_id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@user_id"].Value = user_id;
            cmd.Parameters.Add("@page", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@page"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@page"].Value = page;
            cmd.Parameters.Add("@count", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@count"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@count"].Value = count;
            cmd.Parameters.Add("@posts_count", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@posts_count"].Direction = System.Data.ParameterDirection.InputOutput;
            cmd.Parameters["@posts_count"].Value = posts_count;
            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            System.Data.DataTable table = new DataTable();

            for (int i = 0; (i < reader.FieldCount); i++) {
                System.Type __type;
                string __name;
                __type = reader.GetFieldType(i);
                __name = reader.GetName(i);
                table.Columns.Add(__name, __type);
            }

            while (reader.Read()) {
                System.Data.DataRow row = table.NewRow();
                object[] rowdata = new object[reader.FieldCount];
                reader.GetValues(rowdata);
                row.ItemArray = rowdata;
                table.Rows.Add(row);
            }
            reader.Close();
            DataTable result = table;
            posts_count = (Int32)(cmd.Parameters["@posts_count"].Value);
            connection.Close();
            return result;
        }
        public static DataRow MenuItemsAdd(Int32 parent_id, String url, Int32 sort, String name, Byte new_window) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("MenuItemsAdd", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@parent_id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@parent_id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@parent_id"].Value = parent_id;
            cmd.Parameters.Add("@url", System.Data.SqlDbType.NVarChar, 256);
            cmd.Parameters["@url"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@url"].Value = url;
            cmd.Parameters.Add("@sort", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@sort"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@sort"].Value = sort;
            cmd.Parameters.Add("@name", System.Data.SqlDbType.NVarChar, 32);
            cmd.Parameters["@name"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@name"].Value = name;
            cmd.Parameters.Add("@new_window", System.Data.SqlDbType.TinyInt, 0);
            cmd.Parameters["@new_window"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@new_window"].Value = new_window;
            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            System.Data.DataTable table = new DataTable();

            for (int i = 0; (i < reader.FieldCount); i++) {
                System.Type __type;
                string __name;
                __type = reader.GetFieldType(i);
                __name = reader.GetName(i);
                table.Columns.Add(__name, __type);
            }

            DataRow result;
            if (reader.Read()) {
                System.Data.DataRow row = table.NewRow();
                object[] rowdata = new object[reader.FieldCount];
                reader.GetValues(rowdata);
                row.ItemArray = rowdata;
                result = row;
            } else {
                result = null;
            }
            reader.Close();
            connection.Close();
            return result;
        }
        public static int MenuItemsDel(Int32 id) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("MenuItemsDel", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@id"].Value = id;
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            return result;
        }
        public static DataTable MenuItemsGetAll() {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("MenuItemsGetAll", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            System.Data.DataTable table = new DataTable();

            for (int i = 0; (i < reader.FieldCount); i++) {
                System.Type __type;
                string __name;
                __type = reader.GetFieldType(i);
                __name = reader.GetName(i);
                table.Columns.Add(__name, __type);
            }

            while (reader.Read()) {
                System.Data.DataRow row = table.NewRow();
                object[] rowdata = new object[reader.FieldCount];
                reader.GetValues(rowdata);
                row.ItemArray = rowdata;
                table.Rows.Add(row);
            }
            reader.Close();
            DataTable result = table;
            connection.Close();
            return result;
        }
        public static DataRow MenuItemsGetById(Int32 id) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("MenuItemsGetById", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@id"].Value = id;
            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            System.Data.DataTable table = new DataTable();

            for (int i = 0; (i < reader.FieldCount); i++) {
                System.Type __type;
                string __name;
                __type = reader.GetFieldType(i);
                __name = reader.GetName(i);
                table.Columns.Add(__name, __type);
            }

            DataRow result;
            if (reader.Read()) {
                System.Data.DataRow row = table.NewRow();
                object[] rowdata = new object[reader.FieldCount];
                reader.GetValues(rowdata);
                row.ItemArray = rowdata;
                result = row;
            } else {
                result = null;
            }
            reader.Close();
            connection.Close();
            return result;
        }
        public static DataTable MenuItemsGetByParent(Int32 parent_id) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("MenuItemsGetByParent", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@parent_id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@parent_id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@parent_id"].Value = parent_id;
            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            System.Data.DataTable table = new DataTable();

            for (int i = 0; (i < reader.FieldCount); i++) {
                System.Type __type;
                string __name;
                __type = reader.GetFieldType(i);
                __name = reader.GetName(i);
                table.Columns.Add(__name, __type);
            }

            while (reader.Read()) {
                System.Data.DataRow row = table.NewRow();
                object[] rowdata = new object[reader.FieldCount];
                reader.GetValues(rowdata);
                row.ItemArray = rowdata;
                table.Rows.Add(row);
            }
            reader.Close();
            DataTable result = table;
            connection.Close();
            return result;
        }
        public static int MenuItemsUpdate(Int32 id, Int32 parent_id, String url, Int32 sort, String name, Byte new_window) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("MenuItemsUpdate", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@id"].Value = id;
            cmd.Parameters.Add("@parent_id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@parent_id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@parent_id"].Value = parent_id;
            cmd.Parameters.Add("@url", System.Data.SqlDbType.NVarChar, 256);
            cmd.Parameters["@url"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@url"].Value = url;
            cmd.Parameters.Add("@sort", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@sort"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@sort"].Value = sort;
            cmd.Parameters.Add("@name", System.Data.SqlDbType.NVarChar, 32);
            cmd.Parameters["@name"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@name"].Value = name;
            cmd.Parameters.Add("@new_window", System.Data.SqlDbType.TinyInt, 0);
            cmd.Parameters["@new_window"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@new_window"].Value = new_window;
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            return result;
        }
        public static DataRow MessageAdd(Int32 receiver_id, Int32 sender_id, String title, String text) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("MessageAdd", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@receiver_id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@receiver_id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@receiver_id"].Value = receiver_id;
            cmd.Parameters.Add("@sender_id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@sender_id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@sender_id"].Value = sender_id;
            cmd.Parameters.Add("@title", System.Data.SqlDbType.NVarChar, 64);
            cmd.Parameters["@title"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@title"].Value = title;
            cmd.Parameters.Add("@text", System.Data.SqlDbType.NVarChar, 1024);
            cmd.Parameters["@text"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@text"].Value = text;
            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            System.Data.DataTable table = new DataTable();

            for (int i = 0; (i < reader.FieldCount); i++) {
                System.Type __type;
                string __name;
                __type = reader.GetFieldType(i);
                __name = reader.GetName(i);
                table.Columns.Add(__name, __type);
            }

            DataRow result;
            if (reader.Read()) {
                System.Data.DataRow row = table.NewRow();
                object[] rowdata = new object[reader.FieldCount];
                reader.GetValues(rowdata);
                row.ItemArray = rowdata;
                result = row;
            } else {
                result = null;
            }
            reader.Close();
            connection.Close();
            return result;
        }
        public static int MessageDelByReceiver(Int32 mess_id) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("MessageDelByReceiver", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@mess_id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@mess_id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@mess_id"].Value = mess_id;
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            return result;
        }
        public static int MessageDelBySender(Int32 mess_id) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("MessageDelBySender", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@mess_id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@mess_id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@mess_id"].Value = mess_id;
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            return result;
        }
        public static DataRow MessageGetById(Int32 id) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("MessageGetById", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@id"].Value = id;
            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            System.Data.DataTable table = new DataTable();

            for (int i = 0; (i < reader.FieldCount); i++) {
                System.Type __type;
                string __name;
                __type = reader.GetFieldType(i);
                __name = reader.GetName(i);
                table.Columns.Add(__name, __type);
            }

            DataRow result;
            if (reader.Read()) {
                System.Data.DataRow row = table.NewRow();
                object[] rowdata = new object[reader.FieldCount];
                reader.GetValues(rowdata);
                row.ItemArray = rowdata;
                result = row;
            } else {
                result = null;
            }
            reader.Close();
            connection.Close();
            return result;
        }
        public static DataTable MessageGetByReceiver(Int32 rec_id, Int32 page, Int32 count, ref Int32 mess_count) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("MessageGetByReceiver", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@rec_id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@rec_id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@rec_id"].Value = rec_id;
            cmd.Parameters.Add("@page", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@page"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@page"].Value = page;
            cmd.Parameters.Add("@count", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@count"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@count"].Value = count;
            cmd.Parameters.Add("@mess_count", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@mess_count"].Direction = System.Data.ParameterDirection.InputOutput;
            cmd.Parameters["@mess_count"].Value = mess_count;
            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            System.Data.DataTable table = new DataTable();

            for (int i = 0; (i < reader.FieldCount); i++) {
                System.Type __type;
                string __name;
                __type = reader.GetFieldType(i);
                __name = reader.GetName(i);
                table.Columns.Add(__name, __type);
            }

            while (reader.Read()) {
                System.Data.DataRow row = table.NewRow();
                object[] rowdata = new object[reader.FieldCount];
                reader.GetValues(rowdata);
                row.ItemArray = rowdata;
                table.Rows.Add(row);
            }
            reader.Close();
            DataTable result = table;
            mess_count = (Int32)(cmd.Parameters["@mess_count"].Value);
            connection.Close();
            return result;
        }
        public static DataTable MessageGetBySender(Int32 sender_id, Int32 page, Int32 count, ref Int32 mess_count) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("MessageGetBySender", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@sender_id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@sender_id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@sender_id"].Value = sender_id;
            cmd.Parameters.Add("@page", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@page"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@page"].Value = page;
            cmd.Parameters.Add("@count", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@count"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@count"].Value = count;
            cmd.Parameters.Add("@mess_count", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@mess_count"].Direction = System.Data.ParameterDirection.InputOutput;
            cmd.Parameters["@mess_count"].Value = mess_count;
            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            System.Data.DataTable table = new DataTable();

            for (int i = 0; (i < reader.FieldCount); i++) {
                System.Type __type;
                string __name;
                __type = reader.GetFieldType(i);
                __name = reader.GetName(i);
                table.Columns.Add(__name, __type);
            }

            while (reader.Read()) {
                System.Data.DataRow row = table.NewRow();
                object[] rowdata = new object[reader.FieldCount];
                reader.GetValues(rowdata);
                row.ItemArray = rowdata;
                table.Rows.Add(row);
            }
            reader.Close();
            DataTable result = table;
            mess_count = (Int32)(cmd.Parameters["@mess_count"].Value);
            connection.Close();
            return result;
        }
        public static object MessageGetNewCount(Int32 receiver_id) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("MessageGetNewCount", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@receiver_id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@receiver_id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@receiver_id"].Value = receiver_id;
            object result = cmd.ExecuteScalar();
            connection.Close();
            return result;
        }
        public static int MessageMarkAsRead(Int32 id) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("MessageMarkAsRead", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@id"].Value = id;
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            return result;
        }
        public static DataRow NotesAdd(Int32 user_id, String title, String text, DateTime cdate) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("NotesAdd", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@user_id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@user_id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@user_id"].Value = user_id;
            cmd.Parameters.Add("@title", System.Data.SqlDbType.NVarChar, 256);
            cmd.Parameters["@title"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@title"].Value = title;
            cmd.Parameters.Add("@text", System.Data.SqlDbType.NVarChar, 1024);
            cmd.Parameters["@text"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@text"].Value = text;
            cmd.Parameters.Add("@cdate", System.Data.SqlDbType.DateTime, 0);
            cmd.Parameters["@cdate"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@cdate"].Value = cdate;
            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            System.Data.DataTable table = new DataTable();

            for (int i = 0; (i < reader.FieldCount); i++) {
                System.Type __type;
                string __name;
                __type = reader.GetFieldType(i);
                __name = reader.GetName(i);
                table.Columns.Add(__name, __type);
            }

            DataRow result;
            if (reader.Read()) {
                System.Data.DataRow row = table.NewRow();
                object[] rowdata = new object[reader.FieldCount];
                reader.GetValues(rowdata);
                row.ItemArray = rowdata;
                result = row;
            } else {
                result = null;
            }
            reader.Close();
            connection.Close();
            return result;
        }
        public static int NotesDel(Int32 id) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("NotesDel", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@id"].Value = id;
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            return result;
        }
        public static DataTable NotesGet(Int32 user_id, Int32 page, Int32 count, ref Int32 posts_count) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("NotesGet", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@user_id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@user_id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@user_id"].Value = user_id;
            cmd.Parameters.Add("@page", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@page"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@page"].Value = page;
            cmd.Parameters.Add("@count", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@count"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@count"].Value = count;
            cmd.Parameters.Add("@posts_count", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@posts_count"].Direction = System.Data.ParameterDirection.InputOutput;
            cmd.Parameters["@posts_count"].Value = posts_count;
            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            System.Data.DataTable table = new DataTable();

            for (int i = 0; (i < reader.FieldCount); i++) {
                System.Type __type;
                string __name;
                __type = reader.GetFieldType(i);
                __name = reader.GetName(i);
                table.Columns.Add(__name, __type);
            }

            while (reader.Read()) {
                System.Data.DataRow row = table.NewRow();
                object[] rowdata = new object[reader.FieldCount];
                reader.GetValues(rowdata);
                row.ItemArray = rowdata;
                table.Rows.Add(row);
            }
            reader.Close();
            DataTable result = table;
            posts_count = (Int32)(cmd.Parameters["@posts_count"].Value);
            connection.Close();
            return result;
        }
        public static DataRow NotesGetById(Int32 id) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("NotesGetById", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@id"].Value = id;
            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            System.Data.DataTable table = new DataTable();

            for (int i = 0; (i < reader.FieldCount); i++) {
                System.Type __type;
                string __name;
                __type = reader.GetFieldType(i);
                __name = reader.GetName(i);
                table.Columns.Add(__name, __type);
            }

            DataRow result;
            if (reader.Read()) {
                System.Data.DataRow row = table.NewRow();
                object[] rowdata = new object[reader.FieldCount];
                reader.GetValues(rowdata);
                row.ItemArray = rowdata;
                result = row;
            } else {
                result = null;
            }
            reader.Close();
            connection.Close();
            return result;
        }
        public static DataRow PollAdd(String topic, Int32 author_id, Int32 is_multiselect, Int32 is_open, String answers) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("PollAdd", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@topic", System.Data.SqlDbType.NVarChar, 120);
            cmd.Parameters["@topic"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@topic"].Value = topic;
            cmd.Parameters.Add("@author_id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@author_id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@author_id"].Value = author_id;
            cmd.Parameters.Add("@is_multiselect", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@is_multiselect"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@is_multiselect"].Value = is_multiselect;
            cmd.Parameters.Add("@is_open", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@is_open"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@is_open"].Value = is_open;
            cmd.Parameters.Add("@answers", System.Data.SqlDbType.NVarChar, 1024);
            cmd.Parameters["@answers"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@answers"].Value = answers;
            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            System.Data.DataTable table = new DataTable();

            for (int i = 0; (i < reader.FieldCount); i++) {
                System.Type __type;
                string __name;
                __type = reader.GetFieldType(i);
                __name = reader.GetName(i);
                table.Columns.Add(__name, __type);
            }

            DataRow result;
            if (reader.Read()) {
                System.Data.DataRow row = table.NewRow();
                object[] rowdata = new object[reader.FieldCount];
                reader.GetValues(rowdata);
                row.ItemArray = rowdata;
                result = row;
            } else {
                result = null;
            }
            reader.Close();
            connection.Close();
            return result;
        }
        public static int PollDel(Int32 poll_id) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("PollDel", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@poll_id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@poll_id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@poll_id"].Value = poll_id;
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            return result;
        }
        public static DataTable PollGet(Int32 page, Int32 count, ref Int32 polls_count) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("PollGet", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@page", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@page"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@page"].Value = page;
            cmd.Parameters.Add("@count", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@count"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@count"].Value = count;
            cmd.Parameters.Add("@polls_count", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@polls_count"].Direction = System.Data.ParameterDirection.InputOutput;
            cmd.Parameters["@polls_count"].Value = polls_count;
            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            System.Data.DataTable table = new DataTable();

            for (int i = 0; (i < reader.FieldCount); i++) {
                System.Type __type;
                string __name;
                __type = reader.GetFieldType(i);
                __name = reader.GetName(i);
                table.Columns.Add(__name, __type);
            }

            while (reader.Read()) {
                System.Data.DataRow row = table.NewRow();
                object[] rowdata = new object[reader.FieldCount];
                reader.GetValues(rowdata);
                row.ItemArray = rowdata;
                table.Rows.Add(row);
            }
            reader.Close();
            DataTable result = table;
            polls_count = (Int32)(cmd.Parameters["@polls_count"].Value);
            connection.Close();
            return result;
        }
        public static DataTable PollGetAnswers(Int32 poll_id) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("PollGetAnswers", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@poll_id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@poll_id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@poll_id"].Value = poll_id;
            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            System.Data.DataTable table = new DataTable();

            for (int i = 0; (i < reader.FieldCount); i++) {
                System.Type __type;
                string __name;
                __type = reader.GetFieldType(i);
                __name = reader.GetName(i);
                table.Columns.Add(__name, __type);
            }

            while (reader.Read()) {
                System.Data.DataRow row = table.NewRow();
                object[] rowdata = new object[reader.FieldCount];
                reader.GetValues(rowdata);
                row.ItemArray = rowdata;
                table.Rows.Add(row);
            }
            reader.Close();
            DataTable result = table;
            connection.Close();
            return result;
        }
        public static DataTable PollGetAnswerVoters(Int32 answer_id) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("PollGetAnswerVoters", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@answer_id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@answer_id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@answer_id"].Value = answer_id;
            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            System.Data.DataTable table = new DataTable();

            for (int i = 0; (i < reader.FieldCount); i++) {
                System.Type __type;
                string __name;
                __type = reader.GetFieldType(i);
                __name = reader.GetName(i);
                table.Columns.Add(__name, __type);
            }

            while (reader.Read()) {
                System.Data.DataRow row = table.NewRow();
                object[] rowdata = new object[reader.FieldCount];
                reader.GetValues(rowdata);
                row.ItemArray = rowdata;
                table.Rows.Add(row);
            }
            reader.Close();
            DataTable result = table;
            connection.Close();
            return result;
        }
        public static DataRow PollGetById(Int32 id) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("PollGetById", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@id"].Value = id;
            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            System.Data.DataTable table = new DataTable();

            for (int i = 0; (i < reader.FieldCount); i++) {
                System.Type __type;
                string __name;
                __type = reader.GetFieldType(i);
                __name = reader.GetName(i);
                table.Columns.Add(__name, __type);
            }

            DataRow result;
            if (reader.Read()) {
                System.Data.DataRow row = table.NewRow();
                object[] rowdata = new object[reader.FieldCount];
                reader.GetValues(rowdata);
                row.ItemArray = rowdata;
                result = row;
            } else {
                result = null;
            }
            reader.Close();
            connection.Close();
            return result;
        }
        public static DataRow PollGetLast() {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("PollGetLast", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            System.Data.DataTable table = new DataTable();

            for (int i = 0; (i < reader.FieldCount); i++) {
                System.Type __type;
                string __name;
                __type = reader.GetFieldType(i);
                __name = reader.GetName(i);
                table.Columns.Add(__name, __type);
            }

            DataRow result;
            if (reader.Read()) {
                System.Data.DataRow row = table.NewRow();
                object[] rowdata = new object[reader.FieldCount];
                reader.GetValues(rowdata);
                row.ItemArray = rowdata;
                result = row;
            } else {
                result = null;
            }
            reader.Close();
            connection.Close();
            return result;
        }
        public static int PollIsUserVoted(Int32 user_id, Int32 poll_id) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("PollIsUserVoted", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@user_id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@user_id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@user_id"].Value = user_id;
            cmd.Parameters.Add("@poll_id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@poll_id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@poll_id"].Value = poll_id;
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            return result;
        }
        public static int PollVote(Int32 poll_id, Int32 user_id, String answers) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("PollVote", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@poll_id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@poll_id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@poll_id"].Value = poll_id;
            cmd.Parameters.Add("@user_id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@user_id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@user_id"].Value = user_id;
            cmd.Parameters.Add("@answers", System.Data.SqlDbType.NVarChar, 1024);
            cmd.Parameters["@answers"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@answers"].Value = answers;
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            return result;
        }
        public static DataRow PostAdd(String title, String desc, String text, Byte attached, String source, Int32 user_id) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("PostAdd", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@title", System.Data.SqlDbType.NVarChar, 128);
            cmd.Parameters["@title"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@title"].Value = title;
            cmd.Parameters.Add("@desc", System.Data.SqlDbType.NVarChar, -1);
            cmd.Parameters["@desc"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@desc"].Value = desc;
            cmd.Parameters.Add("@text", System.Data.SqlDbType.NVarChar, -1);
            cmd.Parameters["@text"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@text"].Value = text;
            cmd.Parameters.Add("@attached", System.Data.SqlDbType.TinyInt, 0);
            cmd.Parameters["@attached"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@attached"].Value = attached;
            cmd.Parameters.Add("@source", System.Data.SqlDbType.NVarChar, 1024);
            cmd.Parameters["@source"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@source"].Value = source;
            cmd.Parameters.Add("@user_id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@user_id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@user_id"].Value = user_id;
            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            System.Data.DataTable table = new DataTable();

            for (int i = 0; (i < reader.FieldCount); i++) {
                System.Type __type;
                string __name;
                __type = reader.GetFieldType(i);
                __name = reader.GetName(i);
                table.Columns.Add(__name, __type);
            }

            DataRow result;
            if (reader.Read()) {
                System.Data.DataRow row = table.NewRow();
                object[] rowdata = new object[reader.FieldCount];
                reader.GetValues(rowdata);
                row.ItemArray = rowdata;
                result = row;
            } else {
                result = null;
            }
            reader.Close();
            connection.Close();
            return result;
        }
        public static int PostAttachCategories(Int32 post_id, String query) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("PostAttachCategories", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@post_id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@post_id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@post_id"].Value = post_id;
            cmd.Parameters.Add("@query", System.Data.SqlDbType.NVarChar, 1024);
            cmd.Parameters["@query"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@query"].Value = query;
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            return result;
        }
        public static int PostDel(Int32 id) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("PostDel", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@id"].Value = id;
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            return result;
        }
        public static DataTable PostGet(Int32 page, Int32 count, ref Int32 posts_count) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("PostGet", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@page", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@page"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@page"].Value = page;
            cmd.Parameters.Add("@count", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@count"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@count"].Value = count;
            cmd.Parameters.Add("@posts_count", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@posts_count"].Direction = System.Data.ParameterDirection.InputOutput;
            cmd.Parameters["@posts_count"].Value = posts_count;
            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            System.Data.DataTable table = new DataTable();

            for (int i = 0; (i < reader.FieldCount); i++) {
                System.Type __type;
                string __name;
                __type = reader.GetFieldType(i);
                __name = reader.GetName(i);
                table.Columns.Add(__name, __type);
            }

            while (reader.Read()) {
                System.Data.DataRow row = table.NewRow();
                object[] rowdata = new object[reader.FieldCount];
                reader.GetValues(rowdata);
                row.ItemArray = rowdata;
                table.Rows.Add(row);
            }
            reader.Close();
            DataTable result = table;
            posts_count = (Int32)(cmd.Parameters["@posts_count"].Value);
            connection.Close();
            return result;
        }
        public static DataTable PostGetAttached() {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("PostGetAttached", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            System.Data.DataTable table = new DataTable();

            for (int i = 0; (i < reader.FieldCount); i++) {
                System.Type __type;
                string __name;
                __type = reader.GetFieldType(i);
                __name = reader.GetName(i);
                table.Columns.Add(__name, __type);
            }

            while (reader.Read()) {
                System.Data.DataRow row = table.NewRow();
                object[] rowdata = new object[reader.FieldCount];
                reader.GetValues(rowdata);
                row.ItemArray = rowdata;
                table.Rows.Add(row);
            }
            reader.Close();
            DataTable result = table;
            connection.Close();
            return result;
        }
        public static DataTable PostGetByCat(Int32 page, Int32 count, Int32 cat_id, ref Int32 posts_count) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("PostGetByCat", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@page", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@page"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@page"].Value = page;
            cmd.Parameters.Add("@count", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@count"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@count"].Value = count;
            cmd.Parameters.Add("@cat_id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@cat_id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@cat_id"].Value = cat_id;
            cmd.Parameters.Add("@posts_count", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@posts_count"].Direction = System.Data.ParameterDirection.InputOutput;
            cmd.Parameters["@posts_count"].Value = posts_count;
            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            System.Data.DataTable table = new DataTable();

            for (int i = 0; (i < reader.FieldCount); i++) {
                System.Type __type;
                string __name;
                __type = reader.GetFieldType(i);
                __name = reader.GetName(i);
                table.Columns.Add(__name, __type);
            }

            while (reader.Read()) {
                System.Data.DataRow row = table.NewRow();
                object[] rowdata = new object[reader.FieldCount];
                reader.GetValues(rowdata);
                row.ItemArray = rowdata;
                table.Rows.Add(row);
            }
            reader.Close();
            DataTable result = table;
            posts_count = (Int32)(cmd.Parameters["@posts_count"].Value);
            connection.Close();
            return result;
        }
        public static DataRow PostGetById(Int32 id) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("PostGetById", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@id"].Value = id;
            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            System.Data.DataTable table = new DataTable();

            for (int i = 0; (i < reader.FieldCount); i++) {
                System.Type __type;
                string __name;
                __type = reader.GetFieldType(i);
                __name = reader.GetName(i);
                table.Columns.Add(__name, __type);
            }

            DataRow result;
            if (reader.Read()) {
                System.Data.DataRow row = table.NewRow();
                object[] rowdata = new object[reader.FieldCount];
                reader.GetValues(rowdata);
                row.ItemArray = rowdata;
                result = row;
            } else {
                result = null;
            }
            reader.Close();
            connection.Close();
            return result;
        }
        public static DataTable PostGetCategories(Int32 post_id) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("PostGetCategories", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@post_id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@post_id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@post_id"].Value = post_id;
            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            System.Data.DataTable table = new DataTable();

            for (int i = 0; (i < reader.FieldCount); i++) {
                System.Type __type;
                string __name;
                __type = reader.GetFieldType(i);
                __name = reader.GetName(i);
                table.Columns.Add(__name, __type);
            }

            while (reader.Read()) {
                System.Data.DataRow row = table.NewRow();
                object[] rowdata = new object[reader.FieldCount];
                reader.GetValues(rowdata);
                row.ItemArray = rowdata;
                table.Rows.Add(row);
            }
            reader.Close();
            DataTable result = table;
            connection.Close();
            return result;
        }
        public static DataTable PostGetLast(Int32 count) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("PostGetLast", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@count", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@count"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@count"].Value = count;
            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            System.Data.DataTable table = new DataTable();

            for (int i = 0; (i < reader.FieldCount); i++) {
                System.Type __type;
                string __name;
                __type = reader.GetFieldType(i);
                __name = reader.GetName(i);
                table.Columns.Add(__name, __type);
            }

            while (reader.Read()) {
                System.Data.DataRow row = table.NewRow();
                object[] rowdata = new object[reader.FieldCount];
                reader.GetValues(rowdata);
                row.ItemArray = rowdata;
                table.Rows.Add(row);
            }
            reader.Close();
            DataTable result = table;
            connection.Close();
            return result;
        }
        public static DataTable PostGetTop(Int32 period, Int32 count) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("PostGetTop", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@period", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@period"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@period"].Value = period;
            cmd.Parameters.Add("@count", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@count"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@count"].Value = count;
            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            System.Data.DataTable table = new DataTable();

            for (int i = 0; (i < reader.FieldCount); i++) {
                System.Type __type;
                string __name;
                __type = reader.GetFieldType(i);
                __name = reader.GetName(i);
                table.Columns.Add(__name, __type);
            }

            while (reader.Read()) {
                System.Data.DataRow row = table.NewRow();
                object[] rowdata = new object[reader.FieldCount];
                reader.GetValues(rowdata);
                row.ItemArray = rowdata;
                table.Rows.Add(row);
            }
            reader.Close();
            DataTable result = table;
            connection.Close();
            return result;
        }
        public static object PostIsFavorite(Int32 user_id, Int32 post_id) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("PostIsFavorite", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@user_id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@user_id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@user_id"].Value = user_id;
            cmd.Parameters.Add("@post_id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@post_id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@post_id"].Value = post_id;
            object result = cmd.ExecuteScalar();
            connection.Close();
            return result;
        }
        public static DataTable PostSearch(String query, Int32 page, Int32 count, ref Int32 posts_count) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("PostSearch", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@query", System.Data.SqlDbType.NVarChar, 512);
            cmd.Parameters["@query"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@query"].Value = query;
            cmd.Parameters.Add("@page", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@page"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@page"].Value = page;
            cmd.Parameters.Add("@count", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@count"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@count"].Value = count;
            cmd.Parameters.Add("@posts_count", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@posts_count"].Direction = System.Data.ParameterDirection.InputOutput;
            cmd.Parameters["@posts_count"].Value = posts_count;
            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            System.Data.DataTable table = new DataTable();

            for (int i = 0; (i < reader.FieldCount); i++) {
                System.Type __type;
                string __name;
                __type = reader.GetFieldType(i);
                __name = reader.GetName(i);
                table.Columns.Add(__name, __type);
            }

            while (reader.Read()) {
                System.Data.DataRow row = table.NewRow();
                object[] rowdata = new object[reader.FieldCount];
                reader.GetValues(rowdata);
                row.ItemArray = rowdata;
                table.Rows.Add(row);
            }
            reader.Close();
            DataTable result = table;
            posts_count = (Int32)(cmd.Parameters["@posts_count"].Value);
            connection.Close();
            return result;
        }
        public static int PostUpdate(Int32 id, String title, String desc, String text, Byte attached, String source, Int32 comments_count) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("PostUpdate", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@id"].Value = id;
            cmd.Parameters.Add("@title", System.Data.SqlDbType.NVarChar, 128);
            cmd.Parameters["@title"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@title"].Value = title;
            cmd.Parameters.Add("@desc", System.Data.SqlDbType.NVarChar, -1);
            cmd.Parameters["@desc"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@desc"].Value = desc;
            cmd.Parameters.Add("@text", System.Data.SqlDbType.NVarChar, -1);
            cmd.Parameters["@text"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@text"].Value = text;
            cmd.Parameters.Add("@attached", System.Data.SqlDbType.TinyInt, 0);
            cmd.Parameters["@attached"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@attached"].Value = attached;
            cmd.Parameters.Add("@source", System.Data.SqlDbType.NVarChar, 1024);
            cmd.Parameters["@source"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@source"].Value = source;
            cmd.Parameters.Add("@comments_count", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@comments_count"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@comments_count"].Value = comments_count;
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            return result;
        }
        public static int PostUpdateViews(Int32 id) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("PostUpdateViews", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@id"].Value = id;
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            return result;
        }
        public static DataRow RecoveryAdd(String identifier, Int32 user_id) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("RecoveryAdd", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@identifier", System.Data.SqlDbType.NVarChar, 1024);
            cmd.Parameters["@identifier"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@identifier"].Value = identifier;
            cmd.Parameters.Add("@user_id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@user_id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@user_id"].Value = user_id;
            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            System.Data.DataTable table = new DataTable();

            for (int i = 0; (i < reader.FieldCount); i++) {
                System.Type __type;
                string __name;
                __type = reader.GetFieldType(i);
                __name = reader.GetName(i);
                table.Columns.Add(__name, __type);
            }

            DataRow result;
            if (reader.Read()) {
                System.Data.DataRow row = table.NewRow();
                object[] rowdata = new object[reader.FieldCount];
                reader.GetValues(rowdata);
                row.ItemArray = rowdata;
                result = row;
            } else {
                result = null;
            }
            reader.Close();
            connection.Close();
            return result;
        }
        public static int RecoveryDel(String identifier) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("RecoveryDel", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@identifier", System.Data.SqlDbType.NVarChar, 1024);
            cmd.Parameters["@identifier"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@identifier"].Value = identifier;
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            return result;
        }
        public static DataRow RecoveryGetByIdentifier(String identifier) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("RecoveryGetByIdentifier", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@identifier", System.Data.SqlDbType.NVarChar, 1024);
            cmd.Parameters["@identifier"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@identifier"].Value = identifier;
            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            System.Data.DataTable table = new DataTable();

            for (int i = 0; (i < reader.FieldCount); i++) {
                System.Type __type;
                string __name;
                __type = reader.GetFieldType(i);
                __name = reader.GetName(i);
                table.Columns.Add(__name, __type);
            }

            DataRow result;
            if (reader.Read()) {
                System.Data.DataRow row = table.NewRow();
                object[] rowdata = new object[reader.FieldCount];
                reader.GetValues(rowdata);
                row.ItemArray = rowdata;
                result = row;
            } else {
                result = null;
            }
            reader.Close();
            connection.Close();
            return result;
        }
        public static DataTable RfcGetByNum(String num) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("RfcGetByNum", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@num", System.Data.SqlDbType.NVarChar, 16);
            cmd.Parameters["@num"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@num"].Value = num;
            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            System.Data.DataTable table = new DataTable();

            for (int i = 0; (i < reader.FieldCount); i++) {
                System.Type __type;
                string __name;
                __type = reader.GetFieldType(i);
                __name = reader.GetName(i);
                table.Columns.Add(__name, __type);
            }

            while (reader.Read()) {
                System.Data.DataRow row = table.NewRow();
                object[] rowdata = new object[reader.FieldCount];
                reader.GetValues(rowdata);
                row.ItemArray = rowdata;
                table.Rows.Add(row);
            }
            reader.Close();
            DataTable result = table;
            connection.Close();
            return result;
        }
        public static DataTable RfcSearchByTitle(String query) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("RfcSearchByTitle", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@query", System.Data.SqlDbType.NVarChar, 512);
            cmd.Parameters["@query"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@query"].Value = query;
            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            System.Data.DataTable table = new DataTable();

            for (int i = 0; (i < reader.FieldCount); i++) {
                System.Type __type;
                string __name;
                __type = reader.GetFieldType(i);
                __name = reader.GetName(i);
                table.Columns.Add(__name, __type);
            }

            while (reader.Read()) {
                System.Data.DataRow row = table.NewRow();
                object[] rowdata = new object[reader.FieldCount];
                reader.GetValues(rowdata);
                row.ItemArray = rowdata;
                table.Rows.Add(row);
            }
            reader.Close();
            DataTable result = table;
            connection.Close();
            return result;
        }
        public static DataRow UserAdd(String nick, String pass, Byte role, String email) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("UserAdd", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@nick", System.Data.SqlDbType.NVarChar, 32);
            cmd.Parameters["@nick"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@nick"].Value = nick;
            cmd.Parameters.Add("@pass", System.Data.SqlDbType.NVarChar, 1024);
            cmd.Parameters["@pass"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@pass"].Value = pass;
            cmd.Parameters.Add("@role", System.Data.SqlDbType.TinyInt, 0);
            cmd.Parameters["@role"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@role"].Value = role;
            cmd.Parameters.Add("@email", System.Data.SqlDbType.NVarChar, 32);
            cmd.Parameters["@email"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@email"].Value = email;
            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            System.Data.DataTable table = new DataTable();

            for (int i = 0; (i < reader.FieldCount); i++) {
                System.Type __type;
                string __name;
                __type = reader.GetFieldType(i);
                __name = reader.GetName(i);
                table.Columns.Add(__name, __type);
            }

            DataRow result;
            if (reader.Read()) {
                System.Data.DataRow row = table.NewRow();
                object[] rowdata = new object[reader.FieldCount];
                reader.GetValues(rowdata);
                row.ItemArray = rowdata;
                result = row;
            } else {
                result = null;
            }
            reader.Close();
            connection.Close();
            return result;
        }
        public static int UserDel(Int32 userId) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("UserDel", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@userId", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@userId"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@userId"].Value = userId;
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            return result;
        }
        public static DataRow UserGetByEmail(String email) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("UserGetByEmail", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@email", System.Data.SqlDbType.NVarChar, 512);
            cmd.Parameters["@email"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@email"].Value = email;
            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            System.Data.DataTable table = new DataTable();

            for (int i = 0; (i < reader.FieldCount); i++) {
                System.Type __type;
                string __name;
                __type = reader.GetFieldType(i);
                __name = reader.GetName(i);
                table.Columns.Add(__name, __type);
            }

            DataRow result;
            if (reader.Read()) {
                System.Data.DataRow row = table.NewRow();
                object[] rowdata = new object[reader.FieldCount];
                reader.GetValues(rowdata);
                row.ItemArray = rowdata;
                result = row;
            } else {
                result = null;
            }
            reader.Close();
            connection.Close();
            return result;
        }
        public static DataRow UserGetById(Int32 userId) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("UserGetById", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@userId", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@userId"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@userId"].Value = userId;
            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            System.Data.DataTable table = new DataTable();

            for (int i = 0; (i < reader.FieldCount); i++) {
                System.Type __type;
                string __name;
                __type = reader.GetFieldType(i);
                __name = reader.GetName(i);
                table.Columns.Add(__name, __type);
            }

            DataRow result;
            if (reader.Read()) {
                System.Data.DataRow row = table.NewRow();
                object[] rowdata = new object[reader.FieldCount];
                reader.GetValues(rowdata);
                row.ItemArray = rowdata;
                result = row;
            } else {
                result = null;
            }
            reader.Close();
            connection.Close();
            return result;
        }
        public static DataRow UserGetByLogin(String login) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("UserGetByLogin", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@login", System.Data.SqlDbType.NVarChar, 32);
            cmd.Parameters["@login"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@login"].Value = login;
            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            System.Data.DataTable table = new DataTable();

            for (int i = 0; (i < reader.FieldCount); i++) {
                System.Type __type;
                string __name;
                __type = reader.GetFieldType(i);
                __name = reader.GetName(i);
                table.Columns.Add(__name, __type);
            }

            DataRow result;
            if (reader.Read()) {
                System.Data.DataRow row = table.NewRow();
                object[] rowdata = new object[reader.FieldCount];
                reader.GetValues(rowdata);
                row.ItemArray = rowdata;
                result = row;
            } else {
                result = null;
            }
            reader.Close();
            connection.Close();
            return result;
        }
        public static DataTable UserGetByRole(Int32 role) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("UserGetByRole", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@role", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@role"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@role"].Value = role;
            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            System.Data.DataTable table = new DataTable();

            for (int i = 0; (i < reader.FieldCount); i++) {
                System.Type __type;
                string __name;
                __type = reader.GetFieldType(i);
                __name = reader.GetName(i);
                table.Columns.Add(__name, __type);
            }

            while (reader.Read()) {
                System.Data.DataRow row = table.NewRow();
                object[] rowdata = new object[reader.FieldCount];
                reader.GetValues(rowdata);
                row.ItemArray = rowdata;
                table.Rows.Add(row);
            }
            reader.Close();
            DataTable result = table;
            connection.Close();
            return result;
        }
        public static DataTable UserGetLastRegistered(Int32 count) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("UserGetLastRegistered", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@count", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@count"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@count"].Value = count;
            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            System.Data.DataTable table = new DataTable();

            for (int i = 0; (i < reader.FieldCount); i++) {
                System.Type __type;
                string __name;
                __type = reader.GetFieldType(i);
                __name = reader.GetName(i);
                table.Columns.Add(__name, __type);
            }

            while (reader.Read()) {
                System.Data.DataRow row = table.NewRow();
                object[] rowdata = new object[reader.FieldCount];
                reader.GetValues(rowdata);
                row.ItemArray = rowdata;
                table.Rows.Add(row);
            }
            reader.Close();
            DataTable result = table;
            connection.Close();
            return result;
        }
        public static DataTable UserGetStat() {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("UserGetStat", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            System.Data.DataTable table = new DataTable();

            for (int i = 0; (i < reader.FieldCount); i++) {
                System.Type __type;
                string __name;
                __type = reader.GetFieldType(i);
                __name = reader.GetName(i);
                table.Columns.Add(__name, __type);
            }

            while (reader.Read()) {
                System.Data.DataRow row = table.NewRow();
                object[] rowdata = new object[reader.FieldCount];
                reader.GetValues(rowdata);
                row.ItemArray = rowdata;
                table.Rows.Add(row);
            }
            reader.Close();
            DataTable result = table;
            connection.Close();
            return result;
        }
        public static DataTable UserGetTopPosters(Int32 count) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("UserGetTopPosters", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@count", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@count"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@count"].Value = count;
            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
            System.Data.DataTable table = new DataTable();

            for (int i = 0; (i < reader.FieldCount); i++) {
                System.Type __type;
                string __name;
                __type = reader.GetFieldType(i);
                __name = reader.GetName(i);
                table.Columns.Add(__name, __type);
            }

            while (reader.Read()) {
                System.Data.DataRow row = table.NewRow();
                object[] rowdata = new object[reader.FieldCount];
                reader.GetValues(rowdata);
                row.ItemArray = rowdata;
                table.Rows.Add(row);
            }
            reader.Close();
            DataTable result = table;
            connection.Close();
            return result;
        }
        public static int UserUpdate(Int32 user_id, String pass, Byte role, String email) {
            SqlConnection connection = OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("UserUpdate", connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add("@user_id", System.Data.SqlDbType.Int, 0);
            cmd.Parameters["@user_id"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@user_id"].Value = user_id;
            cmd.Parameters.Add("@pass", System.Data.SqlDbType.NVarChar, 1024);
            cmd.Parameters["@pass"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@pass"].Value = pass;
            cmd.Parameters.Add("@role", System.Data.SqlDbType.TinyInt, 0);
            cmd.Parameters["@role"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@role"].Value = role;
            cmd.Parameters.Add("@email", System.Data.SqlDbType.NVarChar, 32);
            cmd.Parameters["@email"].Direction = System.Data.ParameterDirection.Input;
            cmd.Parameters["@email"].Value = email;
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            return result;
        }
    }
}