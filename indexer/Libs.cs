using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITCommunity.IndexerLib;

namespace ITCommunity.IndexerProg {
    public class Libs {
        private readonly Indexer indexer;
        public Libs(String indexPath) {
            Indexer.Init("", indexPath);
            indexer = Indexer.GetInstance();
        }
        public int PostFreq(String id) {
            return indexer.PostFreq(id);
        }


        internal void Close() {
            indexer.Close();
        }
    }
}
