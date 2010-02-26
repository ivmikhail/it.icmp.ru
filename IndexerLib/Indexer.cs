using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lucene.Net.Index;
using System.IO;
using Lucene.Net.Store;
using Lucene.Net.Documents;
using System.Diagnostics;

namespace ITCommunity.IndexerLib {
    public class Indexer {
        private static Indexer indexer = null;
        private string connectionString;
        private string indexerPath;
        private Lucene.Net.Store.Directory indexerDirectory;
        private IndexWriter _indexWriter = null;
        private IndexWriter indexWriter {
            get {
                if (_indexWriter == null) {
                    String[] files = System.IO.Directory.GetFiles(this.indexerPath);
                    bool isCreateindexes = files.Length == 0;
                    try {
                        this._indexWriter = new IndexWriter(this.indexerDirectory, new Lucene.Net.Analysis.Standard.StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_CURRENT), isCreateindexes, IndexWriter.MaxFieldLength.UNLIMITED);
                    } catch (LockObtainFailedException ex) {
                        Debug.Print(ex.Message);
                        IndexWriter.Unlock(this.indexerDirectory);
                        this._indexWriter = new IndexWriter(this.indexerDirectory, new Lucene.Net.Analysis.Standard.StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_CURRENT), isCreateindexes, IndexWriter.MaxFieldLength.UNLIMITED);
                    }
                }
                return _indexWriter;
            }
        }

        private Indexer(string connectionString, string indexerPath) {
            // TODO: Complete member initialization
            this.connectionString = connectionString;
            this.indexerPath = indexerPath;

            indexerDirectory = FSDirectory.Open(new DirectoryInfo(indexerPath));
        }
        public static void Init(String connectionString, String indexerPath) {
            if (indexer != null) {
                throw new ArgumentException("Cannot call Init() method twice");
            } else {
                if (checkPath(indexerPath)) {
                    indexer = new Indexer(connectionString, indexerPath);
                } else {
                    throw new ArgumentException("Cannot find indexes path: " + indexerPath);
                }
            }
        }

        private static bool checkPath(string indexerPath) {
            if (System.IO.Directory.Exists(indexerPath)) {
                return true;
            } else {
                return false;
            }

        }
        public static Indexer GetInstance() {
            if (indexer == null) {
                throw new ArgumentException("call Init() method before getting instance of indexer");
            } else {
                return indexer;
            }
        }
        public void UpdateDocument(String title, String postText, String postId) {
            Document doc = new Document();
            doc.Add(new Field("title", title, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("post", postText, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field("post_id", postId, Field.Store.YES, Field.Index.NO));
            indexWriter.UpdateDocument(new Term(postId), doc);

        }
        public void Commit() {
            indexWriter.Commit();
        }
        public void Close() {
            indexWriter.Close();
        }
        public void Optimize() {
            indexWriter.Optimize();
        }
    }
}
