using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
using System.Web;
public class Database
{
    private static SqlConnection OpenConnection()
    {
        string connectionString;
        if (ConfigurationManager.ConnectionStrings[Environment.MachineName] != null)
        {
            connectionString = ConfigurationManager.ConnectionStrings[Environment.MachineName].ConnectionString;
        }
        else
        {
            connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }
        SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        return connection;
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

    public static DataRow CategoryAdd(String name, Int32 sort)
    {
        SqlConnection connection = OpenConnection();
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("CategoryAdd", connection);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.Add("@name", System.Data.SqlDbType.VarChar, 32);
        cmd.Parameters["@name"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@name"].Value = name;
        cmd.Parameters.Add("@sort", System.Data.SqlDbType.Int, 0);
        cmd.Parameters["@sort"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@sort"].Value = sort;
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        System.Data.DataTable table = new DataTable();

        for (int i = 0; (i < reader.FieldCount); i++)
        {
            System.Type __type;
            string __name;
            __type = reader.GetFieldType(i);
            __name = reader.GetName(i);
            table.Columns.Add(__name, __type);
        }

        DataRow result;
        if (reader.Read())
        {
            System.Data.DataRow row = table.NewRow();
            object[] rowdata = new object[reader.FieldCount];
            reader.GetValues(rowdata);
            row.ItemArray = rowdata;
            result = row;
        }
        else
        {
            result = null;
        }
        reader.Close();
        connection.Close();
        return result;
    }
    public static int CategoryDel(Int32 id)
    {
        SqlConnection connection = OpenConnection();
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("CategoryDel", connection);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.Add("@id", System.Data.SqlDbType.Int, 0);
        cmd.Parameters["@id"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@id"].Value = id; int result = cmd.ExecuteNonQuery();
        connection.Close();
        return result;
    }
    public static DataTable CategoryGetAll()
    {
        SqlConnection connection = OpenConnection();
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("CategoryGetAll", connection);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        System.Data.DataTable table = new DataTable();

        for (int i = 0; (i < reader.FieldCount); i++)
        {
            System.Type __type;
            string __name;
            __type = reader.GetFieldType(i);
            __name = reader.GetName(i);
            table.Columns.Add(__name, __type);
        }

        while (reader.Read())
        {
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
    public static DataRow CategoryGetById(Int32 id)
    {
        SqlConnection connection = OpenConnection();
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("CategoryGetById", connection);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.Add("@id", System.Data.SqlDbType.Int, 0);
        cmd.Parameters["@id"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@id"].Value = id;
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        System.Data.DataTable table = new DataTable();

        for (int i = 0; (i < reader.FieldCount); i++)
        {
            System.Type __type;
            string __name;
            __type = reader.GetFieldType(i);
            __name = reader.GetName(i);
            table.Columns.Add(__name, __type);
        }

        DataRow result;
        if (reader.Read())
        {
            System.Data.DataRow row = table.NewRow();
            object[] rowdata = new object[reader.FieldCount];
            reader.GetValues(rowdata);
            row.ItemArray = rowdata;
            result = row;
        }
        else
        {
            result = null;
        }
        reader.Close();
        connection.Close();
        return result;
    }
    public static DataRow CommentAdd(Int32 post_id, Int32 user_id, String ip)
    {
        SqlConnection connection = OpenConnection();
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("CommentAdd", connection);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.Add("@post_id", System.Data.SqlDbType.Int, 0);
        cmd.Parameters["@post_id"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@post_id"].Value = post_id;
        cmd.Parameters.Add("@user_id", System.Data.SqlDbType.Int, 0);
        cmd.Parameters["@user_id"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@user_id"].Value = user_id;
        cmd.Parameters.Add("@ip", System.Data.SqlDbType.VarChar, 50);
        cmd.Parameters["@ip"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@ip"].Value = ip;
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        System.Data.DataTable table = new DataTable();

        for (int i = 0; (i < reader.FieldCount); i++)
        {
            System.Type __type;
            string __name;
            __type = reader.GetFieldType(i);
            __name = reader.GetName(i);
            table.Columns.Add(__name, __type);
        }

        DataRow result;
        if (reader.Read())
        {
            System.Data.DataRow row = table.NewRow();
            object[] rowdata = new object[reader.FieldCount];
            reader.GetValues(rowdata);
            row.ItemArray = rowdata;
            result = row;
        }
        else
        {
            result = null;
        }
        reader.Close();
        connection.Close();
        return result;
    }
    public static int CommentDel(Int32 id)
    {
        SqlConnection connection = OpenConnection();
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("CommentDel", connection);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.Add("@id", System.Data.SqlDbType.Int, 0);
        cmd.Parameters["@id"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@id"].Value = id; int result = cmd.ExecuteNonQuery();
        connection.Close();
        return result;
    }
    public static DataTable CommentGetByPost(Int32 id)
    {
        SqlConnection connection = OpenConnection();
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("CommentGetByPost", connection);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.Add("@id", System.Data.SqlDbType.Int, 0);
        cmd.Parameters["@id"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@id"].Value = id;
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        System.Data.DataTable table = new DataTable();

        for (int i = 0; (i < reader.FieldCount); i++)
        {
            System.Type __type;
            string __name;
            __type = reader.GetFieldType(i);
            __name = reader.GetName(i);
            table.Columns.Add(__name, __type);
        }

        while (reader.Read())
        {
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
    public static DataTable CommentGetLast(Int32 count)
    {
        SqlConnection connection = OpenConnection();
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("CommentGetLast", connection);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.Add("@count", System.Data.SqlDbType.Int, 0);
        cmd.Parameters["@count"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@count"].Value = count;
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        System.Data.DataTable table = new DataTable();

        for (int i = 0; (i < reader.FieldCount); i++)
        {
            System.Type __type;
            string __name;
            __type = reader.GetFieldType(i);
            __name = reader.GetName(i);
            table.Columns.Add(__name, __type);
        }

        while (reader.Read())
        {
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
    public static DataRow MenuItemsAdd(Int32 parent_id, String url, Int32 sort)
    {
        SqlConnection connection = OpenConnection();
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("MenuItemsAdd", connection);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.Add("@parent_id", System.Data.SqlDbType.Int, 0);
        cmd.Parameters["@parent_id"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@parent_id"].Value = parent_id;
        cmd.Parameters.Add("@url", System.Data.SqlDbType.VarChar, 256);
        cmd.Parameters["@url"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@url"].Value = url;
        cmd.Parameters.Add("@sort", System.Data.SqlDbType.Int, 0);
        cmd.Parameters["@sort"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@sort"].Value = sort;
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        System.Data.DataTable table = new DataTable();

        for (int i = 0; (i < reader.FieldCount); i++)
        {
            System.Type __type;
            string __name;
            __type = reader.GetFieldType(i);
            __name = reader.GetName(i);
            table.Columns.Add(__name, __type);
        }

        DataRow result;
        if (reader.Read())
        {
            System.Data.DataRow row = table.NewRow();
            object[] rowdata = new object[reader.FieldCount];
            reader.GetValues(rowdata);
            row.ItemArray = rowdata;
            result = row;
        }
        else
        {
            result = null;
        }
        reader.Close();
        connection.Close();
        return result;
    }
    public static int MenuItemsDel(Int32 id)
    {
        SqlConnection connection = OpenConnection();
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("MenuItemsDel", connection);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.Add("@id", System.Data.SqlDbType.Int, 0);
        cmd.Parameters["@id"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@id"].Value = id; int result = cmd.ExecuteNonQuery();
        connection.Close();
        return result;
    }
    public static DataRow MenuItemsGetById(Int32 id)
    {
        SqlConnection connection = OpenConnection();
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("MenuItemsGetById", connection);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.Add("@id", System.Data.SqlDbType.Int, 0);
        cmd.Parameters["@id"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@id"].Value = id;
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        System.Data.DataTable table = new DataTable();

        for (int i = 0; (i < reader.FieldCount); i++)
        {
            System.Type __type;
            string __name;
            __type = reader.GetFieldType(i);
            __name = reader.GetName(i);
            table.Columns.Add(__name, __type);
        }

        DataRow result;
        if (reader.Read())
        {
            System.Data.DataRow row = table.NewRow();
            object[] rowdata = new object[reader.FieldCount];
            reader.GetValues(rowdata);
            row.ItemArray = rowdata;
            result = row;
        }
        else
        {
            result = null;
        }
        reader.Close();
        connection.Close();
        return result;
    }
    public static DataTable MenuItemsGetByParent(Int32 parent_id)
    {
        SqlConnection connection = OpenConnection();
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("MenuItemsGetByParent", connection);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.Add("@parent_id", System.Data.SqlDbType.Int, 0);
        cmd.Parameters["@parent_id"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@parent_id"].Value = parent_id;
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        System.Data.DataTable table = new DataTable();

        for (int i = 0; (i < reader.FieldCount); i++)
        {
            System.Type __type;
            string __name;
            __type = reader.GetFieldType(i);
            __name = reader.GetName(i);
            table.Columns.Add(__name, __type);
        }

        while (reader.Read())
        {
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
    public static int MenuItemsUpdate(Int32 id, Int32 parent_id, String url, Int32 sort)
    {
        SqlConnection connection = OpenConnection();
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("MenuItemsUpdate", connection);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.Add("@id", System.Data.SqlDbType.Int, 0);
        cmd.Parameters["@id"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@id"].Value = id;
        cmd.Parameters.Add("@parent_id", System.Data.SqlDbType.Int, 0);
        cmd.Parameters["@parent_id"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@parent_id"].Value = parent_id;
        cmd.Parameters.Add("@url", System.Data.SqlDbType.VarChar, 256);
        cmd.Parameters["@url"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@url"].Value = url;
        cmd.Parameters.Add("@sort", System.Data.SqlDbType.Int, 0);
        cmd.Parameters["@sort"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@sort"].Value = sort; int result = cmd.ExecuteNonQuery();
        connection.Close();
        return result;
    }
    public static DataRow PostAdd(String title, String desc, String text, Int32 cat_id, Byte attached, String source, Int32 user_id)
    {
        SqlConnection connection = OpenConnection();
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("PostAdd", connection);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.Add("@title", System.Data.SqlDbType.VarChar, 32);
        cmd.Parameters["@title"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@title"].Value = title;
        cmd.Parameters.Add("@desc", System.Data.SqlDbType.VarChar, 512);
        cmd.Parameters["@desc"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@desc"].Value = desc;
        cmd.Parameters.Add("@text", System.Data.SqlDbType.VarChar, 2048);
        cmd.Parameters["@text"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@text"].Value = text;
        cmd.Parameters.Add("@cat_id", System.Data.SqlDbType.Int, 0);
        cmd.Parameters["@cat_id"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@cat_id"].Value = cat_id;
        cmd.Parameters.Add("@attached", System.Data.SqlDbType.TinyInt, 0);
        cmd.Parameters["@attached"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@attached"].Value = attached;
        cmd.Parameters.Add("@source", System.Data.SqlDbType.VarChar, 256);
        cmd.Parameters["@source"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@source"].Value = source;
        cmd.Parameters.Add("@user_id", System.Data.SqlDbType.Int, 0);
        cmd.Parameters["@user_id"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@user_id"].Value = user_id;
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        System.Data.DataTable table = new DataTable();

        for (int i = 0; (i < reader.FieldCount); i++)
        {
            System.Type __type;
            string __name;
            __type = reader.GetFieldType(i);
            __name = reader.GetName(i);
            table.Columns.Add(__name, __type);
        }

        DataRow result;
        if (reader.Read())
        {
            System.Data.DataRow row = table.NewRow();
            object[] rowdata = new object[reader.FieldCount];
            reader.GetValues(rowdata);
            row.ItemArray = rowdata;
            result = row;
        }
        else
        {
            result = null;
        }
        reader.Close();
        connection.Close();
        return result;
    }
    public static int PostDel(Int32 id)
    {
        SqlConnection connection = OpenConnection();
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("PostDel", connection);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.Add("@id", System.Data.SqlDbType.Int, 0);
        cmd.Parameters["@id"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@id"].Value = id; int result = cmd.ExecuteNonQuery();
        connection.Close();
        return result;
    }
    public static DataTable PostGet(Int32 page, Int32 count)
    {
        SqlConnection connection = OpenConnection();
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("PostGet", connection);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.Add("@page", System.Data.SqlDbType.Int, 0);
        cmd.Parameters["@page"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@page"].Value = page;
        cmd.Parameters.Add("@count", System.Data.SqlDbType.Int, 0);
        cmd.Parameters["@count"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@count"].Value = count;
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        System.Data.DataTable table = new DataTable();

        for (int i = 0; (i < reader.FieldCount); i++)
        {
            System.Type __type;
            string __name;
            __type = reader.GetFieldType(i);
            __name = reader.GetName(i);
            table.Columns.Add(__name, __type);
        }

        while (reader.Read())
        {
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
    public static DataTable PostGetAttached()
    {
        SqlConnection connection = OpenConnection();
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("PostGetAttached", connection);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        System.Data.DataTable table = new DataTable();

        for (int i = 0; (i < reader.FieldCount); i++)
        {
            System.Type __type;
            string __name;
            __type = reader.GetFieldType(i);
            __name = reader.GetName(i);
            table.Columns.Add(__name, __type);
        }

        while (reader.Read())
        {
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
    public static DataRow PostGetById(Int32 id)
    {
        SqlConnection connection = OpenConnection();
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("PostGetById", connection);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.Add("@id", System.Data.SqlDbType.Int, 0);
        cmd.Parameters["@id"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@id"].Value = id;
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        System.Data.DataTable table = new DataTable();

        for (int i = 0; (i < reader.FieldCount); i++)
        {
            System.Type __type;
            string __name;
            __type = reader.GetFieldType(i);
            __name = reader.GetName(i);
            table.Columns.Add(__name, __type);
        }

        DataRow result;
        if (reader.Read())
        {
            System.Data.DataRow row = table.NewRow();
            object[] rowdata = new object[reader.FieldCount];
            reader.GetValues(rowdata);
            row.ItemArray = rowdata;
            result = row;
        }
        else
        {
            result = null;
        }
        reader.Close();
        connection.Close();
        return result;
    }
    public static DataTable PostGetTop(Int32 period, Int32 count)
    {
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

        for (int i = 0; (i < reader.FieldCount); i++)
        {
            System.Type __type;
            string __name;
            __type = reader.GetFieldType(i);
            __name = reader.GetName(i);
            table.Columns.Add(__name, __type);
        }

        while (reader.Read())
        {
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
    public static int PostUpdate(Int32 id, String title, String desc, String text, Int32 cat_id, Byte attached, String source)
    {
        SqlConnection connection = OpenConnection();
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("PostUpdate", connection);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.Add("@id", System.Data.SqlDbType.Int, 0);
        cmd.Parameters["@id"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@id"].Value = id;
        cmd.Parameters.Add("@title", System.Data.SqlDbType.VarChar, 32);
        cmd.Parameters["@title"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@title"].Value = title;
        cmd.Parameters.Add("@desc", System.Data.SqlDbType.VarChar, 512);
        cmd.Parameters["@desc"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@desc"].Value = desc;
        cmd.Parameters.Add("@text", System.Data.SqlDbType.VarChar, 2048);
        cmd.Parameters["@text"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@text"].Value = text;
        cmd.Parameters.Add("@cat_id", System.Data.SqlDbType.Int, 0);
        cmd.Parameters["@cat_id"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@cat_id"].Value = cat_id;
        cmd.Parameters.Add("@attached", System.Data.SqlDbType.TinyInt, 0);
        cmd.Parameters["@attached"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@attached"].Value = attached;
        cmd.Parameters.Add("@source", System.Data.SqlDbType.VarChar, 256);
        cmd.Parameters["@source"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@source"].Value = source; int result = cmd.ExecuteNonQuery();
        connection.Close();
        return result;
    }
    public static int PostUpdateViews(Int32 id)
    {
        SqlConnection connection = OpenConnection();
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("PostUpdateViews", connection);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.Add("@id", System.Data.SqlDbType.Int, 0);
        cmd.Parameters["@id"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@id"].Value = id; int result = cmd.ExecuteNonQuery();
        connection.Close();
        return result;
    }
    public static DataRow UserAdd(String nick, String pass, Byte role, String email)
    {
        SqlConnection connection = OpenConnection();
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("UserAdd", connection);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.Add("@nick", System.Data.SqlDbType.VarChar, 32);
        cmd.Parameters["@nick"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@nick"].Value = nick;
        cmd.Parameters.Add("@pass", System.Data.SqlDbType.VarChar, 1024);
        cmd.Parameters["@pass"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@pass"].Value = pass;
        cmd.Parameters.Add("@role", System.Data.SqlDbType.TinyInt, 0);
        cmd.Parameters["@role"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@role"].Value = role;
        cmd.Parameters.Add("@email", System.Data.SqlDbType.VarChar, 32);
        cmd.Parameters["@email"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@email"].Value = email;
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        System.Data.DataTable table = new DataTable();

        for (int i = 0; (i < reader.FieldCount); i++)
        {
            System.Type __type;
            string __name;
            __type = reader.GetFieldType(i);
            __name = reader.GetName(i);
            table.Columns.Add(__name, __type);
        }

        DataRow result;
        if (reader.Read())
        {
            System.Data.DataRow row = table.NewRow();
            object[] rowdata = new object[reader.FieldCount];
            reader.GetValues(rowdata);
            row.ItemArray = rowdata;
            result = row;
        }
        else
        {
            result = null;
        }
        reader.Close();
        connection.Close();
        return result;
    }
    public static int UserDel(Int32 userId)
    {
        SqlConnection connection = OpenConnection();
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("UserDel", connection);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.Add("@userId", System.Data.SqlDbType.Int, 0);
        cmd.Parameters["@userId"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@userId"].Value = userId; int result = cmd.ExecuteNonQuery();
        connection.Close();
        return result;
    }
    public static DataRow UserGetById(Int32 userId)
    {
        SqlConnection connection = OpenConnection();
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("UserGetById", connection);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.Add("@userId", System.Data.SqlDbType.Int, 0);
        cmd.Parameters["@userId"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@userId"].Value = userId;
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        System.Data.DataTable table = new DataTable();

        for (int i = 0; (i < reader.FieldCount); i++)
        {
            System.Type __type;
            string __name;
            __type = reader.GetFieldType(i);
            __name = reader.GetName(i);
            table.Columns.Add(__name, __type);
        }

        DataRow result;
        if (reader.Read())
        {
            System.Data.DataRow row = table.NewRow();
            object[] rowdata = new object[reader.FieldCount];
            reader.GetValues(rowdata);
            row.ItemArray = rowdata;
            result = row;
        }
        else
        {
            result = null;
        }
        reader.Close();
        connection.Close();
        return result;
    }
    public static DataRow UserGetByLogin(String login)
    {
        SqlConnection connection = OpenConnection();
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("UserGetByLogin", connection);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.Add("@login", System.Data.SqlDbType.VarChar, 32);
        cmd.Parameters["@login"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@login"].Value = login;
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        System.Data.DataTable table = new DataTable();

        for (int i = 0; (i < reader.FieldCount); i++)
        {
            System.Type __type;
            string __name;
            __type = reader.GetFieldType(i);
            __name = reader.GetName(i);
            table.Columns.Add(__name, __type);
        }

        DataRow result;
        if (reader.Read())
        {
            System.Data.DataRow row = table.NewRow();
            object[] rowdata = new object[reader.FieldCount];
            reader.GetValues(rowdata);
            row.ItemArray = rowdata;
            result = row;
        }
        else
        {
            result = null;
        }
        reader.Close();
        connection.Close();
        return result;
    }
    public static DataTable UserGetByRole(Int32 role)
    {
        SqlConnection connection = OpenConnection();
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("UserGetByRole", connection);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.Add("@role", System.Data.SqlDbType.Int, 0);
        cmd.Parameters["@role"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@role"].Value = role;
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        System.Data.DataTable table = new DataTable();

        for (int i = 0; (i < reader.FieldCount); i++)
        {
            System.Type __type;
            string __name;
            __type = reader.GetFieldType(i);
            __name = reader.GetName(i);
            table.Columns.Add(__name, __type);
        }

        while (reader.Read())
        {
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
    public static DataTable UserGetLastRegistered(Int32 count)
    {
        SqlConnection connection = OpenConnection();
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("UserGetLastRegistered", connection);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.Add("@count", System.Data.SqlDbType.Int, 0);
        cmd.Parameters["@count"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@count"].Value = count;
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        System.Data.DataTable table = new DataTable();

        for (int i = 0; (i < reader.FieldCount); i++)
        {
            System.Type __type;
            string __name;
            __type = reader.GetFieldType(i);
            __name = reader.GetName(i);
            table.Columns.Add(__name, __type);
        }

        while (reader.Read())
        {
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
    public static DataTable UserGetTopPosters(Int32 count)
    {
        SqlConnection connection = OpenConnection();
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("UserGetTopPosters", connection);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.Add("@count", System.Data.SqlDbType.Int, 0);
        cmd.Parameters["@count"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@count"].Value = count;
        System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
        System.Data.DataTable table = new DataTable();

        for (int i = 0; (i < reader.FieldCount); i++)
        {
            System.Type __type;
            string __name;
            __type = reader.GetFieldType(i);
            __name = reader.GetName(i);
            table.Columns.Add(__name, __type);
        }

        while (reader.Read())
        {
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
    public static int UserUpdate(Int32 user_id, String pass, Byte role, String email)
    {
        SqlConnection connection = OpenConnection();
        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("UserUpdate", connection);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.Add("@user_id", System.Data.SqlDbType.Int, 0);
        cmd.Parameters["@user_id"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@user_id"].Value = user_id;
        cmd.Parameters.Add("@pass", System.Data.SqlDbType.VarChar, 1024);
        cmd.Parameters["@pass"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@pass"].Value = pass;
        cmd.Parameters.Add("@role", System.Data.SqlDbType.TinyInt, 0);
        cmd.Parameters["@role"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@role"].Value = role;
        cmd.Parameters.Add("@email", System.Data.SqlDbType.VarChar, 32);
        cmd.Parameters["@email"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters["@email"].Value = email; int result = cmd.ExecuteNonQuery();
        connection.Close();
        return result;
    }
    //TODO: Скоро этот кусок исчезнет. И нехай.
}