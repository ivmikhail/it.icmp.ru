using System;
using System.Collections.Generic;

using ITCommunity.Core;
using ITCommunity.Db;
using ITCommunity.Db.Tables;


namespace ITCommunity.Models {

    public abstract class MessageListModel : PaginatedModel {

        public List<Message> List {
            get;
            private set;
        }

        public MessageListModel(int? page)
            : base(page) {
            PerPage = 20;
            List = GetMessages();
        }

        protected virtual List<Message> GetMessages() {
            throw new NotImplementedException();
        }
    }
}
