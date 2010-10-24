using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using ITCommunity.Core;
using ITCommunity.DB;
using ITCommunity.DB.Tables;


namespace ITCommunity.Models {

    public class RfcListModel {

        public string Query {
            get;
            private set;
        }
        public List<Rfc> List {
            get;
            private set;
        }
        public RfcListModel() {
            Query = "";
            List = null;
        }
        public RfcListModel(string query) {
            List<Rfc> result = null;
            if (query != string.Empty) {
                if (Regex.IsMatch(query, "[0-9]+") && query.Length <= 4) {
                    result = Rfcs.getByNum(query);
                } else {
                    result = Rfcs.getByTitle(query);
                }
            }
            Query = query;
            List = result;
        }
    }
}
