using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data.SqlClient;
using System.Text;
using System.Data;

using ITCommunity.Core;

namespace ITCommunity.Modules {

    public class WsusFile {

        private static readonly string WSUS_DIR_KEY = "WsusContentPath";

        public string   Name { get; set; }
        public string   Description { get;set; }
        public DateTime ModifiedDate { get; set; }
        public string   Digest { get; set; }
        
        public string RealPath {
            get {
                return Config.Get(WSUS_DIR_KEY) + Digest.Substring(Digest.Length - 2, 2) + @"\" + Digest + Path.GetExtension(Name);
            }
        }
        
        public WsusFile(string name, 
                        string description, 
                        DateTime modifiedDate, 
                        string digest) {
            Name         = name;
            Description  = description;
            ModifiedDate = modifiedDate;
            Digest       = digest;
        }
    }

    public class Wsus {

        private static readonly string WSUS_CONN_KEY = "WsusConnectionString";

        public static WsusFile get(string filename) {
            List<WsusFile> files = search(filename, "", "");
            WsusFile f = null;
            if (files.Count == 1) {
                f = files[0];
            }
            return f;
        }

        public static List<WsusFile> search(string startWith, string contains1, string contains2) {
            List<WsusFile> files = new List<WsusFile>();
            string sql = @"select 
                                F.filename as filename, 
                                F.modified as modifiedDate, 
                                F.filedigest, 
                                L.description as description
                           from tbFile F
                           left outer join tbFileOnServer     S    on    F.FileDigest        = S.FileDigest and S.ActualState <> 1 
                           left outer join tbFileForRevision  FR   on    FR.FileDigest       = F.FileDigest
                           left outer join tbRevision         R    on    R.RevisionID        = FR.RevisionID  
                           left outer join tbBundleDependency B    on    B.BundledRevisionID = FR.RevisionID 
                           left outer join tbRevision         BR   on    BR.RevisionID       = B.RevisionID 
                           left outer join tbUpdate           U    on    U.LocalUpdateID     = bR.LocalUpdateID  
                           left outer join tbPreComputedLocalizedProperty L on L.UpdateID = U.UpdateID AND L.ShortLanguage = 'ru' 
                           
                           where R.IsLatestRevision = 1 and F.filename like @beginWith ";

            SqlConnection conn = new SqlConnection(Config.Get(WSUS_CONN_KEY));
            try {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.Parameters.Add("@beginWith", SqlDbType.VarChar).Value = startWith + "%";
                if (contains1 != null) {
                    sql += " and filename like @contains1 ";
                }
                cmd.Parameters.Add("@contains1", SqlDbType.VarChar).Value = "%" + contains1 + "%";
                if (contains2 != null) {
                    sql += " and filename like @contains2 ";
                }
                cmd.Parameters.Add("@contains2", SqlDbType.VarChar).Value = "%" + contains2 + "%";
                cmd.CommandText = sql + " order by F.filename, R.lastIsLeafChange";

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    files.Add(parse(reader));
                }
            } catch (SqlException ex) {
                Logger.Log.Error("Ошибка при поиске обновлений в БД WSUS", ex);
            } finally {
                if (conn != null && conn.State != ConnectionState.Closed) {
                    conn.Close();
                }
                conn = null;
                
            }
            return files;
        }

        private static WsusFile parse(SqlDataReader reader) {

            byte[] digestBuffer = new byte[20];
            reader.GetBytes(2, 0L, digestBuffer, 0, 20);

            return new WsusFile(reader["filename"].ToString(),
                                reader["description"].ToString(),
                                (DateTime)reader["modifiedDate"],
                                getHex(digestBuffer));
        }

        private static string getHex(byte[] bytes) {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++) {
                string str = Convert.ToString(bytes[i], 0x10);
                if (str.Length == 1) {
                    builder.Append("0");
                }
                builder.Append(str);
            }
            return builder.ToString();
        }
    }
}