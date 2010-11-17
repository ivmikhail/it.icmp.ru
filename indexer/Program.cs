using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using Lucene.Net.Index;
using Lucene.Net.Analysis;
using Lucene.Net.Store;
using Lucene.Net.Documents;
using System.Diagnostics;
using ITCommunity.IndexerLib;


namespace ITCommunity.IndexerProg {
    class Program {
        static void Main(string[] args) {
            index(args);
            //postCount(args);
        }

        private static void postCount(string[] args) {
            Libs libs = new Libs(args[0]);
            Console.WriteLine(libs.PostFreq(args[1]));
            libs.Close();
        }
        private static void index(String[] args) {
            String webConfigPath = getWebConfigPath(args);
            String indexPath = getIndexesPath(args);
            if (webConfigPath == String.Empty || indexPath == String.Empty) {
                Console.WriteLine("indexer <web.config path> <index dir path>");
                return;
            }


            String connString = SomeLib.TextUtil.GetConnectionStringFromXml(webConfigPath);
            Console.WriteLine(connString);

            Indexer.Init(connString, indexPath);
            Indexer indexer = Indexer.GetInstance();

            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from posts", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            int count = 0;
            while (reader.Read()) {
                String title = reader["title"].ToString();
                String text = reader["description"].ToString();
                if (!text.EndsWith(" ")) {
                    text += " ";
                }
                text += reader["text"].ToString();
                String postId = reader["id"].ToString();
                String timestamp = ((DateTime)reader["CreateDate"]).ToString("u");
                indexer.UpdateDocument(title, text, postId, timestamp);
                count++;
                if (count % 100 == 0) {
                    Console.WriteLine(count);
                }
            }
            conn.Close();
            indexer.Optimize();
            indexer.Close();
        }
        private static string getIndexesPath(string[] args) {
            String result = String.Empty;
            if (args.Length > 1) {
                if (!System.IO.Directory.Exists(args[1])) {
                    Console.WriteLine("Index directory " + args[1] + " not exist");
                } else {
                    result = args[1];
                }
            }
            return result;
        }

        private static string getWebConfigPath(string[] args) {
            String result = String.Empty;
            if (args.Length > 0) {
                if(!File.Exists(args[0])) {
                    Console.WriteLine("web.config in " + args[0] + " not exist");
                } else {
                    result = args[0];
                }
            }
            return result;
        }
    }
}
