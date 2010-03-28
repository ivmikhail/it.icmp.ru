using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lucene.Net.Index;
using System.IO;
using Lucene.Net.Store;
using Lucene.Net.Documents;
using System.Diagnostics;
using Lucene.Net.Search;
using Lucene.Net.QueryParsers;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;

namespace ITCommunity.IndexerLib {
    public class Indexer {
        // Названия полей документов в индексе Lucene
        private static class DocField { 
            internal const String Id = "post_id";
            internal const String Text = "post";
            internal const String Title = "title";
        }
    
        private static Indexer indexer = null;
        private static readonly Analyzer analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29);
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
        private IndexReader _indexReader = null;
        private IndexReader indexReader {
            get {
                if (_indexReader == null) {
                    _indexReader = IndexReader.Open(this.indexerDirectory, true);
                }
                return _indexReader;
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
            doc.Add(new Field(DocField.Title, title, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field(DocField.Text, postText, Field.Store.YES, Field.Index.ANALYZED));
            doc.Add(new Field(DocField.Id, postId, Field.Store.YES, Field.Index.NO));
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
        /// <summary>
        /// Возвращает найденные 
        /// </summary>
        /// <param name="queryText"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <param name="posts_count"></param>
        /// <returns></returns>
        public List<SearchedPost> Search(string queryText, int page, int count, ref int posts_count) {
            String[] fields = new String[]{DocField.Title, DocField.Text};
            QueryParser queryParser = new MultiFieldQueryParser(fields, analyzer);
            Query query = queryParser.Parse(queryText);
            IndexSearcher searcher = new IndexSearcher(indexReader);
            TopFieldDocs topDocs = searcher.Search(searcher.CreateWeight(query), null, 10000, Sort.RELEVANCE);

            List<SearchedPost> result = new List<SearchedPost>(topDocs.totalHits);
            int startDocIndex = page * count;
            for(int i = (page-1) * count; i < Math.Min(page*count - 1, topDocs.scoreDocs.Length); i++) {
                ScoreDoc scoreDoc = topDocs.scoreDocs[i];
                Document document = searcher.Doc(scoreDoc.doc);
                SearchedPost post = new SearchedPost(Int32.Parse(document.Get(DocField.Id)), 
                    document.Get(DocField.Title), document.Get(DocField.Text));
                result.Add(post);
            }
            posts_count = topDocs.scoreDocs.Length;
            return result;

        }
    }
}
