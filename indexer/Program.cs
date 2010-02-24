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


namespace indexer {
    class Program {
        static void Main(string[] args) {
            string webConfigPath = getWebConfigPath(args);
            string indexPath = getIndexesPath(args);
            if (webConfigPath == string.Empty || indexPath == string.Empty) {
                Console.WriteLine("indexer <web.config path> <index dir path>");
                return;
            }
            string connString = SomeLib.TextUtil.GetConnectionStringFromXml(webConfigPath);
            Console.WriteLine(connString);

            string[] files =  System.IO.Directory.GetFiles(indexPath);
            bool isCreateindexes = files.Length==0;
            IndexWriter indexWriter = null;
            Lucene.Net.Store.Directory directory = FSDirectory.Open(new DirectoryInfo(indexPath));
            try {
                indexWriter = new IndexWriter(directory, new Lucene.Net.Analysis.Standard.StandardAnalyzer(), isCreateindexes);
            } catch (LockObtainFailedException ex) {
                IndexWriter.Unlock(directory);
                indexWriter = new IndexWriter(directory, new Lucene.Net.Analysis.Standard.StandardAnalyzer(), isCreateindexes);
            }
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from posts", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            int count = 0;
            while (reader.Read()) {
                string title = reader["title"].ToString();
                string text = reader["description"].ToString();
                if(!text.EndsWith(" ")) {
                    text += " ";
                }
                text += reader["text"].ToString();
                string postId = reader["id"].ToString();
                Document doc = new Document();
                doc.Add(new Field("title", title, Field.Store.YES, Field.Index.ANALYZED));
                doc.Add(new Field("post", text, Field.Store.YES, Field.Index.ANALYZED));
                doc.Add(new Field("post_id", postId, Field.Store.YES, Field.Index.NO));
                indexWriter.UpdateDocument(new Term(postId), doc);
                count++;
                if (count % 100 == 0) {
                    Console.WriteLine(count);
                }
            }
            conn.Close();
            indexWriter.Optimize();
            indexWriter.Close();
        }

        private static string getIndexesPath(string[] args) {
            string result = string.Empty;
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
            string result = string.Empty;
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
